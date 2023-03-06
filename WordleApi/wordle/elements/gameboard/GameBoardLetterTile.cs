using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace WordleApi.wordle.elements.gameboard;

public class GameBoardLetterTile
{
    public IWebElement Tile { get; }
    public bool ContainsLetter { get; }
    public GuessResponse Result { get; }

    public GameBoardLetterTile(IWebElement tile)
    {
        Debug.Assert(tile.GetAttribute("class").Contains("tile"));
        Tile = tile;
        var dataState = tile.GetAttribute("data-state");
        Debug.Assert(dataState != null);
        if (dataState.Equals("empty", StringComparison.OrdinalIgnoreCase))
        {
            ContainsLetter = false;
            return;
        }

        var c = char.ToUpper(tile.Text[0]);
        ContainsLetter = true;
        Debug.Assert(char.IsLetter(c));
        if (dataState.Equals("absent", StringComparison.OrdinalIgnoreCase))
        {
            Result = new(LetterColor.Black, c);
        }
        else if (dataState.Equals("present", StringComparison.OrdinalIgnoreCase))
        {
            Result = new(LetterColor.Yellow, c);
        }
        else if (dataState.Equals("correct", StringComparison.OrdinalIgnoreCase))
        {
            Result = new GuessResponse(LetterColor.Green, c);
        }
        else
        {
            throw new ApplicationException($"No valid dataState for {dataState}");
        }
    }

    public static ImmutableArray<GameBoardLetterTile> GetAllLettersFromWordRow(GameBoardRow row)
    {
        return row.Row
            .FindElements(By.XPath("*"))
            .OrderBy(wrapper =>
            {
                var styleString = wrapper.GetAttribute("style");
                var sortingString = Regex.Replace
                    (styleString, @"[^0-9]", string.Empty, RegexOptions.None);
                var sortingValue = int.Parse(sortingString);
                return sortingValue;
            })
            .Select(el => el.FindElements(By.XPath("*"))[0])
            .Select(el => new GameBoardLetterTile(el))
            .ToImmutableArray();
    }
}