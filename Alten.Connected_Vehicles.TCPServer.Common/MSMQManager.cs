using Alten.Connected_Vehicles.DTO;
using Alten.Connected_Vehicles.Infrastructure.Logger;
using Alten.Connected_Vehicles.TCPServer.Common.DataProcessor;
using Alten.Connected_Vehicles.TCPServer.Common.SignalR;
using Alten.Connected_Vehicles.TCPServer.Common.WebAPIConsumer;
using System;

using System.Messaging;


namespace Alten.Connected_Vehicles.TCPServer.Common
{
    public class MSMQManager
    {

        #region Member data
        private string QUEUE_NAME= ".\\Private$\\ATVS";
        /// <summary>
        /// Messaging Queue
        /// </summary>
        private MessageQueue m_MQ;
        /// <summary>
        /// indicates if the MQ is active or not
        /// </summary>
        private bool isActive;

        private Logger Log=new Logger();
             

        #endregion 

        #region CTOR
        /// <summary>
        /// Getting intialized Queue
        /// </summary>
        /// <param name="Queue"></param>
        public MSMQManager()
        {

            InitializeQueue();
            isActive = true;
            m_MQ.Formatter = new BinaryMessageFormatter();
            m_MQ.PeekCompleted += messageQueue_PeekCompleted;
            m_MQ.BeginPeek();
        }
        #endregion

        #region Initializers 
        private void InitializeQueue()
        {
            Log.LogToFile(string.Format("Initializing new Queue for path {0} for new thread", QUEUE_NAME));
            m_MQ = new MessageQueue();
            try
            {
                if (!MessageQueue.Exists(QUEUE_NAME))
                {
                    m_MQ = MessageQueue.Create(QUEUE_NAME);
                }
                else
                {
                    m_MQ = new MessageQueue(QUEUE_NAME);
                }
                m_MQ.Formatter = new BinaryMessageFormatter();

               
            }
            catch (Exception ex)
            {
                Log.LogToFile(string.Format("Exception: {0} - stack Trace {1}", ex.Message, ex.StackTrace));
            }
            
        }
        #endregion

        #region Send
        public void SendToQueue(byte[] bufferdata)
        {
            try
            {
                if (m_MQ != null)
                {
                    m_MQ.Send(bufferdata);
                }
            }
            catch (Exception ex)
            {
                Log.LogToFile(string.Format("MSMQManager Exception: Message {0} - Stack Trace{1}", ex.Message, ex.StackTrace));
            }
        }
        #endregion 
        #region Peek
        private void messageQueue_PeekCompleted(object sender, PeekCompletedEventArgs e)
        {
            try
            {

                MessageQueue mq = (MessageQueue)sender;
               
                if (mq != null)
                {
                    try
                    {
                        Message message = null;
                        try
                        {
                            message = mq.EndPeek(e.AsyncResult);
                            if (message != null)
                            {

                                var BinaryData = message.Body as byte[];
                                if (BinaryData != null)
                                {

                                    PacketDataProcessor PacketProcessor = new PacketDataProcessor(BinaryData);
                                    PacketProcessor.ProcessData();

                                    //Notify Clients through SignalR
                                    
                                        var hubContext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<SignalR.TCPServerHub>();
                                        hubContext.Clients.All.notifyStatus(PacketProcessor.UnitRegNo, PacketProcessor.UnitStatus);
                                   

                                    // Save Transaction into Transaction Database 
                                    TransactionDTO transaction = new TransactionDTO()
                                    {
                                        
                                        RegNo=PacketProcessor.UnitRegNo,
                                        EntryDate=DateTime.Now,
                                        Status=PacketProcessor.UnitStatus
                           
                                    };
                                    WebApiConsumer.SendTransaction(transaction);
                                }
                                
                                m_MQ.Receive();

                            }
                        }
                        catch (Exception ex)
                        {
                            // log the exception to the file
                            Log.LogToFile(string.Format("MSMQManager Exception: Message {0} - Stack Trace{1}",ex.Message,ex.StackTrace)); 
                            //TODO: for better Exception handling we need to implement Execution Context 
                        }

                    }
                    finally
                    {
                        if (isActive)
                        {
                            mq.BeginPeek();
                        }
                    }
                }
                return;
            }
            catch (Exception exc)
            {
                // log the exception to the file
                Log.LogToFile(string.Format("MSMQManager Exception: Message {0} - Stack Trace{1}", exc.Message, exc.StackTrace));
                //TODO: for better Exception handling we need to implement Execution Context 
            }
        }
        #endregion 
    }
}
