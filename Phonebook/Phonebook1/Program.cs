namespace Phonebook1;

class Program
{
    static void Main(string[] args)
    {
        Phonebook phonebook = Phonebook.GetInstance();
        phonebook.LoadAbonentFromFile();
        bool exit = false;
        
 while (!exit)
        {
            Console.WriteLine("Выберите действие: 1- добавить абонента, 2 - удалить абонента, 3 - Найти абонента по номеру, 4 - найти абонента по имени, 5 - выход");
            string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Введите имя:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Введите телефон:");
                        string phoneNumber = Console.ReadLine();

                        Abonent newAbonent = new Abonent { Name = name, PhoneNumber = phoneNumber };
                        phonebook.AddAbonent(newAbonent);
                        break;

                    case "2":
                        Console.Write("Введите номер телефона для удаления: ");
                        string deletedNumber = Console.ReadLine();
                        Abonent tempAbonent = new Abonent {PhoneNumber = deletedNumber};
                        phonebook.DeleteAbonent(tempAbonent);
                        break;

                    case "3":
                        Console.Write("Введите номер телефона: ");
                        string searchNumber = Console.ReadLine();

                        Abonent foundByPhone = phonebook.GetAbonentByPhoneNumber(searchNumber);
                        if (foundByPhone != null)
                            Console.WriteLine($"Имя: {foundByPhone.Name}, телефон: {foundByPhone.PhoneNumber}");
                        else
                            Console.WriteLine("Абонент не найден.");
                        break;

                    case "4":
                        Console.Write("Введите имя: ");
                        string searchName = Console.ReadLine();

                        Abonent foundByName = phonebook.GetAbonentByName(searchName);
                        if (foundByName != null)
                            Console.WriteLine($"Имя: {foundByName.Name}, телефон: {foundByName.PhoneNumber}");
                        else
                            Console.WriteLine("Абонент не найден.");
                        break;

                    case "5":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Необходимо выбрать вариант от 1 до 4, попробуйте снова");
                        break;
                }
        }
    }
}