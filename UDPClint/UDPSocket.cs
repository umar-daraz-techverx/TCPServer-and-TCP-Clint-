﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPClint
{
	public class UDPSocket
	{
		private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
		private const int bufSize = 8 * 1024;
		private State state = new State();
		private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
		private AsyncCallback recv = null;

		public class State
		{
			public byte[] buffer = new byte[bufSize];
		}

		public void Client(string address, int port)
		{
			_socket.Connect(IPAddress.Parse(address), port);
		}

		public void Send(string text)
		{
			byte[] data = Encoding.ASCII.GetBytes(text);
			_socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
			{
				State so = (State)ar.AsyncState;
				int bytes = _socket.EndSend(ar);
				Console.WriteLine("SEND: {0}, {1}", bytes, text);
			}, state);
		}

	}
}
