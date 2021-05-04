using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using GameClient;
using Newtonsoft.Json;

namespace GameClientNamespace
{
    class Socket
    {
        private TcpClient socketConnection;
        private Thread clientReceiveThread;

		public bool ConnectToTcpServer()
		{
			try
			{
				if (socketConnection == null)
				{
					socketConnection = new TcpClient("localhost", 8052);
					clientReceiveThread = new Thread(new ThreadStart(ListenForData));
					clientReceiveThread.IsBackground = true;
					clientReceiveThread.Start();

				}
				return true;
			}
			catch (Exception e)
			{
				Console.WriteLine("Hiba a csatlakozásban" + e);
				return false;
			}
		}

		private void ListenForData()
		{
			try
			{				
				Byte[] bytes = new Byte[1024];
				while (true)
				{	
					using (NetworkStream stream = socketConnection.GetStream())
					{
						int length;				
						while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
						{
							stream.WriteTimeout = 500;
							var incommingData = new byte[length];
							Array.Copy(bytes, 0, incommingData, 0, length);			
							string serverMessage = Encoding.ASCII.GetString(incommingData);
							Console.WriteLine("Kapott üzenet a szervertől: " + serverMessage);

							SingletonGameState.GetInstance().SetGameState(JsonConvert.DeserializeObject<GameState>(serverMessage));
							SendMessage(JsonConvert.SerializeObject(SingletonGameState.GetInstance().GetUserCommand()));
							SingletonGameState.GetInstance().GetUserCommand().deleteCommands();
							stream.Flush();
						}

						
					}
				}
			}
			catch (SocketException socketException)
			{
				Console.WriteLine("Socket hiba: " + socketException);
			}
		}

		public void SendMessage(String message)
		{
			if (socketConnection == null)
			{
				return;
			}
			try
			{		
				NetworkStream stream = socketConnection.GetStream();
				if (stream.CanWrite)
				{
					stream.Flush();
					stream.WriteTimeout = 500;
					byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(message);          
					stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
					Console.WriteLine("Kliens küld üzenetet");
				}
			}
			catch (SocketException socketException)
			{
				Console.WriteLine("Socket exception: " + socketException);
			}
		}


	}

	
}
