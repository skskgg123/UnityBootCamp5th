using System;

class Person
{
    //private string? name; //필드

    public string Name
    {
        get; set; //프로퍼티
    }

    /*public void SetName(string name)
    {
        this.name = name;
    }

    public string GetName()
    {
        return this.name;
    }*/
}

class Property
{
    static void Main(string[] args)
    {
        Person person = new Person();
        person.Name = "김현수";
        Console.WriteLine(person.Name);
    }
}