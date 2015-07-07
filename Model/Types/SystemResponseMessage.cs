namespace Model.Types
{
    public static class SystemResponseMessage
    {
        public static string AuthorizationFailed
        {
            get { return "Authorization token missing or invalid."; }
        }

        public static string RequestAlreadyPending
        {
            get { return "A prior request is already pending approval."; }
        }

        public static string RequestNotValid
        {
            get { return "Not a valid request."; }
        }

        public static string RequestAlreadyApproved
        {
            get { return "Request is already approved."; }
        }

        public static string ProceedingHalted
        {
            get { return "Server refused to further process the request."; }
        }

        public static string NotFound
        {
            get { return "Requested resource is removed or not currently available on the server."; }
        }

        public static string GeneralException
        {
            get { return "General server exception."; }
        }

        public static string NonInheritingRequestBase
        {
            get { return "Request body incomplete or missing authenticating information."; }
        }
    }
}