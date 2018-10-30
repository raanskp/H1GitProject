using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1Project
{
    class Program
    {
        static void Main(string[] args)
        {
			Functions functions = new Functions();

			while (true)
			{
                Console.WriteLine("Hello and welcome to this chat bot.");
                Console.WriteLine("If you would like to chat, type Start");
				Console.Write(">");

                string userValue = Console.ReadLine();				
                functions.HandleCommands(userValue);
			}
        }
    }
}
