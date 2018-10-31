using System;
using System.Collections.Generic;

namespace H1Project
{
    class Program
    {
        private static Functions functions = new Functions();

        static void Main(string[] args)
        {
            functions.Start();

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
            List<Message> conversation = functions.GetCurrentConversation();

            Console.WriteLine();
            foreach (Message message in conversation)
            {
                if (message.WasRecieved())
                    Console.WriteLine(" "+message.GetMessage().PadLeft(78));
                else
                    Console.WriteLine(" "+message.GetMessage());
            }

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
			string errorIndicator = "Error: " + functions.GetLastError();

			if (errorIndicator != "Error: " || errorIndicator == "Error: No more conversations")
			{
				Console.WriteLine(errorIndicator);
			}

		}

		private static void Greet()
		{
			Console.WriteLine("|Hello and welcome to this chat bot.                                           |" + Environment.NewLine +
							  "|If you would like to chat, type Start.                                        |");
		}
	}
}
