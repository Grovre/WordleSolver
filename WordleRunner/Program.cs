// See https://aka.ms/new-console-template for more information

using OpenQA.Selenium.Chrome;
using WordleApi.wordle;
using WordleApi.wordle.elements.gameboard;

Console.WriteLine(string.Join('\n', WordleGame.ReadAllWords()));

var options = new ChromeOptions
{
    BinaryLocation = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe"
};

using var driver = new ChromeDriver(options);
var game = new WordleGame(driver);
Thread.Sleep(2000);
game.ClosePrompt();
game.Keyboard.Type("Audio", TimeSpan.FromSeconds(1));
Thread.Sleep(1000);
game.Refresh();
var tiles = new List<GameBoardLetterTile>();
foreach (var row in game.Board.BoardRows)
{
    tiles.AddRange(row.LetterTiles);
}

foreach (var tile in tiles)
{
    Console.WriteLine($"Valid Letter? {tile.ContainsLetter}");
    if (tile.ContainsLetter)
    {
        Console.WriteLine($"Letter: {tile.Result.Letter}, Color: {tile.Result.Color}");
    }
}