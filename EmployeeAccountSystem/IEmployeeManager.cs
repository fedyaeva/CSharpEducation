using System;

namespace EmployeeAccountSystem;

/// <summary>
/// Управление сотрудниками.
/// </summary>
/// <typeparam name="T">Сотрудник.</typeparam>
public interface IEmployeeManager<T> where T : Employee
{
    /// <summary>
    /// Добавление сотрудника.
    /// </summary>
    /// <param name="employee">Сотрудник.</param>
    void Add(T employee); 
    
    /// <summary>
    /// Получение сотрудника по имени.
    /// </summary>
    /// <param name="name">Имя.</param>
    /// <returns>Сотрудник, если был найден.</returns>
    T Get(string name);
    
    /// <summary>
    /// Получение сотрудника по ИД.
    /// </summary>
    /// <param name="id">ИД сотрудника.</param>
    /// <returns>Сотрудник, если был найден.</returns>
    T Get(Guid id);
    
    /// <summary>
    /// Изменение сотрудника.
    /// </summary>
    /// <param name="employee">Сотрудник.</param>
    void Update(T employee);
    
    /// <summary>
    /// Удаление сотрудника по ИД.
    /// </summary>
    /// <param name="id">ИД сотррудника.</param>
    void Delete(Guid id);
}