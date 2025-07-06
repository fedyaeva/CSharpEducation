namespace EmployeeAccountSystem;

/// <summary>
/// Исключение при не найденном сотруднике
/// </summary>
public class EmployeeNotFoundException : Exception
{
    public EmployeeNotFoundException(string message) : base(message) { }
}