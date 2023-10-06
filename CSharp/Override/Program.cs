using System;

class My
{
    
}

class Your
{
    public override string ToString()
    {
        return "메소드 Override 호출";
    }
}

class Override
{
    static void Main(string[] args)
    {
        My my = new My();
        Console.WriteLine(my.ToString());

        Your your = new Your();
        Console.WriteLine(your.ToString());
    }
}