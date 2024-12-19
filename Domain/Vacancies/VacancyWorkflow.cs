using Domain.Candidates;

namespace Domain.Vacancies;

public sealed class VacancyWorkflow
{
    private VacancyWorkflow(IReadOnlyCollection<VacancyWorkflowStep> steps)
    {
        ArgumentNullException.ThrowIfNull(steps);

        Steps = steps;
    }

    public IReadOnlyCollection<VacancyWorkflowStep> Steps { get; private init; }

    public static VacancyWorkflow Create(IReadOnlyCollection<VacancyWorkflowStep> steps)
        => new(steps);

    public CandidateWorkflow ToCandidate()
        => CandidateWorkflow.Create(Steps.Select(step => step.ToCandidate()).ToArray());
}
