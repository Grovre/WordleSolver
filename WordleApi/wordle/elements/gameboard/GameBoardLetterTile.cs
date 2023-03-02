using System.Collections.Immutable;
using System.Diagnostics;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace WordleApi.wordle.elements.gameboard;

public class GameBoardLetterTile
{
    public IWebElement Tile { get; }

    public GameBoardLetterTile(IWebElement tile)
    {
        Debug.Assert(tile.GetAttribute("class").Contains("tile"));
        Tile = tile;
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