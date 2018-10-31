using System;
using System.IO;
using System.Collections.Generic;

namespace H1Project
{
	public class Functions
	{
        // An array of strings loaded from the "BotSvar.txt" file. Represents all the answers that the bot can give to the user.
        private string[] botAnswers;

        // The current active conversation. Might be null if there is no active conversation
        private string currentConversation;

        // All the conversations. Use a conversation name to get that specific conversation.
        private Dictionary<string, List<Message>> allConversations = new Dictionary<string, List<Message>>();

        // Conversation history queue. Used to keep track of the order when conversations were opened. 
        private List<string> conversationHistory = new List<string>();
        
        // Holds error messages that can be used in the main layout function to indicate to the user that something went wrong.
        private string lastError;

        // Random number generator for all your number generation needs
        private Random random = new Random((int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond));       

        /// <summary>
        /// Ends a conversation by removing the reference to it in the allConversations list. 
        /// </summary>
        /// <param name="conversationName">The name of the conversation which we wish to end.</param>
        private void EndConversation(string conversationName)
        {
            // Test if the conversation exists
            if (!allConversations.ContainsKey(conversationName))
            {
				lastError = "That conversation does not exist";
                return;
            }

            
            conversationHistory.Remove(conversationName);
            allConversations.Remove(conversationName);

            // If we previously switched from a conversation to this one, we can switch back
            if (conversationHistory.Count > 0)
            {
                currentConversation = conversationHistory[conversationHistory.Count - 1];
            }
            else
            {
				// TODO? Should we handle this differently?
				// There are no more conversations to switch to. 
				currentConversation = null;

				lastError = "No more conversations";
            }
        }

        /// <summary>
        /// Returns the latest error that happened. 
        /// </summary>
        /// <returns>The message of the latest error.</returns>
        public string GetLastError()
        {
            return lastError == null ? "" : lastError;
        }

        /// <summary>
        /// Deletes an old conversation from the disk.
        /// </summary>
        /// <param name="filename">Name of the file that contains the old conversation</param>
        private void DeleteConversation(string filename)
        {
            if ( !File.Exists(filename) )
            {
                lastError = "That conversation does not exist.";
                return;
            }

            File.Delete(filename);
        }
		
		/// <summary>
        /// Prints a conversation to the console. FIXME? Unneeded now?
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
                    //PrintRecievedMessage(message.GetMessage());
                }
                else
                {
                    // Commented until layout function is implemented
                    //PrintSentMessage(message.GetMessage());
                }
            }
        }		
		
        /// <summary>
        /// Fetches the list of messages that is the current conversation.
        /// </summary>
        /// <returns>A list that is the current conversation or null if there is no active conversation</returns>
        public List<Message> GetCurrentConversation()
        {
            if (currentConversation == null)
                return new List<Message>();
            else
                return allConversations[currentConversation];
        }

        /// <summary>
        /// Prints the message in a fashion that indicates it was a message sent by the user.
        /// /// FIXME? Unneeded now together with PrintConversation?
        /// </summary>
        /// <param name="message">The message that was sent.</param>
        private void PrintSentMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Prints the message in a fashion that indicates it was a message recieved by the user.
        /// /// FIXME? Unneeded now together with PrintConversation?
        /// </summary>
        /// <param name="message">The message that was recieved.</param>
        private void PrintRecievedMessage(string message)
        {
            Console.WriteLine(message);
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

        /// <summary>
        /// Command handler function to be used when switching between conversations.
        /// </summary>
        /// <param name="conversationName">The name of the conversation to switch to. </param>
        private void CommonSwitchHandler(string conversationName)
        {
            currentConversation = conversationName;
            if (!conversationHistory.Contains(currentConversation))
            {
                conversationHistory.Add(currentConversation);
            }
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
                lastError = "That conversation does not exist";
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
            File.WriteAllLines(filename, outputBuffer);
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
                lastError = "That conversation does not exist";
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
		public void Start()
		{
            botAnswers = File.ReadAllLines(@"BotSvar.txt");
        }

        /// <summary>
        /// Does the smalltalk.
        /// </summary>
        private void SmallTalk(bool isQuestion)
        {
            string answer = botAnswers[random.Next(botAnswers.Length)];
            while (isQuestion && answer.EndsWith("?")){
                answer = botAnswers[random.Next(botAnswers.Length)];
            }

            GetCurrentConversation().Add(new Message(answer,true));
        }

        /// <summary>
        /// Receives the input of the user, and chooses which method to use, based on the userinput provided.
        /// </summary>
        /// <param name="input"></param>
        public void HandleCommands(string input)
		{
            // Clear any old error messages.
            lastError = "";

			try
			{
				string[] a = input.ToLower().Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
				
				switch (a[0])
				{
					case "deleteconversation":
                        if (a.Length >= 2)
                            DeleteConversation(a[1]);
                        else
                            lastError = "Deleteconversation <conversation name>";
                        break;
                    case "printconversation":
                        if (a.Length >= 2)
						    PrintConversation(a[1]);
                        else
                            lastError = "Printconversation <conversation name>";
						break;
					case "startconversation":
                        if (a.Length >= 2)
                            StartConversation(a[1]);
                        else
                            lastError = "StartConversation <conversation name>";
						break;
					case "endconversation":
                        if (a.Length >= 2)
                            EndConversation(a[1]);
                        else
                            lastError = "EndConversation <conversation name>";
						break;
					case "saveconversation":
                        if (a.Length >= 3)
                            SaveConversation(a[1], $"{a[2]}.txt");
                        else
                            lastError = "SaveConversation <conversation name> <filename>";
                        break;
					case "switchconversation":
                        if (a.Length >= 2)
                            SwitchConversation(a[1]);
                        else
                            lastError = "switchconversation <conversation name>";
                        break;
					case "calculate":
						float.TryParse(a[1], out float firstValue);
						float.TryParse(a[3], out float secondValue);
						string result;

						switch (a[2])
						{
							case "+":
								result = (firstValue + secondValue).ToString();
								GetCurrentConversation().Add(new Message(input));
								GetCurrentConversation().Add(new Message("Citizen, your result is: " + result, true));
								break;
							case "-":
								result = (firstValue - secondValue).ToString();
                                GetCurrentConversation().Add(new Message(input));
                                GetCurrentConversation().Add(new Message("Citizen, your result is: " + result, true));
                                break;
							case "*":
								result = (firstValue * secondValue).ToString();
                                GetCurrentConversation().Add(new Message(input));
                                GetCurrentConversation().Add(new Message("Citizen, your result is: " + result, true));
                                break;
							case "/":
                                if ( secondValue == 0 )
                                {
                                    GetCurrentConversation().Add(new Message(input));
                                    GetCurrentConversation().Add(new Message("No lollygaggin'.", true));
                                }
                                else
                                {
                                    result = (firstValue / secondValue).ToString();
                                    GetCurrentConversation().Add(new Message(input));
                                    GetCurrentConversation().Add(new Message("Citizen, your result is: " + result, true));
                                }
                                break;
							default:
								break;
						}

						break;
                    case "start":
                        Start();
						break;
					case "quit":
						Quit();
						break;
					default:
                        if (currentConversation == null)
                        {
                            lastError = "No current conversation. To start a conversation use startconversation";
                        }
                        else
                        {
                            GetCurrentConversation().Add(new Message(input));
                            SmallTalk(input.EndsWith("?"));
                            PrintConversation(currentConversation);
                        }
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
