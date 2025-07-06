namespace EmployeeAccountSystem;

/// <summary>
/// Управление сотрудниками
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IEmployeeManager<T> where T : Employee
{
    void Add(T employee); 
    T Get(string name);
    T Get(Guid id);
    void Update(T employee);
    void Delete(Guid id);
}