namespace Chess.Pieces;

public enum Colour
{
    BLACK,
    WHITE
}

public static class ColourExtensions
{
    public static Colour Opposite(this Colour colour)
    {
        return colour == Colour.BLACK ? Colour.WHITE : Colour.BLACK;
    }
}