using System;

struct BusinessCard
{
    public string name;
    public int age;
}

class StructArray
{
    static void Main(string[] args)
    {
        BusinessCard[] businessCards = new BusinessCard[3];
        businessCards[0].name = "백두산"; businessCards[0].age = 100;
        businessCards[1].name = "한라산"; businessCards[1].age = 80;
    }

}