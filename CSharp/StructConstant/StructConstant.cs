using System;

struct Flower
{
    private string _name;
    private int _price;

    public Flower(string name, int price) // 생성자 (메서드와는 구분되어야 함, 변수명 앞에 아무것도 붙지않음)
    {
        _name = name;
        _price = price;
    }

    public void Show()
    {
        Console.WriteLine($"{_name}, {_price}");
    }
}

class StructConstanct
{
    static void Main(string[] args)
    {
        /*Flower flower;
        flower.name = "Reos";
        flower.price = 12_000;
        Console.WriteLine($"{flower.name}, {flower.price}");*/

        Flower flower = new Flower("Rose", 12_000);
        flower.Show();
    }
}
