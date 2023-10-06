using System;
using System.Xml.Serialization;

class MethodProperty
{
    static void Main()
    {
        Dog dog = new Dog();
        dog.Eat();
    }

}

class Dog
{
    public void Eat()
    {
        Console.WriteLine("밥을 먹는다");
        DIgest();
    }

    private void DIgest()
    {
        Console.WriteLine("밥을 소화한다");
        
    }
}