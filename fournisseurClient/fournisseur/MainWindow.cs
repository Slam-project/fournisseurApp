using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void bntConnectClicked (object sender, EventArgs e)
	{
		MainSession mainSession = MainSession.getMainSession ();
		string packet = "Connect"
			+ Session.separatorCommandString
			+ this.login
			+ Session.separatorCommandString
			+ this.password;
		
		mainSession.Send (packet);
	}
}
