namespace Project.Common.ExternalContracts.Email
{
    public interface IMailSender
    {
        void SendMail(EmailMessage message);
    }
}