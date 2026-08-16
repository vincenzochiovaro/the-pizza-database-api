using brevo_csharp.Client;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThePizzaDatabaseAPI.Core.Services;
using ThePizzaDatabaseAPI.Models.Requests;

namespace ThePizzaDatabaseAPI.Controllers;

public class TimerSubmitSchedule
{
    private readonly ILogger<TimerSubmitSchedule> _logger;

    public TimerSubmitSchedule(ILogger<TimerSubmitSchedule> logger)
    {
        _logger = logger;
    }

    [Function("SubmitScheduleRequest")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
    {
        
        var request = await req.ReadFromJsonAsync<TimerSubmitScheduleRequest>();
        // todo: validation like: e.g remove whiteSpaces
        if (request is null)
        {
            return new BadRequestObjectResult("Invalid request body.");
        }
        
        var recipientEmail = request.Email;
        var recipetSubject = "subject"; // from service x
        var recipientBody = "recBody"; 
        
        var senderName = Environment.GetEnvironmentVariable("BrevoApi:SenderName");
        var senderEmail = Environment.GetEnvironmentVariable("BrevoApi:SenderEmail");
        if (string.IsNullOrWhiteSpace(senderName) ||
            string.IsNullOrWhiteSpace(senderEmail))
        {
            throw new InvalidOperationException("Brevo sender configuration is missing.");
        }
        
        EmailSender.SendEmail(
            senderName,
            senderEmail,
            recipientEmail,
            recipetSubject,
            recipientBody);
        

        
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
        
    }

}