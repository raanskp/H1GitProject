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

			while (true)
			{
				//Input
				functions.HandleCommands("input");
			}
        }
    }
}
