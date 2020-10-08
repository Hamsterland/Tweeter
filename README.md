# Tweeter
Tweeter is a Discord bot written in C# that mocks Tweets.

## Building
```
git pull https://github.com/Hamsterland/Tweeter.git
cd Tweeter
rename appsettings.default.json appsettings.json (Windows)
mv appsettings.default.json appsettings.json (Linux)
```
Insert your bot token and desired prefix into the settings file.
```
dotnet restore
dotnet run -c Release
```
## Features
* Accurate recreation of a Twitter embed
* Randomised like and rewtweet counts

## Example
![img](https://i.imgur.com/I8PdUHG.png)

## Commands
* `.tweet (t) <message>` 
* `.help (h)`
* `.ping` 
* `.about`

## Roadmap
* Support Tweet like and retweet customisation
* Integrate database features
* Implement a `CommandExecutedListener`
