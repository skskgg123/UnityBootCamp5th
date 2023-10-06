using System;

class Inherit
{
    static void Main(string[] args)
    {
        Child child = new Child();
        child.Hi();
        child.Hello();
        child.First();
        child.Go();
        child.No();
    }
}

interface ITest
{
    void No();
}

interface IGrandGrand
{
    void Go();
}

class GrandParent
{
    public void First() => Console.WriteLine("그랜드 어쩌구");
}

class Parent : GrandParent
{
    public void Hi() => Console.WriteLine("안녕");
}

class Child : Parent, IGrandGrand, ITest
{
    public void No() => Console.WriteLine("테스트");
    public void Go() => Console.WriteLine("그랜드그랜드");
    public void Hello() => Console.WriteLine("반갑");
}

