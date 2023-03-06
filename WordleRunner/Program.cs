// See https://aka.ms/new-console-template for more information

using OpenQA.Selenium.Chrome;
using WordleApi.wordle;

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