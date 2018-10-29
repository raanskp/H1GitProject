using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1Project
{
	public class Functions
	{
		private List<Message> currentConversation;
        private Dictionary<string, List<Message>> allConversations = new Dictionary<string, List<Message>>();
		
		/// <summary>
        /// Ends a conversation by removing the reference to it in the allConversations list. 
        /// </summary>
        /// <param name="conversationName">The name of the conversation which we wish to end.</param>
        private void EndConversation(string conversationName)
        {
            List<Message> conversation = allConversations[conversationName];

            // We cannot end a non-existant conversation
            if (conversation != null)
            {

            }
        }
		
		/// <summary>
        /// Prints a conversation to the console.
        /// </summary>
        /// <param name="conversationName">The name of the conversation to print.</param>
        private void printConversation(string conversationName)
        {
            List<Message> conversation = allConversations[conversationName];

            // Make sure that the conversation name is actually valid
            if (conversation != null)
            {

                foreach (Message message in conversation) 
                {
                    if (message.wasRecieved())
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
            List<Message> conversation = new List<Message>();
            allConversations.Add(conversationName, conversation);
            currentConversation = conversation;

            printConversation(conversationName);
        }
		
        /// <summary>
        /// Stores the given conversation to a file.
        /// </summary>
        /// <param name="conversationName">The name of the conversation to save.</param>
        /// <param name="filename">The filename to save the conversation to.</param>
		private void SaveConversation(string conversationName, string filename)
        {

        }

        /// <summary>
        /// Changes the current conversation to the conversation given by conversationName.
        /// </summary>
        /// <param name="conversationName">The name of the conversation to switch to.</param>
        private void SwitchConversation(string conversationName)
        {
            List<Message> conversation = allConversations[conversationName];

            // We can only switch to conversations that exist.
            if (conversation != null)
            {
                // Set the current conversation and print it to the screen
                currentConversation = conversation;
                printConversation(conversationName);
            }
        }

		private void Quit()
		{
			Environment.Exit(0);
		}

		private void Start()
		{
			//Eventuelt redundant
		}

		public void HandleCommands(string input)
		{
			//Eventuelt input.Split(' ');
			switch (input)
			{
				case "DeleteConversation":
					//DeleteConversation();
					break;
				case "PrintConversation":
					printConversation("conversationName");
					break;
				case "StartConversation":
					StartConversation("conversationName");
					break;
				case "EndConversation":
					EndConversation("conversationName");
					break;
				case "SaveConversation":
					SaveConversation("conversationName", "file.txt");
					break;
				case "SwitchConversation":
					SwitchConversation("conversationName");
					break;
				case "Start":
					Start();
					break;
				case "Quit":
					Quit();
					break;
				default:
					//SmallTalk();
					break;
			}
		}
	}	
}
