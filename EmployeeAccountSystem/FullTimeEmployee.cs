using System;

namespace EmployeeAccountSystem;

/// <summary>
/// Сотрудник с полной занятостью (зарплата фиксированная).
/// </summary>
public class FullTimeEmployee : Employee
{
    /// <summary>
    /// Создает сотрудника с полной занятостью.
    /// </summary>
    /// <param name="name">Имя.</param>
    /// <param name="salary">Зарплата.</param>
    public FullTimeEmployee(string name, decimal salary)
    {
        this.Name = name;
        this.BaseSalary = salary;
        this.Id = Guid.NewGuid();
    }
    
    public override decimal CalculateSalary()
    {
        return BaseSalary;
    }
}