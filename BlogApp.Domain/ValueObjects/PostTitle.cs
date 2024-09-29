namespace BlogApp.Domain.ValueObjects;

public class PostTitle
{
    public string Value { get; private set; }

    public PostTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("O título não pode ser vazio.");
        }
        if (value.Length > 100)
        {
            throw new ArgumentException("O título não pode ter mais de 100 caracteres.");
        }

        Value = value;
    }

    public override bool Equals(object obj)
    {
        if (obj is PostTitle other)
        {
            return Value == other.Value;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }
}
