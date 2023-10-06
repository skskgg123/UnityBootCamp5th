using System;

class Shared
{
    public static void Show()
    {
        Console.WriteLine("Hello");
    }

    class StaticMethod
    {
        static void Main(string[] args)
        {
            //Shared shared = new Shared();
            Shared.Show();
        }
       
    }
}