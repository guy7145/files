using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            ICryptograph cat = new AesCryptor(new byte[32], new byte[16]);
            Console.WriteLine("Hello World!");
            
            byte[] encrypted = cat.Encrypt("Hello World!");
            string show = "";
            for (int i = 0; i < encrypted.Length; i++)
            {
                show += Convert.ToChar(encrypted[i]);
            }

            Console.WriteLine(show);
            Console.WriteLine(cat.Decrypt(encrypted));
            Console.ReadKey();
        }
    }
}
