namespace Phonebook;
using System.IO;
using System.Collections.Generic;


public class Phonebook
{
    private static readonly Phonebook instance = new Phonebook();
    private readonly List<Abonent> abonents = new List<Abonent>();
    private readonly string fileName = "phonebook.txt";

    private Phonebook()
    {
        
    }

    public static Phonebook GetInstance()
    {
        return instance;
    }

    /// <summary>
    /// Загрузка абонентов из файла
    /// </summary>
    public void LoadAbonentFromFile()
    {
        if (File.Exists(fileName))
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        string phoneNumber = parts[0];
                        string name = parts[1];
                        abonents.Add(new Abonent(phoneNumber, name));
                    }
                }
            }
        }  
    }
    
    /// <summary>
    /// Сохранение абонента в файл
    /// </summary>
    /// <param name="abonent">Абонент</param>
    private void SaveAbonentToFile(Abonent abonent)
    {
        string entry = $"{abonent.Name},{abonent.PhoneNumber}";
        using (StreamWriter sw = new StreamWriter(fileName, true))
        {
            sw.WriteLine(entry);
        }
        Console.WriteLine("Абонент сохранен");        
    }
    /// <summary>
    /// Сохранение файла
    /// </summary>
    private void SaveAllToFile()
    {
        using (StreamWriter sw = new StreamWriter(fileName))
        {
            foreach (var a in abonents)
            {
                sw.WriteLine($"{a.PhoneNumber},{a.Name}");
            }
        }
    }
    
    /// <summary>
    /// Добавление нового абонента
    /// </summary>
    /// <param name="abonent">Абонент</param>
    public void AddAbonent(Abonent abonent)
    {
        foreach (Abonent abonent2 in abonents)
        {
            if (abonent2.PhoneNumber == abonent.PhoneNumber)
            {
                Console.WriteLine("Абонент с таким номером телефона уже существует");
                return;
            }
        }
        Abonent newAbonent = new Abonent(abonent.PhoneNumber, abonent.Name);
        abonents.Add(newAbonent);
        SaveAbonentToFile(newAbonent); 
        Console.WriteLine("Абонент добавлен");
        SaveAllToFile();
    }

    /// <summary>
    /// Удаление абонента
    /// </summary>
    /// <param name="abonent"></param>
    public void DeleteAbonent(Abonent abonent)
    {
        int deleted = -1;
        for (int i = 0; i < abonents.Count; i++)
        {
            if (abonents[i].PhoneNumber == abonent.PhoneNumber)
            {
                deleted = i;
                break;
            }
        }
        if (deleted != -1)
        {
            abonents.Remove(abonents[deleted]);
            SaveAbonentToFile(abonents[deleted]);
            Console.WriteLine("Абонент удален");
            SaveAllToFile();
        }
        else
        {
            Console.WriteLine("Абонент не найден");
        }
    }

    /// <summary>
    /// Получение абонента по номеру
    /// </summary>
    public Abonent GetAbonentByPhoneNumber(string phoneNumber)
    {
        foreach (var abonent in abonents)
        {
            if (abonent.PhoneNumber == phoneNumber)
                return abonent;
        }
        return null;
    }

    /// <summary>
    /// Получение абонента по имени
    /// </summary>
    public Abonent GetAbonentByName(string name)
    {
        foreach (var abonent in abonents)
        {
            if (abonent.Name == name)
                return abonent;
        }
        return null;
    }

    public bool AddAbonent(string? abonent, string? phoneNumber)
    {
        throw new NotImplementedException();
    }
}