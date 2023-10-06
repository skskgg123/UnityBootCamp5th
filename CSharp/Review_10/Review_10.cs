using System;

struct Point
{
    //전역 변수
    public int x;
    public int y;
    public string name;
    public int age;
    public string address;


    public void Show()
    {
        int a = 10; //지역 변수
        Console.WriteLine(a);
    }
}

class Review_10
{
    static void Main(string[] args)
    {
        Point point;
        point.x = 1;
        point.y = 2;
        Console.WriteLine($"{point.x}, {point.y}");
    }
}
