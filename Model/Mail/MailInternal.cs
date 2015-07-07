using Model.Base;
using System.Collections.Generic;

namespace Model.Mail
{
    public class MailInternal : RequestBase
    {
        public string Sender { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
        public List<string> To { get; set; }
        public string From { get; set; }
    }
}
