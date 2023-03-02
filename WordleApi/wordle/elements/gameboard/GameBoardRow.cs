using System.Diagnostics;
using OpenQA.Selenium;

namespace WordleApi.wordle.elements.gameboard;

public class GameBoardRow
{
    public IWebElement Row { get; }
    public IEnumerable<GameBoardLetterTile> LetterTiles { get; }
    private string? _word;
    public string Word => _word ??= string.Concat(LetterTiles);

    public GameBoardRow(IWebElement row)
    {
        Debug.Assert(row.GetAttribute("class").Contains("row"));
        Row = row;
        _word = null;
        LetterTiles = GameBoardLetterTile.GetAllLettersFromWordRow(this);
    }

    public static IEnumerable<GameBoardRow> GetAllRowsFromGameBoard(GameBoard board)
    {
        return board.Board
            .FindElements(By.XPath("*"))
            .Select(el => new GameBoardRow(el));
    }
}