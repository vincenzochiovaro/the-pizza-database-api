using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using ThePizzaDatabaseAPI.Activities;
using ThePizzaDatabaseAPI.Models.Messages;

namespace ThePizzaDatabaseAPI.Orchestrators;

public class ReminderOrchestrator
{
    [Function(nameof(ReminderOrchestrator))]
    public async Task Run([OrchestrationTrigger] TaskOrchestrationContext context)
    {
        var schedule = context.GetInput<ReminderScheduleMessage>();
        if (schedule == null)
        {
            return;
        }

        await context.CallActivityAsync(
            nameof(SendEmailActivity),
            new SendEmailMessage
            {
                RecipientEmail = schedule.Email,
                Reminders = schedule.Reminders,
                Round = ReminderRound.First
            });

        await context.CreateTimer(schedule.Reminders.SecondRoundTime, CancellationToken.None);

        await context.CallActivityAsync(
            nameof(SendEmailActivity),
            new SendEmailMessage
            {
                RecipientEmail = schedule.Email,
                Reminders = schedule.Reminders,
                Round = ReminderRound.Second
            });

        await context.CreateTimer(schedule.Reminders.ThirdRoundTime, CancellationToken.None);

        await context.CallActivityAsync(
            nameof(SendEmailActivity),
            new SendEmailMessage
            {
                RecipientEmail = schedule.Email,
                Reminders = schedule.Reminders,
                Round = ReminderRound.Third
            });
    }
}