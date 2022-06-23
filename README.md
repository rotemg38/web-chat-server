# AdvancedProgrammingWeb
This is a Web Server side of our chat application which is part of a bigger project for the course Advanced Programming 2.

This server works with local SQL Server.

## Description
The web server includes the rating page of client side and the API that connect with the client.

The API includes the following **commands**:
- Get messages by chat id
- Get the other user of a chat- the one that is not connected
- Get id chat by username
- Get messages of a chat
- Get chat id by 2 users
- Get all contacts of coneected user
- Get the details of user in the system
- Add new user
- Update details about a user
- Delete a user
- Invite to a new conversation
- Get all messages of user with connected user
- Get the messages of a guven username
- Update message deatils
- Delete a message
- Create new message
- Login and SignIn
- Set connection user

## Technologies
- MVC
- Bootstrap
- HTML
- CSS
- C#
- Sessions
- SignalR
- Entity Framework

## Getting Started

### Installing & Executing program

Download the project and via your prefered IDE run the application.

We recommend on Visual Studios as an excellent IDE therefore we will explain how to run our project on this IDE.

First, set the **connection string** to your sql server in file **ServerDbContext.cs** in Repository project.

Secondly, open the "WebApi" project and right click on its title.

*Debug -> Start new instance*

After running this command the brwoser should open the API with Swagger screen.

Second, open the "WebServerApp" project and right clion on its tilte.

In the same way: Debug -> *Start new instance*

In order to run the project with clients, you'll need to get the client program from "https://github.com/rotemg38/AdvancedProgrammingWeb", enter branch "MainWithServer" and follow the instruction there.

Contributors names:

- Rotem Ghidale 
- Shir Fintsy
