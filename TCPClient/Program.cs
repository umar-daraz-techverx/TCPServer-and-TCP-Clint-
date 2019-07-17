using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
	class Program
	{
		static void Main(string[] args)
		{

			var server = new TCPClint();
			Console.WriteLine("Enter Port");
			server.Port = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("Enter Ip Addre 123.123.1234");
			server.HostName = Console.ReadLine();

			while (true)
			{
				

				Console.WriteLine("Press 1 to  connect");
				Console.WriteLine("Press 2 to send message");


				switch (Console.ReadLine())
				{
					case "1":
						server.Connect();
						break;
					case "2":
						server.SendMessag();
						break;
					default:
						
						break;
				}
			}

			

		}


		public class TCPClint
		{
			SimpleTcpClient clint;
			public int Port { get; set; }
			public string HostName { get; set; }
			public TCPClint()
			{
				Console.WriteLine("welcome to clint");
				clint = new SimpleTcpClient();
				clint.StringEncoder = Encoding.UTF8;
				clint.DelimiterDataReceived += Clint_DelimiterDataReceived;
			}

			private void Clint_DelimiterDataReceived(object sender, Message e)
			{
				Console.WriteLine(e.MessageString);
			}

			public void Connect ()
			{
				clint.Connect(this.HostName, this.Port);
				Console.WriteLine("Clint is Connected.....");
			}
			public void SendMessag()
			{
				Console.WriteLine("Enter message");
				clint.WriteLineAndGetReply(Console.ReadLine(), TimeSpan.FromSeconds(3));
			}
		}
	}
}
