namespace WordleApi.wordle;

public record GuessResponse
{
    public readonly LetterColor Color;
    public readonly char Letter;

    public GuessResponse(LetterColor color, char letter)
    {
        Color = color;
        Letter = letter;
    }
}