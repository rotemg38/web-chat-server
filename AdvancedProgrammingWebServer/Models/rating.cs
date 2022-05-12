using System;

public class Rating
{
	int rate;
	string feedback;
	User user;
	public Rating(int rate, string feedback, User user)
    {
		this.rate = rate;
		this.feedback = feedback;
		this.user = user;
    }
}
