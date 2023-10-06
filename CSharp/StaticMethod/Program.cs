using System;

class One
{
    public static void Hi()
    {
        Console.WriteLine("안녕하세요");
    }
}

class Two
{
    public static void Hi()
    {
        Console.WriteLine("반갑습니다");
    }
    public void Hello()
    {
        Console.WriteLine("또 만나요(빨리 가라)");
    }
}

class StaticMethod
{
    static void Main(string[] args)
    {
        One.Hi();
        Two.Hi();

        Two ct = new Two();
        ct.Hello();
    }
}