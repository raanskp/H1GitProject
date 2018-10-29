using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H1Project
{
    class Message
    {
        string message;
        bool isRecieved;

        public Message(string message, bool isRecieved = false)
        {
            this.message = message;
            this.isRecieved = isRecieved;
        }

        public string getMessage()
        {
            return message;
        }

        public bool wasRecieved()
        {
            return isRecieved;
        }
    }
}
