using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TCP
{
	class Program
	{
		
		static void Main(string[] args)
		{
			var server = new Server();
			Console.WriteLine("Enter Port");
			server.Port = Convert.ToInt32(Console.ReadLine());

			Console.WriteLine("Enter Ip Addre 123.123.1234");
			server.IPAddress = Console.ReadLine();

			Console.WriteLine("Press 1 to start server");
			Console.WriteLine("Press 2 to stop server");

			
			switch (Console.ReadLine())
			{
				case "1":
					server.Start();
					break;
				case "2":
					server.Stope();
					break;
				default:
					break;
			}
			// The code provided will print ‘Hello World’ to the console.
			// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
			
			Console.ReadKey();

			// Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
		}

		
	}

	public class Server
	{
		SimpleTcpServer tcpserver;
		public int Port { get; set; }
		public string IPAddress { get; set; }
		public Server()
		{
			Console.WriteLine("Welcome to server");
			tcpserver = new SimpleTcpServer();
			tcpserver.Delimiter = 0x13;
			tcpserver.StringEncoder = Encoding.UTF8;
			tcpserver.DataReceived += Tcpserver_DataReceived;
		}

		private static void Tcpserver_DataReceived(object sender, Message e)
		{
			Console.WriteLine(e.MessageString.ToString());
			e.ReplyLine("hy from me");
		}
		public void Stope()
		{
			tcpserver.Stop();
		}
		public void Start()
		{

			IPAddress ip = System.Net.IPAddress.Parse(this.IPAddress);
		var tempres=	System.Net.IPAddress.Any;
			tcpserver.Start(ip, this.Port);
			Console.WriteLine("server start");
		}
	}
}
