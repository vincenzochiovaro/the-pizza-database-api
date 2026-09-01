using Microsoft.Azure.Functions.Worker;
using ThePizzaDatabaseAPI.Core.Services;
using ThePizzaDatabaseAPI.Models.Messages;
using ThePizzaDatabaseAPI.Models.Requests;

namespace ThePizzaDatabaseAPI.Activities;

public class SendEmailActivity
{
    [Function("SendEmailActivity")]
    public void Run([ActivityTrigger] SendEmailMessage emailMessage)
    {
        var recipientEmail = emailMessage.RecipientEmail;
        var mixingType = emailMessage.MixingType;
        var presetSelected = emailMessage.Preset;
        var lang = emailMessage.Lang;
        var presetData = emailMessage.PresetData;
        
        var mockReminderDetails =
            "It's time to mix the flour, water, salt and yeast and start preparing your dough.";

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
                Console.WriteLine("Sending email.. --FIRST ROUND");

                EmailSender.SendEmail(
                    senderName,
                    senderEmail,
                    recipientEmail,
                    "Your pizza dough reminder",
                    GetEmailBody(
                        presetData,
                        lang.ToString(),
                        presetSelected.ToString(),
                        mixingType.ToString(),
                        mockReminderDetails));

                break;

            case ReminderRound.Second:
                Console.WriteLine("Sending email.. --SECOND ROUND");

                EmailSender.SendEmail(
                    senderName,
                    senderEmail,
                    recipientEmail,
                    "Your pizza dough reminder",
                    GetEmailBody(
                        presetData,
                        lang.ToString(),
                        presetSelected.ToString(),
                        mixingType.ToString(),
                        mockReminderDetails));

                break;

            case ReminderRound.Third:
                Console.WriteLine("Sending email.. --THIRD ROUND");

                EmailSender.SendEmail(
                    senderName,
                    senderEmail,
                    recipientEmail,
                    "Your pizza dough reminder",
                    GetEmailBody(
                        presetData,
                        lang.ToString(),
                        presetSelected.ToString(),
                        mixingType.ToString(),
                        mockReminderDetails));

                break;
        }
    }

    private string GetEmailBody(
        DoughIngredients presetData,
        string lang,
        string presetSelected,
        string mixingType,
        string reminderDetails)
    {
        var isItalian = lang.Equals(
            "it",
            StringComparison.OrdinalIgnoreCase);

        var text = isItalian
            ? new EmailText
            {
                DoughFor = "Per il tuo impasto",
                Technique = "Tecnica",
                Reminder = "Promemoria",
                Ingredients = "Ingredienti",
                SecondDayIngredients = "Ingredienti per il giorno successivo",
                Water = "Acqua",
                Flour = "Farina",
                Salt = "Sale",
                Yeast = "Lievito",
                Grams = "g",
                Footer = "Buona pizza da Aliper"
            }
            : new EmailText
            {
                DoughFor = "For your",
                Technique = "Technique",
                Reminder = "Reminder",
                Ingredients = "Ingredients",
                SecondDayIngredients = "Ingredients for the following day",
                Water = "Water",
                Flour = "Flour",
                Salt = "Salt",
                Yeast = "Yeast",
                Grams = "g",
                Footer = "Happy pizza making from Aliper"
            };

        var day2Ingredients = "";

        if (presetData.WaterDay2.HasValue ||
            presetData.FlourDay2.HasValue ||
            presetData.SaltDay2.HasValue)
        {
            day2Ingredients = $$"""
                <div style="margin-top: 24px;">

                    <h2 style="
                        margin: 0 0 14px 0;
                        font-family: Arial, Helvetica, sans-serif;
                        font-size: 20px;
                        line-height: 28px;
                        color: #222222;
                    ">
                        {{text.SecondDayIngredients}}
                    </h2>

                    <table
                        width="100%"
                        cellpadding="0"
                        cellspacing="0"
                        border="0"
                        style="
                            border-collapse: collapse;
                            background-color: #f7f5f1;
                            border-radius: 10px;
                        "
                    >

                        <tr>
                            <td style="
                                padding: 16px;
                                font-family: Arial, Helvetica, sans-serif;
                                font-size: 15px;
                                color: #555555;
                            ">
                                {{text.Water}}
                            </td>

                            <td align="right" style="
                                padding: 16px;
                                font-family: Arial, Helvetica, sans-serif;
                                font-size: 16px;
                                font-weight: bold;
                                color: #222222;
                            ">
                                {{presetData.WaterDay2}}{{text.Grams}}
                            </td>
                        </tr>

                        <tr>
                            <td style="
                                padding: 16px;
                                border-top: 1px solid #e5e2dd;
                                font-family: Arial, Helvetica, sans-serif;
                                font-size: 15px;
                                color: #555555;
                            ">
                                {{text.Flour}}
                            </td>

                            <td align="right" style="
                                padding: 16px;
                                border-top: 1px solid #e5e2dd;
                                font-family: Arial, Helvetica, sans-serif;
                                font-size: 16px;
                                font-weight: bold;
                                color: #222222;
                            ">
                                {{presetData.FlourDay2}}{{text.Grams}}
                            </td>
                        </tr>

                        <tr>
                            <td style="
                                padding: 16px;
                                border-top: 1px solid #e5e2dd;
                                font-family: Arial, Helvetica, sans-serif;
                                font-size: 15px;
                                color: #555555;
                            ">
                                {{text.Salt}}
                            </td>

                            <td align="right" style="
                                padding: 16px;
                                border-top: 1px solid #e5e2dd;
                                font-family: Arial, Helvetica, sans-serif;
                                font-size: 16px;
                                font-weight: bold;
                                color: #222222;
                            ">
                                {{presetData.SaltDay2}}{{text.Grams}}
                            </td>
                        </tr>

                    </table>

                </div>
                """;
        }

        return $$"""
            <!DOCTYPE html>
            <html>

            <head>
                <meta charset="UTF-8">
                <meta name="viewport" content="width=device-width, initial-scale=1.0">
                <title>{{text.Reminder}}</title>
            </head>

            <body style="
                margin: 0;
                padding: 0;
                background-color: #f3f1ed;
            ">

                <table
                    width="100%"
                    cellpadding="0"
                    cellspacing="0"
                    border="0"
                    style="
                        background-color: #f3f1ed;
                        padding: 30px 0;
                    "
                >

                    <tr>
                        <td align="center">

                            <table
                                width="600"
                                cellpadding="0"
                                cellspacing="0"
                                border="0"
                                style="
                                    max-width: 600px;
                                    width: 100%;
                                    background-color: #ffffff;
                                    border-radius: 14px;
                                    overflow: hidden;
                                "
                            >

                                <!-- Header -->

                                <tr>
                                    <td style="
                                        padding: 28px 32px;
                                        background-color: #222222;
                                    ">

                                        <div style="
                                            font-family: Arial, Helvetica, sans-serif;
                                            font-size: 24px;
                                            font-weight: bold;
                                            letter-spacing: 1px;
                                            color: #ffffff;
                                        ">
                                            ALIPER
                                        </div>

                                        <div style="
                                            margin-top: 5px;
                                            font-family: Arial, Helvetica, sans-serif;
                                            font-size: 12px;
                                            letter-spacing: 1.5px;
                                            color: #cccccc;
                                            text-transform: uppercase;
                                        ">
                                            Pizza made simple
                                        </div>

                                    </td>
                                </tr>

                                <!-- Introduction -->

                                <tr>
                                    <td style="
                                        padding: 36px 32px 20px 32px;
                                    ">

                                        <div style="
                                            font-family: Arial, Helvetica, sans-serif;
                                            font-size: 13px;
                                            font-weight: bold;
                                            letter-spacing: 1.5px;
                                            color: #a05a2c;
                                            text-transform: uppercase;
                                        ">
                                            {{text.Reminder}}
                                        </div>

                                        <h1 style="
                                            margin: 10px 0 8px 0;
                                            font-family: Arial, Helvetica, sans-serif;
                                            font-size: 30px;
                                            line-height: 38px;
                                            color: #222222;
                                        ">
                                            {{text.DoughFor}}
                                            {{presetSelected}}
                                        </h1>

                                        <div style="
                                            font-family: Arial, Helvetica, sans-serif;
                                            font-size: 16px;
                                            line-height: 26px;
                                            color: #666666;
                                        ">
                                            {{text.Technique}}:

                                            <strong style="
                                                color: #222222;
                                            ">
                                                {{mixingType}}
                                            </strong>
                                        </div>

                                        <!-- Reminder message -->

                                        <div style="
                                            margin-top: 22px;
                                            padding: 18px 20px;
                                            background-color: #f7f5f1;
                                            border-left: 4px solid #a05a2c;
                                            border-radius: 8px;
                                            font-family: Arial, Helvetica, sans-serif;
                                            font-size: 16px;
                                            line-height: 25px;
                                            color: #555555;
                                        ">
                                            {{reminderDetails}}
                                        </div>

                                    </td>
                                </tr>

                                <!-- Ingredients -->

                                <tr>
                                    <td style="
                                        padding: 10px 32px 0 32px;
                                    ">

                                        <h2 style="
                                            margin: 0 0 14px 0;
                                            font-family: Arial, Helvetica, sans-serif;
                                            font-size: 20px;
                                            line-height: 28px;
                                            color: #222222;
                                        ">
                                            {{text.Ingredients}}
                                        </h2>

                                        <table
                                            width="100%"
                                            cellpadding="0"
                                            cellspacing="0"
                                            border="0"
                                            style="
                                                border-collapse: collapse;
                                                background-color: #f7f5f1;
                                                border-radius: 10px;
                                            "
                                        >

                                            <!-- Water -->

                                            <tr>
                                                <td style="
                                                    padding: 16px;
                                                    font-family: Arial, Helvetica, sans-serif;
                                                    font-size: 15px;
                                                    color: #555555;
                                                ">
                                                    {{text.Water}}
                                                </td>

                                                <td align="right" style="
                                                    padding: 16px;
                                                    font-family: Arial, Helvetica, sans-serif;
                                                    font-size: 16px;
                                                    font-weight: bold;
                                                    color: #222222;
                                                ">
                                                    {{presetData.Water}}{{text.Grams}}
                                                </td>
                                            </tr>

                                            <!-- Flour -->

                                            <tr>
                                                <td style="
                                                    padding: 16px;
                                                    border-top: 1px solid #e5e2dd;
                                                    font-family: Arial, Helvetica, sans-serif;
                                                    font-size: 15px;
                                                    color: #555555;
                                                ">
                                                    {{text.Flour}}
                                                </td>

                                                <td align="right" style="
                                                    padding: 16px;
                                                    border-top: 1px solid #e5e2dd;
                                                    font-family: Arial, Helvetica, sans-serif;
                                                    font-size: 16px;
                                                    font-weight: bold;
                                                    color: #222222;
                                                ">
                                                    {{presetData.Flour}}{{text.Grams}}
                                                </td>
                                            </tr>

                                            <!-- Salt -->

                                            <tr>
                                                <td style="
                                                    padding: 16px;
                                                    border-top: 1px solid #e5e2dd;
                                                    font-family: Arial, Helvetica, sans-serif;
                                                    font-size: 15px;
                                                    color: #555555;
                                                ">
                                                    {{text.Salt}}
                                                </td>

                                                <td align="right" style="
                                                    padding: 16px;
                                                    border-top: 1px solid #e5e2dd;
                                                    font-family: Arial, Helvetica, sans-serif;
                                                    font-size: 16px;
                                                    font-weight: bold;
                                                    color: #222222;
                                                ">
                                                    {{presetData.Salt}}{{text.Grams}}
                                                </td>
                                            </tr>

                                            <!-- Yeast -->

                                            <tr>
                                                <td style="
                                                    padding: 16px;
                                                    border-top: 1px solid #e5e2dd;
                                                    font-family: Arial, Helvetica, sans-serif;
                                                    font-size: 15px;
                                                    color: #555555;
                                                ">
                                                    {{text.Yeast}}
                                                </td>

                                                <td align="right" style="
                                                    padding: 16px;
                                                    border-top: 1px solid #e5e2dd;
                                                    font-family: Arial, Helvetica, sans-serif;
                                                    font-size: 16px;
                                                    font-weight: bold;
                                                    color: #222222;
                                                ">
                                                    {{presetData.Yeast}}{{text.Grams}}
                                                </td>
                                            </tr>

                                        </table>

                                        {{day2Ingredients}}

                                    </td>
                                </tr>

                                <!-- Footer -->

                                <tr>
                                    <td style="
                                        padding: 30px 32px;
                                        margin-top: 30px;
                                        background-color: #f7f5f1;
                                        text-align: center;
                                    ">

                                        <div style="
                                            font-family: Arial, Helvetica, sans-serif;
                                            font-size: 14px;
                                            color: #777777;
                                        ">
                                            {{text.Footer}}
                                        </div>

                                        <div style="
                                            margin-top: 8px;
                                            font-family: Arial, Helvetica, sans-serif;
                                            font-size: 12px;
                                            color: #aaaaaa;
                                        ">
                                            Aliper · The Pizza Database
                                        </div>

                                    </td>
                                </tr>

                            </table>

                        </td>
                    </tr>

                </table>

            </body>
            </html>
            """;
    }

    private class EmailText
    {
        public required string DoughFor { get; set; }
        public required string Technique { get; set; }
        public required string Reminder { get; set; }
        public required string Ingredients { get; set; }
        public required string SecondDayIngredients { get; set; }
        public required string Water { get; set; }
        public required string Flour { get; set; }
        public required string Salt { get; set; }
        public required string Yeast { get; set; }
        public required string Grams { get; set; }
        public required string Footer { get; set; }
    }
}