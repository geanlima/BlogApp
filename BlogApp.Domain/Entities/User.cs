namespace BlogApp.Domain.Entities;

public class User
{
    public int Id { get; private set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    protected User() { }

    public User(string username, string email, string passwordHash)
    {
        SetUsername(username);
        SetEmail(email);
        SetPasswordHash(passwordHash);
    }

    public void SetUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("O nome de usuário não pode ser vazio.");
        }
        Username = username;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            throw new ArgumentException("O email deve ser válido.");
        }
        Email = email;
    }

    public void SetPasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new ArgumentException("A senha não pode ser vazia.");
        }

        PasswordHash = passwordHash;
    }
}

