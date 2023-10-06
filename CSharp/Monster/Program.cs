using System;
using System.Security.Cryptography.X509Certificates;

class Monster
{
    public static int monsterNum = 0;
    public string? name;
    public int attack;

    public void SetMonster(string  name, int attack)
    {
        this.name = name;
        this.attack = attack;
        monsterNum++;
        if(monsterNum > 3)
        {
            Console.WriteLine("너무 많은 Monster가 호출되었습니다.");
        }

        
    }
    public static int GetMonsterNum()
    { return monsterNum; }
}

class MakeMonster
{
    static void Main(string[] args)
    {
        Monster monster1 = new Monster();
        monster1.SetMonster("짬뽕괴물", 100);

        Monster monster2 = new Monster();
        monster2.SetMonster("짜장괴물", 80);

        Monster monster3 = new Monster();
        monster3.SetMonster("초밥괴물", 500);

        Monster monster4 = new Monster();
        monster4.SetMonster("백반괴물", 50);

        Console.WriteLine(Monster.GetMonsterNum());
    }
}