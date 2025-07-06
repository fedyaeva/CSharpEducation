namespace EmployeeAccountSystem;

/// <summary>
/// Сотрудник с частичной занятостью (зарплата рассчитывается по часам)
/// </summary>
public class PartTimeEmployee : Employee
{
    public decimal hoursWorked { get; set; }

    public PartTimeEmployee(string name, decimal baseSalary, decimal hoursWorked)
    {
        this.name = name;
        this.baseSalary = baseSalary;
        this.hoursWorked = hoursWorked;
        this.id = Guid.NewGuid();
    }

    public override decimal CalculateSalary()
    {
        return baseSalary * hoursWorked;
    }
}