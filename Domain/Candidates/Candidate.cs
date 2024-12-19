using Domain.Companies;

namespace Domain.Candidates;

public sealed class Candidate
{
    public Candidate(Guid id, Guid vacancyId, Guid? referralId, CandidateDocument document, CandidateWorkflow workflow)
    {
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(workflow);

        Id = id;
        VacancyId = vacancyId;
        ReferralId = referralId;
        Workflow = workflow;
        Document = document;
    }

    public Guid Id { get; private init; }
    public Guid VacancyId { get; private init; }
    public Guid? ReferralId { get; private init; }
    public CandidateDocument Document { get; private init; }
    public CandidateWorkflow Workflow { get; private init; }

    internal static Candidate Create(
        Guid vacancyId,
        Guid? referralId,
        CandidateDocument document,
        CandidateWorkflow workflow)
        => new(Guid.NewGuid(), vacancyId, referralId, document, workflow);

    public void Approve(User user, string feedback)
        => Workflow.Approve(user, feedback);

    public void Reject(User user, string feedback)
        => Workflow.Reject(user, feedback);

    public void Restart()
        => Workflow.Restart();
}