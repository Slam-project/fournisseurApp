using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Threading;
using System.Text;
using System.Reflection;


class Session
{
	private ManualResetEvent connectDone;
	private IPAddress address;
	private int port;
	private AddressFamily addressFamily;
	private IPEndPoint iep;
	private Socket client;
	private ProtocolType protocoleType;
	public static string[] separatorCommand = new string[] { "_::_" };
	public static string separatorCommandString = "_::_";
	private string[] separatorSocket = new string[] { "<EOF>" };
	private bool isConnect = false;


	public Session (IPAddress address, int port, AddressFamily addressFamily, ProtocolType protocoleType) {
		this.connectDone = new ManualResetEvent (true);
		this.address = address;
		this.port = port;
		this.addressFamily = addressFamily;
		this.protocoleType = protocoleType;
	}

	public void Connect(ref ManualResetEvent joinDone)
	{
		this.iep = new IPEndPoint (this.address, this.port);
		this.client = new Socket (this.addressFamily, SocketType.Stream, this.protocoleType);

		client.BeginConnect( this.iep, new AsyncCallback(ConnectCallback), this.client);
		this.connectDone.WaitOne ();
		joinDone.Set ();
	}

	/**
	 * Finaly connection between client and server (Auth presently);
	 */
	private void ConnectCallback(IAsyncResult ar)
	{
		try {
			Socket handler = (Socket) ar.AsyncState;

			handler.EndConnect(ar);

			StateObject state = new StateObject();
			state.workSocket = handler;

			handler.BeginReceive(state.buffer, 0, StateObject.Buffersize, 0, new AsyncCallback(readCallback), state);

			this.isConnect = true;
			this.connectDone.Set ();
		} catch (Exception) {
			this.connectDone.Set ();
		}
	}

	/**
	 * Use to send a socket to server
	 */
	public void Send(String data) 
	{
		data = data.Replace ("<EOF>", "< EOF >");
		data += "<EOF>";
		byte[] byteData = Encoding.ASCII.GetBytes (data);
		this.client.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(SendCallback), this.client);
	}

	/**
	 * Finish to send a socket to a client
	 */
	private void SendCallback (IAsyncResult ar)
	{
		Socket client = (Socket)ar.AsyncState;
		client.EndSend (ar);
	}

	/**
	 * Call when client receive a socket from server
	 */
	private void readCallback(IAsyncResult ar)
	{
		StateObject state = (StateObject)ar.AsyncState;
		Socket handler = state.workSocket;

		int read = handler.EndReceive(ar);

		if (read > 0)
		{
			string test = Encoding.ASCII.GetString(state.buffer, 0, read);
			state.sb.Append(test);

			string content = state.sb.ToString();

			string[] commands = content.Split(separatorSocket, StringSplitOptions.None);

			if (commands.Length > 1)
			{
				for (int i = 0; i < commands.Length - 1; i++)
				{
					this.UseCommand(commands[i].ToString());
				}

				state.sb = new StringBuilder(commands[commands.Length - 1]);
			}

			handler.BeginReceive(state.buffer, 0, StateObject.Buffersize, 0, new AsyncCallback(readCallback), state);
		}
	}

	public virtual void UseCommand(string command)
	{

	}
		
	public bool IsConnect()
	{
		return this.isConnect;
	}
}