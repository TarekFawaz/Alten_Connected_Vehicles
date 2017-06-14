using Alten.Connected_Vehicles.Infrastructure.Logger;
using Alten.Connected_Vehicles.TCPServer.Common.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Alten.Connected_Vehicles.TCPServer.Common.Server
{
    /// <summary>
    /// TCPServer is the Server class. When "StartServer" method is called
    /// this Server object tries to connect to a IP Address specified on a port
    /// configured. Then the server start listening for client socket requests.
    /// As soon as a request comes in from any client then a Client Socket 
    /// Listening thread will be started. That thread is responsible for client
    /// communication.
    /// </summary>
    public class TCPServer: IServer
    {
        #region Data Members 
        public static IPAddress DEFAULT_SERVER = IPAddress.Parse("127.0.0.1");
        public static int DEFAULT_PORT = 88;
        public static IPEndPoint DEFAULT_IP_END_POINT =
            new IPEndPoint(DEFAULT_SERVER, DEFAULT_PORT);

        public IPAddress SERVER_IP;
        public int SERVER_PORT;
           
        
        
        /// <summary>
        /// Local Variables Declaration.
        /// </summary>
        private TcpListener m_server = null;
        private bool m_stopServer = false;
        private bool m_stopPurging = false;
        private Thread m_serverThread = null;
        private Thread m_purgingThread = null;
        private List<ISocketListener> m_socketListenersList = null;
        private Logger log = new Logger();
        #endregion

        #region CTORS
        public TCPServer()
        {
            Init(DEFAULT_IP_END_POINT);
        }
        public TCPServer(IPAddress serverIP)
        {
            Init(new IPEndPoint(serverIP, DEFAULT_PORT));
        }

        public TCPServer(int port)
        {
            Init(new IPEndPoint(DEFAULT_SERVER, port));
        }

        public TCPServer(IPAddress serverIP, int port)
        {
            Init(new IPEndPoint(serverIP, port));
        }

        public TCPServer(string serverIP, string port)
        {
            if (IPAddress.TryParse(serverIP, out SERVER_IP) && int.TryParse(port, out SERVER_PORT))
            {
                Init(new IPEndPoint(SERVER_IP, SERVER_PORT));
            }
            else
            {
                log.LogToFile("Fail Fast : Could not Parse Server IP or Server Port");
                throw new Exception("Fail Fast : Could not Parse Server IP or Server Port");
            }
        }

        public TCPServer(IPEndPoint ipNport)
        {
            Init(ipNport);
        }

        #endregion

        #region Destructor 
        /// <summary>
        /// Need destructor to stop the server before disposing the server object
        /// </summary>
        ~TCPServer()
        {
            StopServer();
        }
        #endregion 

        #region IServer Implemented
        /// <summary>
        /// Init method that create a server (TCP Listener) Object based on the
        /// IP Address and Port information that is passed in.
        /// </summary>
        /// <param name="ipNport"></param>
        public void Init(IPEndPoint ipNport)
        {
            try
            {
                m_server = new TcpListener(ipNport);
                
            }
            catch (Exception ex)
            {
                m_server = null;
                log.LogToFile(string.Format("TCP Server Exception: Message {0} - Stack Trace{1}", ex.Message, ex.StackTrace));
                throw new Exception("Unable to intialize TCP Server ");
            }
        }

        /// <summary>
        /// Method that starts TCP/IP Server.
        /// </summary>
        public void StartServer()
        {
            if (m_server != null)
            {
                // Create a List for storing SocketListeners before
                // starting the server.
                m_socketListenersList = new List<ISocketListener>();

                // Start the Server and start the thread to listen client 
                // requests.
                m_server.Start();
                m_serverThread = new Thread(new ThreadStart(ServerWaitClient));
                m_serverThread.Start();

                // Create a low priority thread that checks and deletes client
                // SocktConnection objcts that are marked for deletion.
                m_purgingThread = new Thread(new ThreadStart(PurgingThreadStart));
                m_purgingThread.Priority = ThreadPriority.Lowest;
                m_purgingThread.Start();

                //Start Self hosted SignalR service 
                try
                {

                    Microsoft.Owin.Hosting.WebApp.Start<Startup>("http://192.168.1.8:3578"); // Port should be configuration 

                    log.LogToFile("start SignalR Self host service");
                }
                catch (Exception e)
                {
                    log.LogToFile(string.Format("Start Signalr self host service Exception : {0}", e.Message));
                }

            }
        }

        /// <summary>
        /// Method that stops the TCP/IP Server.
        /// </summary>
        public void StopServer()
        {
            if (m_server != null)
            {
                // It is important to Stop the server first before doing
                // any cleanup. If not so, clients might being added as
                // server is running, but supporting data structures
                // (such as m_socketListenersList) are cleared. This might
                // cause exceptions.

                // Stop the TCP/IP Server.
                m_stopServer = true;
                m_server.Stop();

                // Wait for one second for the the thread to stop.
                m_serverThread.Join(1000);

                // If still alive; Get rid of the thread.
                if (m_serverThread.IsAlive)
                {
                    m_serverThread.Abort();
                }
                m_serverThread = null;

                m_stopPurging = true;
                m_purgingThread.Join(1000);
                if (m_purgingThread.IsAlive)
                {
                    m_purgingThread.Abort();
                }
                m_purgingThread = null;

                // Free Server Object.
                m_server = null;

                // Stop All clients.
                StopAllSocketListers();
            }
        }

        /// <summary>
        /// Method that stops all clients and clears the list.
        /// </summary>
        public void StopAllSocketListers()
        {
            foreach (ISocketListener socketListener
                        in m_socketListenersList)
            {
                socketListener.StopSocketListener();
            }
            // Remove all elements from the list.
            m_socketListenersList.Clear();
            m_socketListenersList = null;
        }


        /// <summary>
        /// TCP/IP Server Thread that is listening for clients.
        /// </summary>
        public void ServerWaitClient()
        {
            object o = new object();

            m_server.BeginAcceptTcpClient(new AsyncCallback(OnClientConnected), o);
        }

        /// <summary>
        /// Thread method for purging Client Listeneres that are marked for
        /// deletion (i.e. clients with socket connection closed). This thead
        /// is a low priority thread and sleeps for 10 seconds and then check
        /// for any client SocketConnection objects which are obselete and 
        /// marked for deletion.
        /// </summary>
        public void PurgingThreadStart()
        {
            while (!m_stopPurging)
            {
                List<ISocketListener> deleteList = new List<ISocketListener>();

                
                // Check for any clients SocketListeners that are to be
                // deleted and put them in a separate list in a thread sage
                // fashon.
                Monitor.Enter(m_socketListenersList);
                lock (m_socketListenersList)
                {
                    foreach (ISocketListener socketListener
                                 in m_socketListenersList)
                    {
                        if (socketListener.IsMarkedForDeletion())
                        {
                            deleteList.Add(socketListener);
                            socketListener.StopSocketListener();
                            
                        }
                    }

                    // Delete all the client SocketConnection objects which are
                    // in marked for deletion and are in the delete list.
                    for (int i = 0; i < deleteList.Count; ++i)
                    {
                        m_socketListenersList.Remove(deleteList[i]);
                    }

                   
                }
                Monitor.Exit(m_socketListenersList);

                deleteList = null;
                Thread.Sleep(10000);
            }
        }
        #endregion

        #region Events Handlers 

        private void OnClientConnected(IAsyncResult result)
        {
            if (m_stopServer)
            {
                return;
            }
            // Client Socket variable;
            Socket clientSocket = null;
            ISocketListener socketListener = null;

            try
            {
                // Wait for any client requests and if there is any 
                // request from any client accept it (Wait indefinitely).
                clientSocket = m_server.EndAcceptSocket(result);
                //
                MessageQueue msmq = new MessageQueue();
                // Create a SocketListener object for the client.
                socketListener = new TCPSocketListener(clientSocket);

               
                // Add the socket listener to an array list in a thread 
                // safe fashon.
                Monitor.Enter(m_socketListenersList);
                lock (m_socketListenersList)
                {
                    m_socketListenersList.Add(socketListener);
                }
                Monitor.Exit(m_socketListenersList);

                // Start a communicating with the client in a different
                // thread.

                socketListener.StartSocketListener();

            }
            catch (SocketException se)
            {
                log.LogToFile(string.Format("Exception :{0} - Stacktrace: {1}", se.Message, se.StackTrace));
                m_stopServer = true;
            }
            finally
            {
                ServerWaitClient();
            }
        }
        #endregion 
    }
}
