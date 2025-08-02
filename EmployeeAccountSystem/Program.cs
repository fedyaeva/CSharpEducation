using System;

namespace EmployeeAccountSystem;

class Program
{
    static void Main(string[] args)
    {
        var employee = new EmployeeManager<Employee>();
  
        bool exit = false;
        while(!exit){
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Добавить сотрудника");
            Console.WriteLine("2. Получить информацию о сотруднике по имени");
            Console.WriteLine("3. Обновить данные о сотруднике по ИД");
            Console.WriteLine("4. Удалить сотрудника по ИД");
            Console.WriteLine("5. Выйти");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddEmployee(employee);
                    break;
                case "2":
                    GetEmployeeInfo(employee);
                    break;
                case "3":
                    UpdateEmployee(employee);
                    break;
                case "4":
                    DeleteEmployee(employee);
                    break;
                case "5":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Действие не найдено, повторите ввод");
                    break;
            }
        }
    }
    
     #region Методы
     
    /// <summary>
   /// Процесс добавления сотрудника.
   /// </summary>
   /// <param name="manager">Менеджер управления сотрудниками.</param>
    public static void AddEmployee(EmployeeManager<Employee> manager)
        {
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            Console.WriteLine("Выберите тип сотрудника:");
            Console.WriteLine("1. Полный рабочий день");
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
   /// Процесс получения информации о сотруднике по имени.
   /// </summary>
   /// <param name="manager">Менеджер управления сотрудниками.</param>
    public static void GetEmployeeInfo(EmployeeManager<Employee> manager)
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
   /// Процесс изменения сотрудника.
   /// </summary>
   /// <param name="manager">Менеджер управления сотрудниками.</param>
    public static void UpdateEmployee(EmployeeManager<Employee> manager)
    {
        Console.Write("Введите ИД сотрудника для обновления: ");
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
                Console.Write($"Обновить информацию о сотруднике '{fullTimeEmployee.Name}'.\n");
                Console.Write("Введите новую зарплату: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal salary))
                {
                    Console.WriteLine("Необходимо было ввести число");
                    return;
                }
                var updatedFullTimeEmp = new FullTimeEmployee(fullTimeEmployee.Name, salary) { Id = fullTimeEmployee.Id };
                manager.Update(updatedFullTimeEmp);
                Console.WriteLine("Информация обновлена");
            }
            else if (editableEmployee is PartTimeEmployee partTimeEmployee)
            {
                Console.Write($"Обновить информацию о сотруднике '{partTimeEmployee.Name}'.\n");
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
                var updatedPart = new PartTimeEmployee(partTimeEmployee.Name, rate, hours) { Id = partTimeEmployee.Id };
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
   /// Процесс удаления сотрудника.
   /// </summary>
   /// <param name="manager">Менеджер управления сотрудниками.</param>
    public static void DeleteEmployee(EmployeeManager<Employee> manager)
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
            Console.WriteLine($"Сотрудник с ИД '{id}' успешно удален.");
        }
        catch (EmployeeNotFoundException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    /// <summary>
    /// Отображение информации о сотруднике.
    /// </summary>
    /// <param name="employee">Сотрудник.</param>
    static void ShowEmployeeInfo(Employee employee)
    {
        if (employee is FullTimeEmployee fullTimeEmployee)
        {
            Console.WriteLine("Сотрудник с полной занятостью:");
            Console.WriteLine($"ИД: {fullTimeEmployee.Id}");
            Console.WriteLine($"Имя: {fullTimeEmployee.Name}");
            Console.WriteLine($"Зарплата: {fullTimeEmployee.BaseSalary}");
            Console.WriteLine($"Расчитанная зарплата: {fullTimeEmployee.CalculateSalary()}");
        }
        else if (employee is PartTimeEmployee partTimeEmployee)
        {
            Console.WriteLine("Сотрудник с частичной занятостью:");
            Console.WriteLine($"ИД: {partTimeEmployee.Id}");
            Console.WriteLine($"Имя: {partTimeEmployee.Name}");
            Console.WriteLine($"Часовая ставка: {partTimeEmployee.BaseSalary}");
            Console.WriteLine($"Отработано часов: {partTimeEmployee.HoursWorked}");
            Console.WriteLine($"Расчитаная зарплата: {partTimeEmployee.CalculateSalary()}");
        }
    }
    
    #endregion
}