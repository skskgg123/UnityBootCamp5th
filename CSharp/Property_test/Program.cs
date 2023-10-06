using System;
using System.Globalization;

class Property
{
    static void Main(string[] args)
    {
        PP pp = new PP();
        pp.name = "백두산";
        pp.age = 100;
        Console.WriteLine($"{pp.name}, {pp.age}");

        PC pc = new PC("홍길동", 21);
        pc.Print();

        PI pi = new PI();
        pi.Name = "임꺽정";
        pi.Age = 30;
        Console.WriteLine($"{pi.Name}, {pi.Age}");
        
    }
}

class PP
{
    public string? name;
    public int age;

}

class PC
{
    private string? name;
    private int age;

    public PC(string name, int age)
    {
        this.name = name;
        this.age = age; 
    }

    public void Print()
    {
        Console.WriteLine($"{name}, {age}");
    }
}

class PI
{
    public string Name
    {
        get; set;
    }
    public int Age
    {
        get; set;
    }
    
}