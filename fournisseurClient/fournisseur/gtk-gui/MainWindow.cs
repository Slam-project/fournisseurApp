
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.Fixed fixed3;
	
	private global::Gtk.Entry login;
	
	private global::Gtk.Entry password;
	
	private global::Gtk.Image logo;
	
	private global::Gtk.Button btnConnect;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		this.DefaultWidth = 220;
		this.DefaultHeight = 400;
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.fixed3 = new global::Gtk.Fixed ();
		this.fixed3.Name = "fixed3";
		this.fixed3.HasWindow = false;
		// Container child fixed3.Gtk.Fixed+FixedChild
		this.login = new global::Gtk.Entry ();
		this.login.CanFocus = true;
		this.login.Name = "login";
		this.login.IsEditable = true;
		this.login.InvisibleChar = '●';
		this.fixed3.Add (this.login);
		global::Gtk.Fixed.FixedChild w1 = ((global::Gtk.Fixed.FixedChild)(this.fixed3 [this.login]));
		w1.X = 45;
		w1.Y = 290;
		// Container child fixed3.Gtk.Fixed+FixedChild
		this.password = new global::Gtk.Entry ();
		this.password.CanFocus = true;
		this.password.Name = "password";
		this.password.IsEditable = true;
		this.password.InvisibleChar = '●';
		this.fixed3.Add (this.password);
		global::Gtk.Fixed.FixedChild w2 = ((global::Gtk.Fixed.FixedChild)(this.fixed3 [this.password]));
		w2.X = 45;
		w2.Y = 320;
		// Container child fixed3.Gtk.Fixed+FixedChild
		this.logo = new global::Gtk.Image ();
		this.logo.Name = "logo";
		this.logo.Pixbuf = new global::Gdk.Pixbuf (global::System.IO.Path.Combine (global::System.AppDomain.CurrentDomain.BaseDirectory, "./logo.jpg"));
		this.fixed3.Add (this.logo);
		// Container child fixed3.Gtk.Fixed+FixedChild
		this.btnConnect = new global::Gtk.Button ();
		this.btnConnect.CanFocus = true;
		this.btnConnect.Name = "btnConnect";
		this.btnConnect.UseUnderline = true;
		this.btnConnect.Label = global::Mono.Unix.Catalog.GetString ("Connexion");
		this.fixed3.Add (this.btnConnect);
		global::Gtk.Fixed.FixedChild w4 = ((global::Gtk.Fixed.FixedChild)(this.fixed3 [this.btnConnect]));
		w4.X = 90;
		w4.Y = 362;
		this.Add (this.fixed3);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.btnConnect.Clicked += new global::System.EventHandler (this.bntConnectClicked);
	}
}
