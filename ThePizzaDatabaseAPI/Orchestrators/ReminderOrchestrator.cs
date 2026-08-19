using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

namespace ThePizzaDatabaseAPI.Orchestrators;

public class ReminderOrchestrator
{
    [Function(nameof(ReminderOrchestrator))]
    public void Run([OrchestrationTrigger] TaskOrchestrationContext context)
    {
        Console.WriteLine("ReminderOrchestrator started");
    }
}