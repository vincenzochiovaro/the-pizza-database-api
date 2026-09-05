using ThePizzaDatabaseAPI.Core.Domains;
using ThePizzaDatabaseAPI.Infrastructure.Models;

namespace ThePizzaDatabaseAPI.Infrastructure.Mappers;

public static class ReminderMessageMapper
{
    public static ReminderMessageDomain ToDomain(ReminderMessageDocument document)
    {
        return new ReminderMessageDomain
        {
            Rounds = new ReminderRoundsDomain
            {
                Round1 = MapRound(document.Rounds.Round1),
                Round2 = MapRound(document.Rounds.Round2),
                Round3 = MapRound(document.Rounds.Round3)
            }
        };
    }

    private static ReminderRoundTranslationDomain MapRound(
        ReminderRoundTranslation round)
    {
        return new ReminderRoundTranslationDomain
        {
            En = MapMessageType(round.En),
            It = MapMessageType(round.It)
        };
    }

    private static ReminderMessageTypeDomain MapMessageType(
        ReminderMessageType messageType)
    {
        return new ReminderMessageTypeDomain
        {
            Hands = MapTranslation(messageType.Hands),
            Planetary = MapTranslation(messageType.Planetary),
            Spiral = MapTranslation(messageType.Spiral)
        };
    }

    private static ReminderMessageTranslationDomain MapTranslation(
        ReminderMessageTranslation translation)
    {
        return new ReminderMessageTranslationDomain
        {
            Subject = translation.Subject,
            Message = translation.Message
        };
    }
}