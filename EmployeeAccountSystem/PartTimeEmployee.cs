using System;

namespace EmployeeAccountSystem;

/// <summary>
/// Сотрудник с частичной занятостью (зарплата рассчитывается по часам).
/// </summary>
public class PartTimeEmployee : Employee
{
    /// <summary>
    /// Отработанные часы работы.
    /// </summary>
    public decimal HoursWorked { get; set; }

    /// <summary>
    /// Создает сотрудника с частичной занятостью.
    /// </summary>
    /// <param name="name">Имя.</param>
    /// <param name="baseSalary">Часовая ставка.</param>
    /// <param name="hoursWorked">Отработанные часы работы.</param>
    public PartTimeEmployee(string name, decimal baseSalary, decimal hoursWorked)
    {
        this.Name = name;
        this.BaseSalary = baseSalary;
        this.HoursWorked = hoursWorked;
        this.Id = Guid.NewGuid();
    }

    public override decimal CalculateSalary()
    {
        return BaseSalary * HoursWorked;
    }
}