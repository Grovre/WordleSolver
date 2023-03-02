using System.Collections.Immutable;
using System.Diagnostics;
using OpenQA.Selenium;

namespace WordleApi.wordle.elements.keyboard;

public class KeyboardRow
{
    public IWebElement Row { get; }
    public IEnumerable<KeyboardKey> Keys { get; }

    public KeyboardRow(IWebElement row)
    {
        Debug.Assert(row.GetAttribute("class").Contains("row"));
        Row = row;
        Keys = KeyboardKey.GetKeysFromRow(this);
    }

    public static ImmutableArray<KeyboardRow> GetRowsFromKeyboard(Keyboard keyboard)
    {
        return keyboard.Board
            .FindElements(By.XPath("*"))
            .Select(el => new KeyboardRow(el))
            .ToImmutableArray();
    }

    public static IEnumerable<KeyboardKey> GetAllKeysFromKeyboard(Keyboard keyboard)
    {
        return keyboard.KeyRows.SelectMany(row => row.Keys);
    }
}