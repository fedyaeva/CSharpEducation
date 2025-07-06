namespace EmployeeAccountSystem;

/// <summary>
/// Сотрудник с полной занятостью (зарплата фиксированная)
/// </summary>
public class FullTimeEmployee : Employee
{
    public FullTimeEmployee(string name, decimal salary)
    {
        this.name = name;
        this.baseSalary = salary;
        this.id = Guid.NewGuid();
    }

    public override decimal CalculateSalary()
    {
        return baseSalary;
    }
}