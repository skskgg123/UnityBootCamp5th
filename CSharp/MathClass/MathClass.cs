using System;

class MyMath
{
    //int sum; //필드
    public void Sum(int x, int y)
    {
        int sum = x + y;
        Console.WriteLine($"합은 = {sum}");
    }
}

class MathClass
{
    static void Main(string[] args)
    {
        MyMath myMath = new MyMath();
        myMath.Sum(10, 20);
    }
}