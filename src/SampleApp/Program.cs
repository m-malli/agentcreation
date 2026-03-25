using SampleApp;

// Initialize the workflow
var workflow = new Workflow();

Console.WriteLine("===========================================");
Console.WriteLine("  GitHub Agent Workflow Demo (Console)");
Console.WriteLine("===========================================\n");

// Display agents and their roles
Console.WriteLine("Available Agents:");
Console.WriteLine($"  1. {workflow.Architect}");
Console.WriteLine($"  2. {workflow.Developer}");
Console.WriteLine($"  3. {workflow.QA}");
Console.WriteLine();

// Interactive loop
while (true)
{
    Console.Write("Enter a requirement (or 'quit' to exit): ");
    var requirement = Console.ReadLine();

    if (requirement?.ToLower() == "quit")
    {
        Console.WriteLine("Exiting. Goodbye!");
        break;
    }

    // Execute the workflow
    var result = workflow.ExecuteWorkflow(requirement ?? string.Empty);

    Console.WriteLine();
    if (result.Success)
    {
        Console.WriteLine("--- Workflow Execution ---");
        Console.WriteLine(result.ArchitectPlan);
        Console.WriteLine(result.DeveloperImplementation);
        Console.WriteLine(result.QAValidation);
        Console.WriteLine($"\nStatus: ✓ {result.Message}");
    }
    else
    {
        Console.WriteLine($"Status: ✗ {result.Message}");
    }

    Console.WriteLine();
}
