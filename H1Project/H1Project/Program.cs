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
            List<string> buffer = new List<string>();

            buffer.Add(Environment.NewLine);
            foreach (Message message in conversation)
            {
                if (message.WasRecieved())
                {
                    string leftoverMessage = message.GetMessage();
                    while ( leftoverMessage.Length >= 40 )
                    {
                        string part = leftoverMessage.Substring(0, 40);
                        buffer.Add(" " + part.PadLeft(78));
                        leftoverMessage = leftoverMessage.Substring(40);
                    }
                    buffer.Add(" " + leftoverMessage.PadLeft(78));
                }
                else
                {
                    string leftoverMessage = message.GetMessage();
                    while (leftoverMessage.Length >= 40)
                    {
                        string part = leftoverMessage.Substring(0, 40);
                        buffer.Add(" " + part);
                        leftoverMessage = leftoverMessage.Substring(40);
                    }
                    buffer.Add(" " + leftoverMessage);
                }
            }

            int startIndex = buffer.Count > 23 ? buffer.Count - 23: 0;

            for ( int i = startIndex; i < buffer.Count; i++ )
            {
                Console.WriteLine(buffer[i]);
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
