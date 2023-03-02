using System.Diagnostics;
using OpenQA.Selenium;

namespace WordleApi.wordle.elements.keyboard;

public class Keyboard
{
    public IWebElement Board { get; }
    public IEnumerable<KeyboardRow> KeyRows { get; }

    public Keyboard(IWebElement keyboard)
    {
        Debug.Assert(keyboard.GetAttribute("class").Contains("board"));
        Board = keyboard;
        KeyRows = KeyboardRow.GetRowsFromKeyboard(this);
    }

    public void Type(ReadOnlySpan<char> chars, TimeSpan delayBetweenInput)
    {
        var keyboardMap = KeyboardKey.MapCharToKey(KeyboardRow.GetAllKeysFromKeyboard(this));
        for (var i = 0; i < chars.Length; i++)
        {
            Task.Delay(delayBetweenInput).Wait();

            var c = char.ToUpper(chars[i]);
            Debug.Assert(keyboardMap.ContainsKey(c));
            keyboardMap[c].Click();
        }

        Task.Delay(delayBetweenInput).Wait();
        keyboardMap[(char)1].Click(); // Enter
    }
}