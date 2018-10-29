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
            
        }
		
		/// <summary>
        /// Prints a conversation to the console.
        /// </summary>
        /// <param name="conversationName">The name of the conversation to print.</param>
        private void printConversation(string conversationName)
        {
            List<Message> conversation = allConversations[conversationName];

            // Make sure that the conversation name is actually valid
            if ( conversation != null )
            {
                foreach (Message message in conversation ) 
                {
                    if ( message.wasRecieved())
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
            if ( allConversations.ContainsKey(conversationName) )
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
		
		private void SaveConversation(string conversationName, string filename)
        {

        }

        private void SwitchConversation(string conversationName)
        {

        }
	}

	public void Quit()
	{
		Environment.Exit(0);
	}

	public void Start()
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
			case "StartConversation":
				//StartConversation();
				break;
			case "EndConversation":
				//EndConversation();
				break;
			case "SaveConversation":
				//SaveConversation();
				break;
			default:
				//SmallTalk();
				break;
		}
	}
}
