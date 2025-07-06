namespace EmployeeAccountSystem;

/// <summary>
/// Управление сотрудниками
/// </summary>
/// <typeparam name="T"></typeparam>
public class EmployeeManager<T> : IEmployeeManager<T> where T : Employee
{
    private List<T> employees = new List<T>();

/// <summary>
/// Добавление сотрудника в список
/// </summary>
/// <param name="employee"></param>
/// <exception cref="EmployeeExistsException"></exception>
    public void Add(T employee)
    {
        if (employees.Exists(e => e.name == employee.name))
            throw new EmployeeExistsException($"Сотрудник с именем '{employee.name}' уже есть в списке");

        employees.Add(employee);
    }


/// <summary>
/// Получение информации о сотруднике по имени
/// </summary>
/// <param name="name"></param>
/// <returns></returns>
/// <exception cref="EmployeeNotFoundException"></exception>
    public T Get(string name)
    {
        var employee = employees.Find(e => e.name == name);
        if (employee == null)
            throw new EmployeeNotFoundException($"Сотрудник с именем '{name}' не найден");
        return employee;
    }
    
/// <summary>
/// Получение информации о сотруднике по ИД
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
/// <exception cref="EmployeeNotFoundException"></exception>
    public T Get(Guid id)
    {
        var employee = employees.Find(e => e.id == id);
        if (employee == null)
            throw new EmployeeNotFoundException($"Сотрудник с ИД {id} не найден");
        return employee;
    }

/// <summary>
/// Изменение сотрудника
/// </summary>
/// <param name="employee"></param>
/// <exception cref="EmployeeNotFoundException"></exception>
    public void Update(T employee)
    {
        var index = employees.FindIndex(e => e.id == employee.id);
        if (index == -1)
            throw new EmployeeNotFoundException($"Сотрудник с ИД {employee.id} не найден");
        employees[index] = employee;
    }

/// <summary>
/// Удаление сотрудника
/// </summary>
/// <param name="id"></param>
/// <exception cref="EmployeeNotFoundException"></exception>
    public void Delete(Guid id)
    {
        var index = employees.FindIndex(e => e.id == id);
        if (index == -1)
            throw new EmployeeNotFoundException($"Сотрудник с ИД {id} не найден");
        employees.RemoveAt(index);
    }

/// <summary>
/// Процесс добавления сотрудника
/// </summary>
/// <param name="manager"></param>
    public void AddEmployee(EmployeeManager<Employee> manager)
        {
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            Console.WriteLine("Выберите тип сотрудника:");
            Console.WriteLine("1. Полная занятость");
            Console.WriteLine("2. Частичная занятость");
            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        Console.Write("Введите фиксированную зарплату: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal baseSalaryFullTime))
                        {
                            Console.WriteLine("Зарплата указана неверно, необходимо было ввести число");
                            return;
                        }

                        var fullTimeEmployee = new FullTimeEmployee(name, baseSalaryFullTime);
                        manager.Add(fullTimeEmployee);
                        Console.WriteLine("Сотрудник успешно добавлен");
                        break;
                    case "2":
                        Console.Write("Введите ставку за час: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal baseSalaryPartTime))
                        {
                            Console.WriteLine("Ставка указана неверно, необходимо было ввести число");
                            return;
                        }

                        Console.Write("Введите количество отработанных часов: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal hoursWorked))
                        {
                            Console.WriteLine("Часы указаны неверно, необходимо было ввести число");
                            return;
                        }

                        var partTimeEmployee = new PartTimeEmployee(name, baseSalaryPartTime, hoursWorked);
                        manager.Add(partTimeEmployee);
                        Console.WriteLine("Сотрудник успешно добавлен");
                        break;
                    default:
                        Console.WriteLine("Для выбора типа сотрудника доступны только варианты 1 и 2");
                        break;
                }
            }
            catch (EmployeeExistsException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    
/// <summary>
/// Процесс получения информации о сотруднике по имени
/// </summary>
/// <param name="manager"></param>
    public void GetEmployeeInfo(EmployeeManager<Employee> manager)
    {
        Console.Write("Введите имя сотрудника: ");
        string name = Console.ReadLine();

        try
        {
            var employee = manager.Get(name);
            ShowEmployeeInfo(employee);
        }
        catch (EmployeeNotFoundException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
    
/// <summary>
/// Процесс изменения сотрудника
/// </summary>
/// <param name="manager"></param>
    public void UpdateEmployee(EmployeeManager<Employee> manager)
    {
        Console.Write("Введите ИД сотрудника для обновления данных: ");
        string idInput = Console.ReadLine();

        if (!Guid.TryParse(idInput, out Guid id))
        {
            Console.WriteLine("ИД введен неправильно");
            return;
        }

        try
        {
            var editableEmployee = manager.Get(id);

            if (editableEmployee is FullTimeEmployee fullTimeEmployee)
            {
                Console.Write($"Обновить информацию о сотруднике {fullTimeEmployee.name} \n");
                Console.Write("Введите новую зарплату: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
                {
                    Console.WriteLine("Необходимо было ввести число");
                    return;
                }
                var updatedFullTimeEmp = new FullTimeEmployee(fullTimeEmployee.name, salary) { id = fullTimeEmployee.id };
                manager.Update(updatedFullTimeEmp);
                Console.WriteLine("Информация обновлена");
            }
            else if (editableEmployee is PartTimeEmployee partTimeEmployee)
            {
                Console.Write($"Обновить информацию о сотруднике {partTimeEmployee.name} \n");
                Console.Write("Введите количество отработанных часов: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal hours))
                {
                    Console.WriteLine("Необходимо указать число");
                    return;
                }
                Console.Write("Введите почасовую ставку: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal rate))
                {
                    Console.WriteLine("Необходимо было ввести число");
                    return;
                }
                var updatedPart = new PartTimeEmployee(partTimeEmployee.name, rate, hours) { id = partTimeEmployee.id };
                manager.Update(updatedPart);
                Console.WriteLine("Информация обновлена");
            }
        }
        catch (EmployeeNotFoundException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }


/// <summary>
/// Процесс удаления сотрудника
/// </summary>
/// <param name="manager"></param>
    public void DeleteEmployee(EmployeeManager<Employee> manager)
    {
        Console.Write("Введите ИД удаляемого сотрудника: ");
        string idInput = Console.ReadLine();

        if (!Guid.TryParse(idInput, out Guid id))
        {
            Console.WriteLine("ИД введен неправильно");
            return;
        }

        try
        {
            manager.Delete(id);
            Console.WriteLine($"Сотрудник с ИД {id} успешно удален");
        }
        catch (EmployeeNotFoundException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

 /// <summary>
 /// Отображение информации о сотруднике
 /// </summary>
 /// <param name="employee"></param>
    static void ShowEmployeeInfo(Employee employee)
    {
        if (employee is FullTimeEmployee fullTimeEmployee)
        {
            Console.WriteLine("Сотрудник с полной занятостью:");
            Console.WriteLine($"ИД: {fullTimeEmployee.id}");
            Console.WriteLine($"Имя: {fullTimeEmployee.name}");
            Console.WriteLine($"Зарплата: {fullTimeEmployee.baseSalary}");
            Console.WriteLine($"Расчитанная зарплата: {fullTimeEmployee.CalculateSalary()}");
        }
        else if (employee is PartTimeEmployee partTimeEmployee)
        {
            Console.WriteLine("Сотрудник с частичной занятостью:");
            Console.WriteLine($"ИД: {partTimeEmployee.id}");
            Console.WriteLine($"Имя: {partTimeEmployee.name}");
            Console.WriteLine($"Часовая ставка: {partTimeEmployee.baseSalary}");
            Console.WriteLine($"Отработано часов: {partTimeEmployee.hoursWorked}");
            Console.WriteLine($"Расчитаная зарплата: {partTimeEmployee.CalculateSalary()}");
        }
    }
}