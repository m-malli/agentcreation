namespace SampleApp;

/// <summary>
/// Represents a single agent in the workflow with its role and tools.
/// </summary>
public class Agent
{
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string[] Tools { get; set; } = [];

    public override string ToString()
    {
        return $"{Name} ({Role}): {string.Join(", ", Tools)}";
    }
}

/// <summary>
/// Represents the three-stage workflow: Architect -> Developer -> QA.
/// </summary>
public class Workflow
{
    public Agent Architect { get; set; }
    public Agent Developer { get; set; }
    public Agent QA { get; set; }

    public Workflow()
    {
        Architect = new Agent
        {
            Name = "Architect",
            Role = "Planner",
            Tools = ["search", "read"]
        };

        Developer = new Agent
        {
            Name = "Developer",
            Role = "Implementer",
            Tools = ["search", "read", "edit"]
        };

        QA = new Agent
        {
            Name = "QA",
            Role = "Validator",
            Tools = ["search", "read", "edit"]
        };
    }

    /// <summary>
    /// Executes the workflow: Architect plans, Developer implements, QA validates.
    /// </summary>
    public WorkflowResult ExecuteWorkflow(string requirement)
    {
        if (string.IsNullOrWhiteSpace(requirement))
        {
            return new WorkflowResult
            {
                Success = false,
                Message = "Requirement cannot be empty."
            };
        }

        var result = new WorkflowResult { Success = true };

        // Architect stage: Plan
        result.ArchitectPlan = $"[{Architect.Name}] Planning '{requirement}' using tools: {string.Join(", ", Architect.Tools)}";

        // Developer stage: Implement
        result.DeveloperImplementation = $"[{Developer.Name}] Implementing '{requirement}' using tools: {string.Join(", ", Developer.Tools)}";

        // QA stage: Validate
        result.QAValidation = $"[{QA.Name}] Validating implementation using tools: {string.Join(", ", QA.Tools)}";

        result.Message = "Workflow completed successfully.";
        return result;
    }
}

/// <summary>
/// Result of a workflow execution.
/// </summary>
public class WorkflowResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public string ArchitectPlan { get; set; } = string.Empty;
    public string DeveloperImplementation { get; set; } = string.Empty;
    public string QAValidation { get; set; } = string.Empty;
}
