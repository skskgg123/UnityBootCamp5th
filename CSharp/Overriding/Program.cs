using System;

class Parent
{
    public virtual void Work() => Console.WriteLine("프로게이머");
}

class Child : Parent
{
    public override void Work() => Console.WriteLine("프로그래머");
}

class Overriding
{
    static void Main(string[] args)
    {
        Parent parent = new Parent();
        parent.Work();

        Child child = new Child();
        child.Work();
    }
}