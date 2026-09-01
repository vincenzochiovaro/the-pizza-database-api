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
                Preset = schedule.Preset,
                MixingType = schedule.MixingType,
                Reminders = schedule.Reminders,
                Lang = schedule.Lang,
                PresetData = schedule.PresetData,
                Round = ReminderRound.First
            });

        await context.CreateTimer(schedule.Reminders.SecondRoundTime, CancellationToken.None);

        await context.CallActivityAsync(
            nameof(SendEmailActivity),
            new SendEmailMessage
            {
                RecipientEmail = schedule.Email,
                Preset = schedule.Preset,
                Lang = schedule.Lang,
                MixingType = schedule.MixingType,
                Reminders = schedule.Reminders,
                PresetData = schedule.PresetData,
                Round = ReminderRound.Second
            });

        await context.CreateTimer(schedule.Reminders.ThirdRoundTime, CancellationToken.None);

        await context.CallActivityAsync(
            nameof(SendEmailActivity),
            new SendEmailMessage
            {
                RecipientEmail = schedule.Email,
                Preset = schedule.Preset,
                Lang = schedule.Lang,
                MixingType = schedule.MixingType,
                Reminders = schedule.Reminders,
                PresetData = schedule.PresetData,
                Round = ReminderRound.Third
            });
    }
}