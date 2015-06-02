using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;

public class MainSession
{
	private static MainSession mainSession;
	private Session session;

	private MainSession ()
	{
		ManualResetEvent joinDone = new ManualResetEvent (true);

		this.session = new Session (
			IPAddress.Parse ("192.168.1.74"),
			27015,
			AddressFamily.InterNetwork,
			ProtocolType.Tcp
		);

		this.session.Connect (ref joinDone);
		joinDone.WaitOne ();
	}

	public static MainSession getMainSession()
	{
		if (mainSession == null) {
			mainSession = new MainSession ();
		}

		return mainSession;
	}

	public void Send(string data)
	{
		this.session.Send (data);
	}
}

