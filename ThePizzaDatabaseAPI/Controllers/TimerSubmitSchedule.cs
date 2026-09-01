using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DurableTask.Client;
using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Models.Messages;
using ThePizzaDatabaseAPI.Models.Requests;
using ThePizzaDatabaseAPI.Orchestrators;

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
        if (request is null)
        {
            return new BadRequestObjectResult("Invalid request body.");
        }

        var isUserEmailVerified = true; // todo 
        
        var reminders = _calculateReminderSchedule.CalculateTimings(request.Date, request.Time, request.Preset);

        var emailReminderMsg = new ReminderScheduleMessage()
        {
            Email = request.Email,
            Reminders = reminders,
            Preset = request.Preset,
            Lang = request.Lang,
            PresetData = request.PresetData,
            MixingType = request.MixingType,
        };

        var instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
            nameof(ReminderOrchestrator),
            emailReminderMsg);

        return new ObjectResult(new
        {
            InstanceId = instanceId
        })
        {
            StatusCode = 202
        };
    }
}