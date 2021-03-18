# Chat Challenge

This is an API with the fundamentals things that need a Chat Room, It allows you to create users, send a broadcast message to the user's chat room, and allows consulting a stock service through a Bot. 

## Installation
First, you need to configure your Database Settings. Go to the file named appsettings.json in the main project (ChatChallenge).

```bash
 "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ChatChallenge;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
```

**Set ChatChallenge as your Startup project.**

For setting it, right click on the project and  then click Set as Startup Project

![image](https://user-images.githubusercontent.com/17713455/111626691-b3835300-87c4-11eb-8c25-21e0d5a3b71f.png)


**Use the Package Console Manager to create the Database and its tables.**

Set ChatChallenge.Model as the console default project and run this command:

```bash
Add-Migration init
```
After run the command, it would happen: 

![image](https://user-images.githubusercontent.com/17713455/111626468-6e5f2100-87c4-11eb-8698-d1205bb9c83e.png)

Now, Run the migration.

```bash
Update-Database
```
Second, you need to configure your Database Settings but now in the project named ChatChallenge.Bus. Go to the file named appsettings.json and change it.

```bash
 "NServiceBusConfig": {
    "SenderName": "ChatChallengeBus",
    "PublisherName": "ChatChallengeA",
    "ConnectionString": "Server=.;Database=ChatChallenge;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
```
Also, you need to configure what is the client URL that can make request to your API and to configure what is your current API URL. Go to the file named appsettings.json in the main project (ChatChallenge).
```bash
 "AppSettings": {
    "ClientUrls": [ "http://localhost:8080" ],
    "ApiUrl": "https://localhost:44368"
  },
```

## Usage

1. Set as Startup Projects: ChatChallenge and ChatChallenge.Bus

For setting it, right click on the solution and  then click Set Startup Projects

![image](https://user-images.githubusercontent.com/17713455/111631029-52aa4980-87c9-11eb-9bd6-3054d5e07c92.png)

Set the projects actions to Start:

![image](https://user-images.githubusercontent.com/17713455/111631383-b59be080-87c9-11eb-9c62-d27574d89e55.png)


2. Now feel free to run the solution. 

## Additional features

1. .NET identity for users authentication

2. Handle messages that are not understood or any exceptions raised within the bot. 
 
