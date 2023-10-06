using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hyunsoo
{
    class Cat
    {
        public string? color;
        public string? name;

        public void Meow()
        {
            Console.Write($"{name} : 야옹 \n{name} : {color} \n");
        }

    }

    class Animal
    {
        static void Main(string[] args)
        {
            Cat kitty = new Cat();
            Cat nero = new Cat();

            kitty.name = "키티";
            kitty.color = "하얀색";
            nero.name = "네로";
            nero.color = "검은색";

            kitty.Meow();
            nero.Meow();
        }
    }
}
