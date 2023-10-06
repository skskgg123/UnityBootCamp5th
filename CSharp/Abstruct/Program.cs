using System;

public abstract class Shape
{
    public abstract double GetArea();
}

public class Square : Shape
{
    private int _size;

    public Square (int size)
    {
        this._size = size;
    }

    public override double GetArea()
    {
        return _size * _size;
    }
}

class What
{
    static void Main(string[] args)
    {
        Square square = new Square(10);
        Console.WriteLine( square.GetArea());

    }
}