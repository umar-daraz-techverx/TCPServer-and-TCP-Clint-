using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP
{
	class Program
	{
		static void Main(string[] args)
		{


			UDPSocket s = new UDPSocket();
			s.Server("172.27.96.1", 9696);
			Console.ReadKey();
			
		}
	}
}
