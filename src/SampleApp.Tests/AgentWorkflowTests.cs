using Xunit;
using SampleApp;

namespace SampleApp.Tests;

public class AgentWorkflowTests
{
    [Fact]
    public void Workflow_InitializesThreeAgents()
    {
        // Arrange & Act
        var workflow = new Workflow();

        // Assert
        Assert.NotNull(workflow.Architect);
        Assert.NotNull(workflow.Developer);
        Assert.NotNull(workflow.QA);
    }

    [Fact]
    public void Architect_HasCorrectProperties()
    {
        // Arrange & Act
        var workflow = new Workflow();

        // Assert
        Assert.Equal("Architect", workflow.Architect.Name);
        Assert.Equal("Planner", workflow.Architect.Role);
        Assert.Contains("search", workflow.Architect.Tools);
        Assert.Contains("read", workflow.Architect.Tools);
        Assert.Equal(2, workflow.Architect.Tools.Length);
    }

    [Fact]
    public void Developer_HasCorrectProperties()
    {
        // Arrange & Act
        var workflow = new Workflow();

        // Assert
        Assert.Equal("Developer", workflow.Developer.Name);
        Assert.Equal("Implementer", workflow.Developer.Role);
        Assert.Contains("search", workflow.Developer.Tools);
        Assert.Contains("read", workflow.Developer.Tools);
        Assert.Contains("edit", workflow.Developer.Tools);
        Assert.Equal(3, workflow.Developer.Tools.Length);
    }

    [Fact]
    public void QA_HasCorrectProperties()
    {
        // Arrange & Act
        var workflow = new Workflow();

        // Assert
        Assert.Equal("QA", workflow.QA.Name);
        Assert.Equal("Validator", workflow.QA.Role);
        Assert.Contains("search", workflow.QA.Tools);
        Assert.Contains("read", workflow.QA.Tools);
        Assert.Contains("edit", workflow.QA.Tools);
        Assert.Equal(3, workflow.QA.Tools.Length);
    }

    [Fact]
    public void ExecuteWorkflow_WithValidRequirement_ReturnsSuccess()
    {
        // Arrange
        var workflow = new Workflow();
        var requirement = "Add a new feature";

        // Act
        var result = workflow.ExecuteWorkflow(requirement);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("Workflow completed successfully.", result.Message);
        Assert.NotEmpty(result.ArchitectPlan);
        Assert.NotEmpty(result.DeveloperImplementation);
        Assert.NotEmpty(result.QAValidation);
    }

    [Fact]
    public void ExecuteWorkflow_WithEmptyRequirement_ReturnsFail()
    {
        // Arrange
        var workflow = new Workflow();

        // Act
        var result = workflow.ExecuteWorkflow(string.Empty);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Requirement cannot be empty.", result.Message);
    }

    [Fact]
    public void ExecuteWorkflow_WithWhitespaceRequirement_ReturnsFail()
    {
        // Arrange
        var workflow = new Workflow();

        // Act
        var result = workflow.ExecuteWorkflow("   ");

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Requirement cannot be empty.", result.Message);
    }

    [Fact]
    public void ExecuteWorkflow_IncludesAllThreeStages()
    {
        // Arrange
        var workflow = new Workflow();
        var requirement = "Implement feature X";

        // Act
        var result = workflow.ExecuteWorkflow(requirement);

        // Assert - Verify all three stages are present in output
        Assert.Contains("Architect", result.ArchitectPlan);
        Assert.Contains("Developer", result.DeveloperImplementation);
        Assert.Contains("QA", result.QAValidation);
        Assert.Contains(requirement, result.ArchitectPlan);
        Assert.Contains(requirement, result.DeveloperImplementation);
    }

    [Fact]
    public void Agent_ToString_FormatsCorrectly()
    {
        // Arrange
        var agent = new Agent
        {
            Name = "TestAgent",
            Role = "Tester",
            Tools = ["tool1", "tool2"]
        };

        // Act
        var output = agent.ToString();

        // Assert
        Assert.Contains("TestAgent", output);
        Assert.Contains("Tester", output);
        Assert.Contains("tool1", output);
        Assert.Contains("tool2", output);
    }

    [Fact]
    public void ExecuteWorkflow_WithNullRequirement_ReturnsFail()
    {
        // Arrange
        var workflow = new Workflow();

        // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type
        var result = workflow.ExecuteWorkflow(null!);
#pragma warning restore CS8625

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Requirement cannot be empty.", result.Message);
    }

    [Fact]
    public void ExecuteWorkflow_WithSpecialCharacters_ReturnsSuccess()
    {
        // Arrange
        var workflow = new Workflow();
        var requirement = "Add @feature with #tags & special chars!";

        // Act
        var result = workflow.ExecuteWorkflow(requirement);

        // Assert
        Assert.True(result.Success);
        Assert.Contains(requirement, result.ArchitectPlan);
        Assert.Contains(requirement, result.DeveloperImplementation);
        Assert.Contains(requirement, result.QAValidation);
    }

    [Fact]
    public void ExecuteWorkflow_WithVeryLongRequirement_ReturnsSuccess()
    {
        // Arrange
        var workflow = new Workflow();
        var requirement = string.Concat(Enumerable.Repeat("Long requirement text. ", 50)); // ~1100 chars

        // Act
        var result = workflow.ExecuteWorkflow(requirement);

        // Assert
        Assert.True(result.Success);
        Assert.Contains(requirement, result.ArchitectPlan);
        Assert.Equal("Workflow completed successfully.", result.Message);
    }

    [Fact]
    public void ExecuteWorkflow_WithSingleCharRequirement_ReturnsSuccess()
    {
        // Arrange
        var workflow = new Workflow();

        // Act
        var result = workflow.ExecuteWorkflow("X");

        // Assert
        Assert.True(result.Success);
        Assert.Contains("X", result.ArchitectPlan);
    }

    [Fact]
    public void Agent_WithEmptyToolsArray_ToStringFormatsCorrectly()
    {
        // Arrange
        var agent = new Agent
        {
            Name = "NoTools",
            Role = "Observer",
            Tools = []
        };

        // Act
        var output = agent.ToString();

        // Assert
        Assert.Contains("NoTools", output);
        Assert.Contains("Observer", output);
        Assert.DoesNotContain(", ", output); // No tools, so no comma-space
    }

    [Fact]
    public void WorkflowResult_DefaultState_IsInitialized()
    {
        // Arrange & Act
        var result = new WorkflowResult();

        // Assert
        Assert.False(result.Success);
        Assert.Equal(string.Empty, result.Message);
        Assert.Equal(string.Empty, result.ArchitectPlan);
        Assert.Equal(string.Empty, result.DeveloperImplementation);
        Assert.Equal(string.Empty, result.QAValidation);
    }

    [Fact]
    public void ExecuteWorkflow_RequirementPreservedInAllStages()
    {
        // Arrange
        var workflow = new Workflow();
        var requirement = "Unique requirement identifier 12345";

        // Act
        var result = workflow.ExecuteWorkflow(requirement);

        // Assert - Requirement should appear in all three stages for traceability
        Assert.Contains(requirement, result.ArchitectPlan);
        Assert.Contains(requirement, result.DeveloperImplementation);
        Assert.Contains(requirement, result.QAValidation);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  \t  ")]
    [InlineData("\n")]
    public void ExecuteWorkflow_WithVariousWhitespaceInputs_ReturnsFail(string requirement)
    {
        // Arrange
        var workflow = new Workflow();

        // Act
        var result = workflow.ExecuteWorkflow(requirement);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Requirement cannot be empty.", result.Message);
    }
}
