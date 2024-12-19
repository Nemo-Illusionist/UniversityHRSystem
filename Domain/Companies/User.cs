namespace Domain.Companies;

public sealed class User
{
    private User(Guid id, Guid roleId, string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        Id = id;
        RoleId = roleId;
        Name = name;
    }

    public Guid Id { get; private init; }
    public Guid RoleId { get; private init; }
    public string Name { get; private init; }

    public static User Create(Guid roleId, string name)
        => new(Guid.NewGuid(), roleId, name);
}
