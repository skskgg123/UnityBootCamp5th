using System;

public class Counter
{
    public void GetTodayCount()
    {
        Console.WriteLine("오늘은 1000명이 참여하였습니다.");
    }
}

class ClassObject
{
    static void Main(string[] args)
    {
        Counter counter = new Counter();
        counter.GetTodayCount();

    }
}