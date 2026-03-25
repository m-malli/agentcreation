# agentcreation

This is my first agent creation repo featuring GitHub Copilot custom agents and a .NET sample application demonstrating the Architect → Developer → QA workflow.

## Project Structure

```
.github/
  agents/
    architect.agent.md        # Plans and specifies changes
    developer.agent.md        # Implements the plans (you are here in Developer mode)
    qa.agent.md               # Validates quality and tests
src/
  SampleApp/
    SampleApp.csproj          # .NET 8 Console app project
    Program.cs                # Interactive CLI entry point
    AgentWorkflow.cs          # Workflow model (Agent, Workflow, WorkflowResult classes)
  SampleApp.Tests/
    SampleApp.Tests.csproj    # xUnit test project
    AgentWorkflowTests.cs     # Unit tests for workflow logic
```

## Sample Console App

A .NET 8 console application demonstrating the three-agent workflow pattern. The app models Architect (Planner), Developer (Implementer), and QA (Validator) agents, then simulates a workflow execution based on user input.

### Prerequisites

- .NET 8.0 SDK or later

### Build

```bash
dotnet build src/SampleApp/SampleApp.csproj
```

### Run

```bash
dotnet run --project src/SampleApp/SampleApp.csproj
```

The app will present an interactive prompt. Enter a requirement (e.g., "Add authentication") and the workflow will simulate execution through all three stages.

### Run Tests

```bash
dotnet test src/SampleApp.Tests/SampleApp.Tests.csproj
```

Build and test in Release mode:

```bash
dotnet build --configuration Release src/SampleApp/SampleApp.csproj
dotnet test --configuration Release src/SampleApp.Tests/SampleApp.Tests.csproj
```

## Workflow Integration

This sample demonstrates:
- **Agent separation of concerns**: Architect plans, Developer implements, QA validates
- **Tool assignments**: Each agent has specific tools available
- **Workflow execution**: Input flows through all three stages
- **Unit test coverage**: Validates workflow behavior and edge cases
