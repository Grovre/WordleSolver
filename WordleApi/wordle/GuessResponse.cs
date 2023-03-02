namespace WordleApi.wordle;

public record GuessResponse
{
    public readonly LetterColor Color;
    public readonly char Letter;
}