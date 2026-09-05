namespace ThePizzaDatabaseAPI.Core.Domains;

public class ReminderMessageDomain
{
    public required ReminderRoundsDomain Rounds { get; init; }
}

public class ReminderRoundsDomain
{
    public required ReminderRoundTranslationDomain Round1 { get; set; }
    public required ReminderRoundTranslationDomain Round2 { get; set; }
    public required ReminderRoundTranslationDomain Round3 { get; set; }
}

public class ReminderRoundTranslationDomain
{
    public required ReminderMessageTypeDomain En { get; set; }
    public required ReminderMessageTypeDomain It { get; set; }
}

public class ReminderMessageTypeDomain
{
    public required ReminderMessageTranslationDomain Hands { get; set; }
    public required ReminderMessageTranslationDomain Planetary { get; set; }
    public required ReminderMessageTranslationDomain Spiral { get; set; }
}

public class ReminderMessageTranslationDomain
{
    public required string Subject { get; set; }
    public required string Message { get; set; }
}