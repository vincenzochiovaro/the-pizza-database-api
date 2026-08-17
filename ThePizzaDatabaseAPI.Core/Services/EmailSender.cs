using brevo_csharp.Api;
using brevo_csharp.Model;

namespace ThePizzaDatabaseAPI.Core.Services;

public class EmailSender
{
    public static void SendEmail(
        string senderName, 
        string senderEmail, 
        string recipientEmail, 
        string recipientSubject, 
        string recipientBody)
    {
        var sender = new SendSmtpEmailSender(senderName, senderEmail);
        var recipient = new SendSmtpEmailTo(recipientEmail);

        var sendSmtpEmail = new SendSmtpEmail(
            sender,
            [recipient]
        )
        {
            Subject = recipientSubject,
            HtmlContent = recipientBody
        };

        var apiInstance = new TransactionalEmailsApi();
        apiInstance.SendTransacEmail(sendSmtpEmail);
    }
}