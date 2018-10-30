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
	        bool introGiven = false;
	        int phoneHeight = 23;
			string line = "--------------------------------------------------------------------------------";
			string space = "|                                                                              |";


			while (true)
	        {
		        Console.WriteLine(line);
		        for (int i = 0; i <= phoneHeight; i++)
		        {
					if (introGiven == false && i == phoneHeight)
					{
						GreetTheUser();
						introGiven = true;
					}
					Console.WriteLine(space);
		        }
		        Console.WriteLine(line);

		        string userValue = Console.ReadLine();				
		        functions.HandleCommands(userValue);
	        }
        }

		private static void GreetTheUser()
		{
			Console.WriteLine("|Hello and welcome to this chat bot.                                           |" + Environment.NewLine +
													  "|If you would like to chat, type Start.                                        |");
		}
	}
}
