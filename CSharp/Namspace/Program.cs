using System;
using System.Xml.Serialization;

namespace Korea
{
    namespace Seoul
    {
        public class Car
        {
            public void Run()
            {
                Console.WriteLine("ㄴ는야 서울 차");
            }
        }
    }

    namespace Incheon
    {
        public class Car
        {
            public void Run()
            {
                Console.WriteLine("아 뭘 자꾸 불러");
            }
        }
    }

    class NamespaceLink
    {
        static void Main(string[] args)
        {
            Korea.Seoul.Car seooul = new Seoul.Car();
            seooul.Run();

            
        }
    }

}

