using System.Diagnostics;
using System.Text.Json.Nodes;
using OpenQA.Selenium;
using WordleApi.helpers;
using WordleApi.wordle.elements.gameboard;
using WordleApi.wordle.elements.keyboard;

namespace WordleApi.wordle;

public class WordleGame
{
    public IWebDriver Driver { get; }
    public GameBoard Board { get; }
    public Keyboard Keyboard { get; }

    public WordleGame(IWebDriver driver, string url = NyTimesWordleXPaths.Url)
    {
        driver.Url = url;
        driver.Navigate().GoToUrl(url);
        Driver = driver;
        var boardEl = driver.FindElement(By.XPath(NyTimesWordleXPaths.Board));
        var keyboardEl = driver.FindElement(By.XPath(NyTimesWordleXPaths.Keyboard));
        Board = new GameBoard(boardEl);
        Keyboard = new Keyboard(keyboardEl);
    }

    public void ClosePrompt()
    {
        var prompt = Driver.FindElement(By.XPath(NyTimesWordleXPaths.ClosePromptButton));
        Debug.Assert(prompt != null && prompt.GetAttribute("class").Contains("close"));
        prompt.Click();
    }

    public static string[] ReadAllWords()
    {
        var words = JsonArray
            .Parse(File.ReadAllText(@"C:\Users\lando\RiderProjects\Wordle\WordleApi\wordle\DefaultWordsJsonArray.json"))
            .AsArray()
            .Select(n => n.ToString())
            .Select(word => word.Trim())
            .Select(word => word.ToUpper())
            .Where(word => !string.IsNullOrWhiteSpace(word))
            .ToArray();
        words.AsSpan().Shuffle(Random.Shared); // Avoid ordered words
        return words;
    }
}