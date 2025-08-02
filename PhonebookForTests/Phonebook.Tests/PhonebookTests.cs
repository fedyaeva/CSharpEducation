using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using NUnit.Framework.Legacy;

namespace Phonebook.Tests;

public class PhonebookTests
{
    private Phonebook phonebook;
    
    private Subscriber subscriber1;
    
    private Subscriber subscriber2;
    
    [SetUp]
    public void Setup()
    {
        phonebook = new Phonebook();
        
        var phoneNumber1 = new PhoneNumber("89112223344", PhoneNumberType.Personal);
        
        var phoneNumber2 = new PhoneNumber("89223334455", PhoneNumberType.Work);
        
        subscriber1 = new Subscriber("Саша", new List<PhoneNumber> { phoneNumber1 });
        
        subscriber2 = new Subscriber("Маша", new List<PhoneNumber> { phoneNumber2 });
    }

    [TearDown]
    public void TearDown()
    {
        phonebook = null;
    }
    
    [Test]
    public void AddSubscriber()
    {
        //Arrange
        var newSubscriber = new Subscriber("Паша", new List<PhoneNumber>
        {
            new PhoneNumber("89334445566", PhoneNumberType.Personal)
        });
        
        //Act
        phonebook.AddSubscriber(newSubscriber);
        
        var actualSubscriber = phonebook.GetSubscriber(newSubscriber.Id);
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(actualSubscriber, Is.EqualTo(newSubscriber));
            
            Assert.That(actualSubscriber.Name, Is.EqualTo(newSubscriber.Name));
            
            Assert.That(actualSubscriber.PhoneNumbers, Is.EqualTo(newSubscriber.PhoneNumbers));
        });
    }

    [Test]
    public void AddExistingSubscriber()
    {
        //Arrange
        var newSubscriber = subscriber1;
        
        //Act
        phonebook.AddSubscriber(subscriber1);
        
        phonebook.AddSubscriber(newSubscriber);

        //Assert
        Assert.Throws<InvalidOperationException>(() => phonebook.AddSubscriber(newSubscriber));
    }

    [Test]
    public void GetSubscriber()
    {
        //Arrange
        phonebook.AddSubscriber(subscriber2);
        
        //Act
        var foundSubscriber = phonebook.GetSubscriber(subscriber2.Id);
        
        //Assert
        Assert.That(foundSubscriber, Is.EqualTo(subscriber2));
    }

    [Test]
    public void GetAllSubscribers()
    {
        //Arrange
        phonebook.AddSubscriber(subscriber1);
        
        phonebook.AddSubscriber(subscriber2);
        
        //Act
        var allSubscribers = phonebook.GetAll();
        
        //Assert
        Assert.Multiple(() =>
        {
            CollectionAssert.Contains(allSubscribers, subscriber1);
            
            CollectionAssert.Contains(allSubscribers, subscriber2);
            
            Assert.That(allSubscribers, Is.EquivalentTo(new[] { subscriber1, subscriber2 }));
        });
    }

    [Test]
    public void AddPhoneNumberToSubscriber()
    {
        //Arrange
        phonebook.AddSubscriber(subscriber1);
        
        var newPhoneNumber = new PhoneNumber("89445556677", PhoneNumberType.Work);
        
        //Act
        phonebook.AddNumberToSubscriber(subscriber1, newPhoneNumber);
        
        var updatedSubscriber = phonebook.GetSubscriber(subscriber1.Id);
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(updatedSubscriber.PhoneNumbers, Has.Some.Matches<PhoneNumber>(pn => pn.Number == "89445556677"));
            
            Assert.That(updatedSubscriber.PhoneNumbers.Count, Is.EqualTo(2));
        });
    }

    [Test]
    public void RenameSubscriber()
    {
        //Arrange
        phonebook.AddSubscriber(subscriber2);
        
        string newName = "Маша с новым именем";
        
        //Act
        phonebook.RenameSubscriber(subscriber2, newName);
        
        var updatedSubscriber = phonebook.GetSubscriber(subscriber2.Id);
        
        //Assert
        Assert.That(updatedSubscriber.Name, Is.EqualTo(newName));
    }
    
    [Test]
    public void UpdateSubscriber()
    {
        //Arrange
        phonebook.AddSubscriber(subscriber1);
        
        var updatedNumbers = new List<PhoneNumber>
        {
            new PhoneNumber("89556667788", PhoneNumberType.Personal)
        };

        var updatedName = "Саша с новым именем";
        
        var updatedSubscriber = new Subscriber(subscriber1.Id, updatedName, updatedNumbers);
        
        //Act
        phonebook.UpdateSubscriber(subscriber1, updatedSubscriber);
        
        var actualSubscriber = phonebook.GetSubscriber(updatedSubscriber.Id);
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(actualSubscriber, Is.EqualTo(updatedSubscriber));
            
            Assert.That(actualSubscriber.Name, Is.EqualTo(updatedName));
            
            Assert.That(actualSubscriber.PhoneNumbers, Is.EqualTo(updatedNumbers));
        });
    }
    
    [Test]
    public void DeleteSubscriber()
    {
        //Arrange
        phonebook.AddSubscriber(subscriber1);
        
        phonebook.AddSubscriber(subscriber2);
        
        //Act
        phonebook.DeleteSubscriber(subscriber1);
        
        var allSubscribers = phonebook.GetAll();
        
        //Assert
        CollectionAssert.DoesNotContain(allSubscribers, subscriber1);
        
        CollectionAssert.Contains(allSubscribers, subscriber2);
    }
}