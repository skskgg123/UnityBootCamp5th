using System;

class Category
{
    public void Print(int i)
    {
        Console.WriteLine($"{i}번째 학생의 데이터가 생성되었습니다.");
    }

    class ClassArray
    {
        static void Main(string[] args)
        {
            Category[] category = new Category[10];

            for (int i = 0; i < category.Length; i++)
            {
                category[i] = new Category();
            }

            for (int i = 0; i < category.Length; i++)
            {
                category[i].Print(i);
            }

        }


    }
}