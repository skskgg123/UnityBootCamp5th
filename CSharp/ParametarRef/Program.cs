using System;

class ParameterRef
{
    static void Main(string[] args)
    {
        int num = 10;
        Console.WriteLine(num);

        Do(out num);
        Console.WriteLine(num);
    }

    static void Do(out int num)
    {
        num = 20;
        Console.WriteLine($"[2] num = {num}");
    }
}
