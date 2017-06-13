using Alten.Connected_Vehicles.Infrastructure.Logger;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace Alten.Connected_Vehicles.TCPServer.Common.Server
{
    public class TCPSocketListener :ISocketListener
    {
        #region Memeber Data 

        public Socket m_clientSocket = null;
        private bool m_stopClient = false;
        private Thread m_clientListenerThread = null;
        private bool m_markedForDeletion = false;
        

        //multi-threading control handles
        ManualResetEvent doneEvent = new ManualResetEvent(false);
        ManualResetEvent connectedEvent = new ManualResetEvent(false);
        ManualResetEvent sentEvent = new ManualResetEvent(false);
               
        
        // Time monitors
        private DateTime m_lastReceiveDateTime;
        private DateTime m_currentReceiveDateTime;

       
        private MSMQManager m_QueueManager;
        private Logger log = new Logger();
        #endregion

        #region CTORS
        public TCPSocketListener(Socket clientSocket)
        {
            m_clientSocket = clientSocket;
           
            
        }
        #endregion

        #region Destructor 
        ~TCPSocketListener()
        {
            StopSocketListener();
        }
        #endregion 



        #region ISocket listener Impelmented 
        /// <summary>
        /// Method that starts SocketListener Thread.
        /// </summary>
        public void StartSocketListener()
        {
            m_QueueManager = new MSMQManager();
            if (m_clientSocket != null)
            {
                m_clientListenerThread =
                    new Thread(new ThreadStart(SocketDataReceiver));

                m_clientListenerThread.Start();
            }
        }

        /// <summary>
        /// Thread method that does the communication to the client. This 
        /// thread tries to receive from client and if client sends any data
        /// then parses it and again wait for the client data to come in
        /// an Asynchronous way through a callback method 
        /// </summary>
        public void SocketDataReceiver()
        {
            // Define the Socket listener time out
            Timer t = new Timer(new TimerCallback(DisconnectClient),
               null, 40000, 360000); // this planned to be configuration 


            StateObject state = new StateObject();
            state.buffer = new Byte[m_clientSocket.ReceiveBufferSize];

            m_clientSocket.BeginReceive(state.buffer, 0, state.buffer.Length, SocketFlags.None, SocketReadCallBack, state);

            t.Change(Timeout.Infinite, Timeout.Infinite);
            t = null;
        }

        public void AcceptClientData(byte[] bufferdata)
        {
            // Send Data to Queue
            m_QueueManager.SendToQueue(bufferdata);
            // Save Raw Message for archiving 
            


            // Save Raw Message into log File
            //the purpose of adding such line is to keep track of the incoming transactions 
            // and avoid missing any packet
            log.LogToFile(string.Format("Raw Data: {0} \r\n",bufferdata.ToString()));
        }

        public bool AuthenticateClient()
        {
            //Implement Client Authentications based on Device protocol 
            return true;
        }

        public void StopSocketListener()
        {
            if (m_clientSocket != null)
            {
                m_stopClient = true;
                m_clientSocket.Close();

                // Wait for one second for the the thread to stop.
                m_clientListenerThread.Join(10000);

                // If still alive; Get rid of the thread.
                if (m_clientListenerThread.IsAlive)
                {
                    m_clientListenerThread.Abort();
                }
                m_clientListenerThread = null;
                m_clientSocket = null;
                m_markedForDeletion = true;

                

            }
        }

        public bool IsMarkedForDeletion()
        {

            return false;
        }


        #endregion

        #region Event Handlers & Callbacks
        public void DisconnectClient(object o)
        {
            if (m_lastReceiveDateTime.Equals(m_currentReceiveDateTime))
            {
                this.StopSocketListener();
            }
            else
            {
                m_lastReceiveDateTime = m_currentReceiveDateTime;
            }
        }

        private void SocketReadCallBack(IAsyncResult result)
        {
            if (!result.IsCompleted)
            {
                int legth = m_clientSocket.EndReceive(result);
                StateObject state2 = new StateObject();
                state2.buffer = new Byte[legth];

                m_clientSocket.BeginReceive(state2.buffer, 0, state2.buffer.Length, SocketFlags.None, SocketReadCallBack, state2);
                return;
            }

            int size = 0;

            try
            {
                int R = m_clientSocket.EndReceive(result);

                if (R == 0)
                {
                    m_stopClient = true;
                    m_markedForDeletion = true;
                    return;
                }
                else
                {
                    StateObject ostate = result.AsyncState as StateObject;
                    size = R;

                    m_currentReceiveDateTime = DateTime.Now;
                    if (size > 0)
                    {
                        Byte[] databuffer = new Byte[size];
                        Array.Copy(ostate.buffer, 0, databuffer, 0, size);
                        AcceptClientData(databuffer);

                        doneEvent.Set();
                        if (!m_markedForDeletion)
                        {
                            SocketDataReceiver();
                        }
                    }

                }
            }
            catch (SocketException se)
            {
                log.LogToFile(string.Format("Exception: {0} - StackTrace : {1}", se.Message, se.StackTrace));
                m_stopClient = true;
                m_markedForDeletion = true;
            }
        }
        #endregion
    }
}
