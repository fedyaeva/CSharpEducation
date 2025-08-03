using System;

namespace EmployeeAccountSystem;

/// <summary>
/// Сотрудники.
/// </summary>
public abstract class Employee
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Зарплата.
    /// </summary>
    public decimal BaseSalary  { get; set; }
    
    /// <summary>
    /// ИД.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Расчет зарплаты сотрудника.
    /// </summary>
    /// <returns>Рассчитанная зарплата.</returns>
    public abstract decimal CalculateSalary();
    
    /// <summary>
    /// Создание сотрудника с заданным ИД.
    /// </summary>
    /// <param name="id">ИД.</param>
    protected Employee(Guid id)
    {
        this.Id = id;
    }

    /// <summary>
    /// Создание сотрудника без параметров.
    /// </summary>
    protected Employee()
    {
        
    }
}