using System.Collections.Immutable;
using System.Diagnostics;
using OpenQA.Selenium;

namespace WordleApi.wordle.elements.keyboard;

public class KeyboardKey
{
    public IWebElement Key { get; }
    public char Char { get; }
    public bool IsEnterKey { get; }
    public bool IsBackspaceKey { get; }

    public KeyboardKey(IWebElement key)
    {
        Debug.Assert(key.GetAttribute("class").Contains("key"), $"Key class is actually: {key.GetAttribute("class")}");
        Key = key;
        var str = key.Text;
        if (string.IsNullOrWhiteSpace(str))
            str = key.GetAttribute("aria-label");
        Debug.Assert(str != null);

        if (str.Equals("enter", StringComparison.OrdinalIgnoreCase))
        {
            Char = (char)1;
            IsEnterKey = true;
        }
        else if (str.Equals("backspace", StringComparison.OrdinalIgnoreCase))
        {
            Char = (char)2;
            IsBackspaceKey = true;
        }
        else
        {
            var c = str[0];
            Debug.Assert(char.IsLetter(c));
            Char = char.ToUpper(c);
        }
    }

    public static ImmutableArray<KeyboardKey> GetKeysFromRow(KeyboardRow row)
    {
        return row.Row
            .FindElements(By.XPath("*"))
            .Where(el => el.TagName.Equals("button", StringComparison.OrdinalIgnoreCase))
            .Select(el => new KeyboardKey(el))
            .ToImmutableArray();
    }

    public static Dictionary<char, KeyboardKey> MapCharToKey(IEnumerable<KeyboardKey> keys)
    {
        return keys.ToDictionary(keyboardKey => keyboardKey.Char);
    }

    public void Click()
    {
        Key.Click();
    }
}