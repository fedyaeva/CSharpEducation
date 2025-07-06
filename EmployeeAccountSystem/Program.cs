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
                    employee.AddEmployee(employee);
                    break;
                case "2":
                    employee.GetEmployeeInfo(employee);
                    break;
                case "3":
                    employee.UpdateEmployee(employee);
                    break;
                case "4":
                    employee.DeleteEmployee(employee);
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
}