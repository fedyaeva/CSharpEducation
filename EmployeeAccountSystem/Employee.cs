namespace EmployeeAccountSystem;

/// <summary>
/// Сотрудники
/// </summary>
public abstract class Employee
{

    public string name { get; set; }
    public decimal baseSalary  { get; set; }
    
    public Guid id { get; set; }

    protected Employee(Guid id)
    {
        this.id = id;
    }

    protected Employee()
    {
        
    }

    /// <summary>
    /// Расчет зарплаты сотрудника
    /// </summary>
    /// <returns></returns>
    public abstract decimal CalculateSalary();
}