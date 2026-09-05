using Microsoft.Azure.Functions.Worker;
using ThePizzaDatabaseAPI.Core.Enums;
using ThePizzaDatabaseAPI.Core.Interfaces;
using ThePizzaDatabaseAPI.Core.Services;
using ThePizzaDatabaseAPI.Models.Messages;

namespace ThePizzaDatabaseAPI.Activities;

public class SendEmailActivity
{
    private readonly IReminderMessageRepository _reminderMessageRepository;
    private readonly ReminderEmailTemplate _reminderEmailTemplate;

    public SendEmailActivity(
        IReminderMessageRepository reminderMessageRepository,
        ReminderEmailTemplate reminderEmailTemplate)
    {
        _reminderMessageRepository = reminderMessageRepository;
        _reminderEmailTemplate = reminderEmailTemplate;
    }

    [Function("SendEmailActivity")]
    public async Task Run([ActivityTrigger] SendEmailMessage emailMessage)
    {
        var reminderDetails = await _reminderMessageRepository.GetByPresetAsync(
            emailMessage.Preset.ToString());

        if (reminderDetails is null)
        {
            throw new InvalidOperationException(
                $"Reminder details were not found for preset '{emailMessage.Preset}'.");
        }

        var reminderRound = emailMessage.Round switch
        {
            ReminderRound.First => reminderDetails.Rounds.Round1,
            ReminderRound.Second => reminderDetails.Rounds.Round2,
            ReminderRound.Third => reminderDetails.Rounds.Round3,
            _ => throw new ArgumentOutOfRangeException(nameof(emailMessage.Round))
        };

        var reminderLanguage = emailMessage.Lang == Language.It
            ? reminderRound.It
            : reminderRound.En;

        var reminderMessage = emailMessage.MixingType switch
        {
            MixingType.Hands => reminderLanguage.Hands,
            MixingType.Planetary => reminderLanguage.Planetary,
            MixingType.Spiral => reminderLanguage.Spiral,
            _ => throw new ArgumentOutOfRangeException(nameof(emailMessage.MixingType))
        };

        var emailContent = _reminderEmailTemplate.Create(
            emailMessage.PresetData,
            emailMessage.Preset,
            emailMessage.MixingType,
            emailMessage.Round,
            reminderMessage,
            emailMessage.Lang);

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
            emailMessage.RecipientEmail,
            emailContent.Subject,
            emailContent.Body);
    }
}