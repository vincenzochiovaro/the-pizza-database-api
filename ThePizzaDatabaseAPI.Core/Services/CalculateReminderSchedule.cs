using ThePizzaDatabaseAPI.Core.Domains;
using ThePizzaDatabaseAPI.Core.Enums;
using ThePizzaDatabaseAPI.Core.Interfaces;

namespace ThePizzaDatabaseAPI.Core.Services;

public class CalculateReminderSchedule : ICalculateReminderSchedule
{
    public ReminderSchedule CalculateTimings(string date, string time, PizzaPreset preset)
    {
        
        // if preset is Direct:
        //take the date and time (this is the datetime of when the pizza is ready to be baked)
        // we need to create 3 previous steps:
        //8:10h from the date and time selected we want to set the firstRoundTime
        //then after 4h (so 4 hours before the initial time) set seconRoundTime
        // then on the time selected set  The THirdRoundTIme
        
        // if preset is Biga
        // 18h from the date and time selected we want to set the firstRoundTime
        // (likely day after) after those 18h set seconRoundTime 
        // theb on the time selected set the thirdROundTime
        
        // if preset is Express
        // 3h from the date and time we want to set the firstRoundTime
        // 1h after (so 2h before the original time) set secondRount
        // then on the timeselected set the third roundtime
        
        // test edge case like:
        // if biga is selected the gap between now and the time selected must be minimum 20h (give 2h tollerance for ui delay)
        //same for each preset.
        
        // test accurately and strictly the order of each round, obviously firstRound is older than second and second
        // is older than third etc.
        
        var now = DateTime.UtcNow;
        return new ReminderSchedule()
        {
            FirstRoundTime = now,
            SecondRoundTime = now.AddMinutes(1),
            ThirdRoundTime = now.AddMinutes(2)
        };
    }
}

// todo - implement logic. + unit tests

// biga 2 timer would be mix ingredients let it rest for half hour  before making doughballs and make it
// rest for the next 3 hours. odn't worry an email will let you know once you ready to bake!