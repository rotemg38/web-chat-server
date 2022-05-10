using System;

namespace AdvancedProgrammingWebServer.Models

public class Message
{
	private string type;
	private string text;
	private string date;
	private string mediaSrc;

	public Message(Message msg) {
		this.type = msg.type;
		this.text = msg.text;
		this.date = msg.date;
		this.mediaSrc = msg.mediaSrc;
	}

	public Message(string type, string text, string date, string mediaSRc)
    {
		this.type = type;
		this.text = text;
		this.date = date;
		this.mediaSrc = mediaSRc;
    }
	public Message(string type, string text, string date)
	{
		this.type = type;
		this.text = text;
		this.date = date;
		this.mediaSrc = "";
	}

}
