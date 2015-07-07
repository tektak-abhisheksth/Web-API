using Model.Common;
using Model.Types;
using TekTak.iLoop.Kauwa;

namespace TekTak.iLoop.Helper
{
    public static class Extensions
    {
        /// <summary>
        /// Returns Session object as required by JService.
        /// </summary>
        /// <param name="session">Current user's session containing all information.</param>
        /// <returns>Session object.</returns>
        public static Session GetSession(this SystemSession session)
        {
            var userSession = new Session
            {
                SessionToken = session.LoginToken,
                DeviceId = session.DeviceId,
                UserId = session.UserName,
                Replay = true,
                IUserId = session.UserId,
                TransportIp = session.Ip
            };
            return userSession;
        }

        /// <summary>
        /// Returns database status response.
        /// </summary>
        /// <typeparam name="T">The type in context.</typeparam>
        /// <param name="status">Current response status.</param>
        /// <returns>Status Data information.</returns>
        public static StatusData<T> GetStatusData<T>(this DbStatus status)
        {
            var response = new StatusData<T>
            {
                Status = (SystemDbStatus)status.DbStatusCode,
                SubStatus = status.DbSubStatusCode,
                Message = status.DbStatusMsg
            };
            return response;
        }
    }
}
