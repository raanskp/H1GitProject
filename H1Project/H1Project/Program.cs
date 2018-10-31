using System;

namespace H1Project
{
    class Program
    {
        static void Main(string[] args)
        {
	        Functions functions = new Functions();

			
			while (true)
	        {
				DrawLayout();

		        string userValue = Console.ReadLine();				
		        functions.HandleCommands(userValue);
				Console.Clear();
			}
        }

		private static void DrawLayout()
		{
			int height = 23;
			int width = 80;
			string line = "--------------------------------------------------------------------------------";
			Console.SetCursorPosition(0, 0);
			Console.WriteLine(line);
			for (int i = 0; i < height; i++)
			{
				Console.SetCursorPosition(0, i);
				Console.Write("|");
				Console.SetCursorPosition(width, i);
				Console.Write("|");
			}
			
			Console.WriteLine(Environment.NewLine + line);
		}

		private static void Greet()
		{
			Console.WriteLine("|Hello and welcome to this chat bot.                                           |" + Environment.NewLine +
							  "|If you would like to chat, type Start.                                        |");
		}
	}
}
