using System;

class PrivateConstruct
{
    static void Main()
    {
        My my = new My("홍길동", 21);
        my.PrintMy();

        My my2 = new My("임꺽정", 40);
        my2.PrintMy();

    }
    
}

public class My
{
    private string _name;
    private int _age;

    public My(string name, int age)
    {
        _name = name;
        _age = age; 
    }
    public void PrintMy()
    {
        Console.WriteLine($"이름: {_name}, 나이: {_age}");
    }
}