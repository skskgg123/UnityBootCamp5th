using System;

class Student
{
    public Student()
    {
        Console.WriteLine("Student 생성자가 호출되었습니다");
    }
}

class ConstructDemo
{
    public ConstructDemo()
    {
        Console.WriteLine("생성자가 호출되었습니다");
    }

    static void Main(string[] args)
    {
        //ConstructDemo constructDemo = new ConstructDemo();

        Student student = new Student();


    }
}