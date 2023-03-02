using System.Diagnostics;
using OpenQA.Selenium;

namespace WordleApi.wordle.elements.gameboard;

public class GameBoard
{
    public IWebElement Board { get; }
    public IEnumerable<GameBoardRow> BoardRows { get; }

    public GameBoard(IWebElement board)
    {
        Debug.Assert(board.GetAttribute("class").Contains("board"));
        Board = board;
        BoardRows = GameBoardRow.GetAllRowsFromGameBoard(this);
    }
}