namespace WordleApi.wordle;

public static class NyTimesWordleXPaths
{
    public const string Url = @"https://www.nytimes.com/games/wordle/index.html";
    public const string ClosePromptButton = "/html/body/div/div/dialog/div/button";
    public const string Board = "//*[@id=\"wordle-app-game\"]/div[1]/div";
    public const string Keyboard = "//*[@id=\"wordle-app-game\"]/div[2]";
}