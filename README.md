# Tweeter
Tweeter is a Discord bot written in C# that mocks Tweets.

## Building
```
git pull https://github.com/Hamsterland/Tweeter.git
cd Tweeter
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
* `.ping` 

## Roadmap
* Support more advanced Tweet customisation. Ex. images, ranges... 
* Integrate database features
* Implement a `CommandExecutedListener`
* Create a help command
