using System;

namespace Boxing
{
    class Dummy { }
    enum State { None, }
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 3;
            object obj1 = 1;
            object obj2 = 1.4f;
            object obj3 = "베이가";
            object obj4 = new Dummy();
            object obj5 = State.None;
            a = (int)obj1; //unboxing
        }
    }
}
