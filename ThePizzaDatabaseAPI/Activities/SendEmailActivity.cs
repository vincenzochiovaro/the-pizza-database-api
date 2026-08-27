using Microsoft.Azure.Functions.Worker;
using ThePizzaDatabaseAPI.Core.Services;
using ThePizzaDatabaseAPI.Models.Messages;

namespace ThePizzaDatabaseAPI.Activities;

public class SendEmailActivity
{
    [Function("SendEmailActivity")]
    public void Run([ActivityTrigger] SendEmailMessage emailMessage)
    {
        // todo
        var recipientEmail = emailMessage.RecipientEmail;
        // var reminderdetails = service layer ->  infrastructure mongocall by preset

        var senderName = Environment.GetEnvironmentVariable("BrevoApi:SenderName");
        var senderEmail = Environment.GetEnvironmentVariable("BrevoApi:SenderEmail");
        if (string.IsNullOrWhiteSpace(senderName) ||
            string.IsNullOrWhiteSpace(senderEmail))
        {
            throw new InvalidOperationException("Email sender configuration is missing.");
        }
        
        switch (emailMessage.Round)
        {
            case ReminderRound.First:
                Console.WriteLine("sendimg email.. --FIRST ROUND");
                EmailSender.SendEmail(
                    senderName,
                    senderEmail,
                    recipientEmail,
                    "todoSubject", // reminderdetails.Rounds.Round1.{En/it}.Subject
                    "todoBody");// reminderdetails.Rounds.Round1.{En/it}.Body
                
                break;
            case ReminderRound.Second:
                Console.WriteLine("sendimg email.. --SECOND ROUND");
                        
                EmailSender.SendEmail(
                    senderName,
                    senderEmail,
                    recipientEmail,
                    "todoSubject",// reminderdetails.Rounds.Round2.{En/it}.Subject
                    "todoBody"); // reminderdetails.Rounds.Round2.{En/it}.Body
                
                break;
            case ReminderRound.Third:
                Console.WriteLine("sendimg email.. --THIRD ROUND");
                        
                EmailSender.SendEmail(
                    senderName,
                    senderEmail,
                    recipientEmail,
                    "todoSubject", // reminderdetails.Rounds.Round3.{En/it}.Subject
                    "todoBody"); // reminderdetails.Rounds.Round3.{En/it}.Body
                
                break;
        }
    }
}