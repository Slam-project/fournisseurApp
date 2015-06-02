using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class StateObject {

	public Socket workSocket = null;
	public const int Buffersize = 1024;
	public byte[] buffer = new byte[Buffersize];
	public StringBuilder sb = new StringBuilder();
}
