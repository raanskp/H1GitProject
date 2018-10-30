using System;
using System.Collections.Generic;

namespace H1Project
{
	public class Functions
	{
        private string currentConversation;
        private Dictionary<string, List<Message>> allConversations = new Dictionary<string, List<Message>>();
        private List<string> conversationHistory = new List<string>();
		
        /// <summary>
        /// Ends a conversation by removing the reference to it in the allConversations list. 
        /// </summary>
        /// <param name="conversationName">The name of the conversation which we wish to end.</param>
        private void EndConversation(string conversationName)
        {
            // Test if the conversation exists
            if (!allConversations.ContainsKey(conversationName))
            {
                Console.WriteLine("That conversation does not exist");
                return;
            }

            conversationHistory.Remove(currentConversation);
            // If we previously switched from a conversation to this one, we can switch back
            if (conversationHistory.Count > 0)
            {
                currentConversation = conversationHistory[conversationHistory.Count - 1];
            }
            else
            {
                // TODO? Should we handle this differently?
                // There are no more conversations to switch to. 
                Console.WriteLine("No more conversations");
            }
        }
		
		/// <summary>
        /// Prints a conversation to the console.
        /// </summary>
        /// <param name="conversationName">The name of the conversation to print.</param>
        private void PrintConversation(string conversationName)
        {
            // Test if the conversation exists
            if (!allConversations.ContainsKey(conversationName))
            {
                Console.WriteLine("That conversation does not exist");
                return;
            }

            foreach (Message message in allConversations[conversationName]) 
            {
                if (message.WasRecieved())
                {
                    // Commented until layout function is implemented
                    //printRecievedMessage(message.getMessage());
                }
                else
                {
                    // Commented until layout function is implemented
                    //printSentMessage(message.getMessage());
                }
            }
        }		
		
		/// <summary>
        /// This function starts a conversation by creating a new List<Message> to contain all the messages sent/recieved.
        /// If a conversation already exists, it will switch to that conversation instead. 
        /// </summary>
        /// <param name="conversationName">The name of the conversation which we wish to start.</param>
        private void StartConversation(string conversationName)
        {
            // Test if it is a pre-existing conversation and switch to it if it is
            if (allConversations.ContainsKey(conversationName))
            {
                SwitchConversation(conversationName);
                return;
            }

            // Create a new conversation, add it to the conversation list and set it active
            allConversations.Add(conversationName, new List<Message>());
            CommonSwitchHandler(conversationName);
        }

        private void CommonSwitchHandler(string conversationName)
        {
            currentConversation = conversationName;
            conversationHistory.Add(currentConversation);
            PrintConversation(conversationName);
        }

        /// <summary>
        /// Stores the given conversation to a file.
        /// </summary>
        /// <param name="conversationName">The name of the conversation to save.</param>
        /// <param name="filename">The filename to save the conversation to.</param>
        private void SaveConversation(string conversationName, string filename)
        {
            // Test if the conversation actually exists before trying to save it
            if (!allConversations.ContainsKey(conversationName))
            {
                Console.WriteLine("That conversation does not exist");
                return;
            }

            List<Message> conversation = allConversations[conversationName];

            // Buffer the output, so we can write it all to the file in one go
            string[] outputBuffer = new string[conversation.Count];
            for (int i = 0; i < conversation.Count; i++)
            {
                Message message = conversation[i];

                if (message.WasRecieved())
                    outputBuffer[i] = "Other: " + message.GetMessage();
                else
                    outputBuffer[i] = "Me: " + message.GetMessage();
            }

            // Write the buffer 
            System.IO.File.WriteAllLines(filename, outputBuffer);
        }

        /// <summary>
        /// Changes the current conversation to the conversation given by conversationName.
        /// </summary>
        /// <param name="conversationName">The name of the conversation to switch to.</param>
        private void SwitchConversation(string conversationName)
        {
            // Test if the conversation exists
            if (!allConversations.ContainsKey(conversationName))
            {
                Console.WriteLine("That conversation does not exist");
                return;
            }

            // Set the current conversation and print it to the screen
            CommonSwitchHandler(conversationName);
        }

		/// <summary>
		/// Quits the console application.
		/// </summary>
		private void Quit()
		{
            Console.WriteLine("Good bye, have a nice day");
			Environment.Exit(0);
		}

		/// <summary>
		/// Introduction to the bot, functionality, commands, experience. - Probably redundant.
		/// </summary>
		private void Start()
		{
            Console.WriteLine("Contents of BotSvar.txt =");
            foreach (string line in System.IO.File.ReadAllLines(@"BotSvar.txt"))
            {
                Console.WriteLine("\t" + line);
            }
        }

        /// <summary>
        /// Does the smalltalk
        /// </summary>
        private void SmallTalk()
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Receives the input of the user, and chooses which method to use, based on the userinput provided.
        /// </summary>
        /// <param name="input"></param>
        public void HandleCommands(string input)
		{
			try
			{
				string[] a = input.ToLower().Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
				
				switch (a[0])
				{
					case "deleteconversation":
						//DeleteConversation();
						break;
					case "printconversation":
                        if ( a.Length >= 2 )
						    PrintConversation(a[1]);
                        else
                            Console.WriteLine("Printconversation <conversation name>");
						break;
					case "startconversation":
                        if (a.Length >= 2)
                            StartConversation(a[1]);
                        else
                            Console.WriteLine("StartConversation <conversation name>");
						break;
					case "endconversation":
                        if (a.Length >= 2)
                            EndConversation(a[1]);
                        else
                            Console.WriteLine("EndConversation <conversation name>");
						break;
					case "saveconversation":
                        if (a.Length >= 3)
                            SaveConversation(a[1], $"{a[2]}.txt");
                        else
                            Console.WriteLine("SaveConversation <conversation name> <filename>");
                        break;
					case "switchconversation":
                        if (a.Length >= 3)
                            SwitchConversation(a[1]);
                        else
                            Console.WriteLine("switchconversation <conversation name>");
                        break;
                    case "start":
						Start();
						break;
					case "quit":
						Quit();
						break;
					default:
                        SmallTalk();
                        break;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
    }	
}
