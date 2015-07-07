using System;
using Thrift.Transport;
using Utility;

namespace TekTak.iLoop
{
    /// <summary>
    /// Provides an object representation of service client that handles the details related to the connection.
    /// </summary>
    public class ServiceClientAdapter : IDisposable
    {
        private readonly TTransport _transport;

        /// <summary>
        /// Initializes a new instance of the ServiceClientAdapter class.
        /// </summary>
        public ServiceClientAdapter()
        {
            _transport = new TSocket(SystemConstants.JHost, SystemConstants.JHostPort);
        }

        /// <summary>
        /// Establishes and opens a physical connection with the service.
        /// </summary>
        /// <returns>Client service objects to invoke RPC calls to the client.</returns>
        public Services Connect()
        {
            try
            {
                _transport.Open();
                return new Services(_transport);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Closes and disposes the connection and releases unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _transport.Close();
        }
    }
}