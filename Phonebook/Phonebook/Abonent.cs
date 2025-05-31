namespace Phonebook;

public class Abonent
{
    
    private string phoneNumber;

    public string PhoneNumber
    {
        get { return phoneNumber; }
        set { phoneNumber = value; }
    }
    
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public Abonent(string phoneNumber, string name)
    {
        this.name = name;
        this.phoneNumber = phoneNumber;
    }
    
    public Abonent()
    {
    }
    
}