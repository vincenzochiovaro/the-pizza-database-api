using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DurableTask.Client;
using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Core.Services;
using ThePizzaDatabaseAPI.Models.Requests;

namespace ThePizzaDatabaseAPI.Controllers;

public class TimerSubmitSchedule
{
    private readonly ILogger<TimerSubmitSchedule> _logger;
    private readonly ICalculateReminderSchedule _calculateReminderSchedule;

    public TimerSubmitSchedule(ILogger<TimerSubmitSchedule> logger,
        ICalculateReminderSchedule calculateReminderSchedule)
    {
        _logger = logger;
        _calculateReminderSchedule = calculateReminderSchedule;
    }

    [Function("TimerSubmitSchedule")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
        [DurableClient] DurableTaskClient client)
    {
        var request = await req.ReadFromJsonAsync<TimerSubmitScheduleRequest>();
        // todo: validation: e.g remove whiteSpaces
        if (request is null)
        {
            return new BadRequestObjectResult("Invalid request body.");
        }

        var instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
            "ReminderOrchestrator",
            request);
        
        var reminders = _calculateReminderSchedule.CalculateTimings(request.Date, request.Time, request.Preset);
        // todo Implement service


        // TODO START - create Service x to retrieve all email information based on the current stage (durable function)
        // todo: add all env variables to cloud
        var recipientEmail = request.Email;
        var recipetSubject = "subject";
        var recipientBody = "recBody";
        var senderName = Environment.GetEnvironmentVariable("BrevoApi:SenderName");
        var senderEmail = Environment.GetEnvironmentVariable("BrevoApi:SenderEmail");
        if (string.IsNullOrWhiteSpace(senderName) ||
            string.IsNullOrWhiteSpace(senderEmail))
        {
            throw new InvalidOperationException("Email sender configuration is missing.");
        }

        EmailSender.SendEmail(
            senderName,
            senderEmail,
            recipientEmail,
            recipetSubject,
            recipientBody);
        // TODO END


        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}