using DocAccessApproval.Domain.Exceptions.UserExceptions;
using DocAccessApproval.Domain.SeedWork;

namespace DocAccessApproval.Domain.AggregateModels.UserAggregate;

public class User : AggregateRoot
{
    public User()
    {
        _userRoles = new List<UserRole>();
    }
    public User(Guid id) : this()
    {
        Id = id;
    }
    public User(Guid id, string firstName, string lastName, string email, string userName) : this(id)
    {
        UserException.ThrowIfNullOrEmpty(firstName);
        UserException.ThrowIfNullOrEmpty(lastName);
        UserException.ThrowIfNullOrEmpty(email);
        UserException.ThrowIfNullOrEmpty(userName);


        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UserName = userName;
    }

    public User(string firstName, string lastName, string email, string userName, byte[] passwordSalt, byte[] passwordHash) :
        this(Guid.NewGuid(), firstName, lastName, email, userName)
    {
        SetPasswords(passwordSalt, passwordHash);
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string UserName { get; private set; }
    public byte[] PasswordSalt { get; private set; }
    public byte[] PasswordHash { get; private set; }

    private List<UserRole> _userRoles;
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    public void SetPasswords(byte[] passwordSalt, byte[] passwordHash)
    {
        UserException.ThrowIfNull(passwordSalt);
        UserException.ThrowIfNull(passwordHash);

        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
    }

    public void AddUserRole(Guid roleId)
    {
        if (_userRoles.Any(x => x.RoleId == roleId))
            throw new UserException($"User already has this role");

        _userRoles.Add(new UserRole(Id, roleId));
    }

    public void AddUserRole(Role role)
    {
        AddUserRole(role.Id);
    }

    public void RemoveUserRole(Guid roleId)
    {
        var userRole = _userRoles.FirstOrDefault(x => x.RoleId == roleId);
        UserException.ThrowIfNull(userRole);

        _userRoles.Remove(userRole);
    }
}
