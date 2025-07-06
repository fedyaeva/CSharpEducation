namespace EmployeeAccountSystem;

/// <summary>
/// Исключение при уже существующем сотруднике
/// </summary>
public class EmployeeExistsException : Exception
{
    public EmployeeExistsException(string message) : base(message) { }
}