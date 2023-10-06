using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTest
{
    class Person
    {
        public string name = "홍길동";
        public int age = 10;
    }

    class VarClass
    {
        static void Main(string[] args)
        {
            var hong = new {name = "홍길동", age = 10};
            var park = new {name = "임꺽정", age = 140};

            Console.WriteLine($"{hong.name}, {park.name}");
        }
    }
}
