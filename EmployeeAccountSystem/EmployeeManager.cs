using System;
using System.Collections.Generic;

namespace EmployeeAccountSystem;

/// <summary>
/// Управление сотрудниками.
/// </summary>
/// <typeparam name="T">Сотрудник.</typeparam>
public class EmployeeManager<T> : IEmployeeManager<T> where T : Employee
{
    /// <summary>
    /// Список сотрудников.
    /// </summary>
    private List<T> employees = new List<T>();

    #region IEmployeeManager
    public void Add(T employee)
    {
        if (employees.Exists(e => e.Name == employee.Name))
            throw new EmployeeExistsException($"Сотрудник с именем '{employee.Name}' уже есть в списке");

        employees.Add(employee);
    }
    
    public T Get(string name)
    {
        var employee = employees.Find(e => e.Name == name);
        if (employee == null)
            throw new EmployeeNotFoundException($"Сотрудник с именем '{name}' не найден");
        return employee;
    }
    
    public T Get(Guid id)
    {
        var employee = employees.Find(e => e.Id == id);
        if (employee == null)
            throw new EmployeeNotFoundException($"Сотрудник с ИД {id} не найден");
        return employee;
    }
    
    public void Update(T employee)
    {
        var index = employees.FindIndex(e => e.Id == employee.Id);
        if (index == -1)
            throw new EmployeeNotFoundException($"Сотрудник с ИД {employee.Id} не найден");
        employees[index] = employee;
    }
    
    public void Delete(Guid id)
    {
        var index = employees.FindIndex(e => e.Id == id);
        if (index == -1)
            throw new EmployeeNotFoundException($"Сотрудник с ИД {id} не найден");
        employees.RemoveAt(index);
    }
    #endregion
}