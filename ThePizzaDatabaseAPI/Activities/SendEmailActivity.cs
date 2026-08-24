using Microsoft.Azure.Functions.Worker;
using ThePizzaDatabaseAPI.Models.Messages;

namespace ThePizzaDatabaseAPI.Activities;

public class SendEmailActivity
{
    [Function("SendEmailActivity")]

    public void Run([ActivityTrigger] SendEmailMessage emailMessage)
    {
        Console.WriteLine("Activity started");
        // TODO START - create Service x to retrieve all email information based on the current stage (durable function)
        // todo: add all env variables to cloud
        // var recipientEmail = emailMessage.RecipientEmail;
        // var recipetSubject = "subject"; // subject and body will need to be created based on the stage we are.
        // var recipientBody = "recBody";
        // var senderName = Environment.GetEnvironmentVariable("BrevoApi:SenderName");
        // var senderEmail = Environment.GetEnvironmentVariable("BrevoApi:SenderEmail");
        // if (string.IsNullOrWhiteSpace(senderName) ||
        //     string.IsNullOrWhiteSpace(senderEmail))
        // {
        //     throw new InvalidOperationException("Email sender configuration is missing.");
        // }

        Console.WriteLine("HELLO");
        // EmailSender.SendEmail(
        //     senderName,
        //     senderEmail,
        //     recipientEmail,
        //     recipetSubject,
        //     recipientBody);
        // TODO END
    }
}