namespace KoiCareSys.Data.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string? Password { get; set; }

    public string Email { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public Enums.UserStatus Status { get; set; }

    public string? PhoneNumber { get; set; }

    public Enums.UserRole Role { get; set; }

    public virtual ICollection<Pond> Ponds { get; set; } = new List<Pond>();
}
