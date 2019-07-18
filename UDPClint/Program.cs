using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPClint
{
	class Program
	{
		static void Main(string[] args)
		{
			UDPSocket c = new UDPSocket();
			c.Client("172.27.96.1", 9696);
			c.Send("TEST!");
			Console.ReadKey();
		}
	}
}
