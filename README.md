# Chat Challenge

This is an API with the fundamentals things that need a Chat Room, It allows you to create users, send a broadcast message to the user's chat room, and allows consulting a stock service through a Bot. 

## Installation
First, you need is configure your Database Settings. Go to the file named appsettings.json in the main project (ChatChallenge).

```bash
 "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ChatChallenge;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
```

Set ChatChallenge as your Startup project.

Use the Package Console Manager to create de Database and its tables.

Set ChatChallenge.Model as the console default project.

```bash
Add-Migration init
```

Run the migration.
```bash
Update-Database
```
Second, you need is configure your Database Settings but now in the project named ChatChallenge.Bus. Go to the file named appsettings.json and change it.

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

2. Now feel free to run the solution. 
