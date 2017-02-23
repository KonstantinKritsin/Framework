namespace Project.Common.ExternalContracts.Email
{
    public class EmailMessage
    {
        public string To { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Attachment[] Attachments { get; set; }
    }
}