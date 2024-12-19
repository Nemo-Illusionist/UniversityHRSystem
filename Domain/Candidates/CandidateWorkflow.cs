using Domain.Companies;

namespace Domain.Candidates;

public sealed class CandidateWorkflow
{
    private CandidateWorkflow(IReadOnlyCollection<CandidateWorkflowStep> steps)
    {
        ArgumentNullException.ThrowIfNull(steps);

        Steps = steps;
    }

    public IReadOnlyCollection<CandidateWorkflowStep> Steps { get; private set; }
    internal CandidateStatus Status => GetStatus();

    internal static CandidateWorkflow Create(IReadOnlyCollection<CandidateWorkflowStep> steps)
        => new(steps);

    internal void Approve(User user, string feedback)
    {
        GetCurrentInProcessingStep().Approve(user, feedback);
    }

    internal void Reject(User user, string feedback)
    {
        GetCurrentInProcessingStep().Reject(user, feedback);
    }

    internal void Restart()
    {
        foreach (var step in Steps)
        {
            step.Restart();
        }
    }

    private CandidateWorkflowStep GetCurrentInProcessingStep()
    {
        var status = GetStatus();

        if (status == CandidateStatus.Approved)
        {
            throw new Exception(""); // todo: добавить нормальное сообщение
        }

        if (status == CandidateStatus.Rejected)
        {
            throw new Exception(""); // todo: добавить нормальное сообщение
        }

        return Steps
            .Where(step => step.Status == CandidateStatus.InProcessing)
            .OrderBy(step => step.Number)
            .First();
    }

    private CandidateStatus GetStatus()
    {
        if (Steps.All(step => step.Status == CandidateStatus.Approved))
        {
            return CandidateStatus.Approved;
        }

        if (Steps.Any(step => step.Status == CandidateStatus.Rejected))
        {
            return CandidateStatus.Rejected;
        }

        return CandidateStatus.InProcessing;
    }
}
