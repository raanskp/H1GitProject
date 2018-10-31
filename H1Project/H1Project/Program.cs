using System;
using System.Collections.Generic;

namespace H1Project
{
    class Program
    {
        private static Functions functions = new Functions();
		private static int height = 23;
		private static int width = 80;

		static void Main(string[] args)
        {
            //functions.Start();

			while (true)
	        {
				DrawLayout(height, width);
		        string userValue = Console.ReadLine();				
		        functions.HandleCommands(userValue);
				Console.Clear();
			}
        }

		private static void DrawLayout(int height, int width)
		{
			int halfWidth = width / 2;

			List<string> buffer = new List<string>
			{
				Environment.NewLine
			};

			foreach (Message message in functions.GetCurrentConversation())
			{
				if (message.WasRecieved())
				{
					string leftoverMessage = message.GetMessage();
					while (leftoverMessage.Length >= halfWidth)
					{
						string part = leftoverMessage.Substring(0, halfWidth);
						buffer.Add(" " + part.PadLeft(width - 2));
						leftoverMessage = leftoverMessage.Substring(halfWidth);
					}
					buffer.Add(" " + leftoverMessage.PadLeft(width - 2));
				}
				else
				{
					string leftoverMessage = message.GetMessage();
					while (leftoverMessage.Length >= halfWidth)
					{
						string part = leftoverMessage.Substring(0, halfWidth);
						buffer.Add(" " + part);
						leftoverMessage = leftoverMessage.Substring(halfWidth);
					}
					buffer.Add(" " + leftoverMessage);
				}
			}

			for (int i = buffer.Count > height ? buffer.Count - height + 1 : 0; i < buffer.Count; i++)
			{
				Console.WriteLine(buffer[i]);
			}

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

			ErrorHandler();
		}

		private static void ErrorHandler()
		{
			string errorIndicator = "Error: " + functions.GetLastError();

			if (errorIndicator != "Error: " || errorIndicator == "Error: No more conversations")
			{
				Console.WriteLine(errorIndicator);
			}
		}
	}
}
