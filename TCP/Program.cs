using SimpleTCP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
			Console.ReadKey();
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
			SaveData("test", e.Data);
			//e.ReplyLine("hy from me");
		}
		private static bool SaveData(string FileName, byte[] Data)
		{
			var respinse = GetMimeFromBytes(Data);
			BinaryWriter Writer = null;
			string Name = @"C:\temp\yourfile2.png";

			try
			{
				// Create a new stream to write to the file
				Writer = new BinaryWriter(File.OpenWrite(Name));

				// Writer raw data                
				Writer.Write(Data);
				Writer.Flush();
				Writer.Close();
			}
			catch
			{
				//...
				return false;
			}

			return true;
		}

		public static int MimeSampleSize = 256;

		public static string DefaultMimeType = "application/octet-stream";

		[DllImport(@"urlmon.dll", CharSet = CharSet.Auto)]
		private extern static uint FindMimeFromData(
			uint pBC,
			[MarshalAs(UnmanagedType.LPStr)] string pwzUrl,
			[MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
			uint cbSize,
			[MarshalAs(UnmanagedType.LPStr)] string pwzMimeProposed,
			uint dwMimeFlags,
			out uint ppwzMimeOut,
			uint dwReserverd
		);

		public static string GetMimeFromBytes(byte[] data)
		{
			try
			{
				uint mimeType;
				FindMimeFromData(0, null, data, (uint)MimeSampleSize, null, 0, out mimeType, 0);

				var mimePointer = new IntPtr(mimeType);
				var mime = Marshal.PtrToStringUni(mimePointer);
				Marshal.FreeCoTaskMem(mimePointer);

				return mime ?? DefaultMimeType;
			}
			catch
			{
				return DefaultMimeType;
			}
		}
		public void Stope()
		{
			tcpserver.Stop();
		}
		public void Start()
		{
			IPAddress ip = System.Net.IPAddress.Parse(this.IPAddress);
			var tempres = System.Net.IPAddress.Any;
			tcpserver.Start(ip, this.Port);
			Console.WriteLine("server start.....");
		}
	}
}
