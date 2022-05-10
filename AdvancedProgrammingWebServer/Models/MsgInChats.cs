using System;

namespace AdvancedProgrammingWebServer.Models
public class MsgInChats
{
	private int msgId;
	private Message msg;
	private User from;
	private User to;

	MsgInChats() { }

	MsgInChats(int id, Message msg, User from , User to)
    {
		this.msgId = id;
		this.msg = msg;
		this.from = from;
		this.to = to;
    }

	

}
