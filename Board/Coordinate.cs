namespace Chess.Board;

public record Coordinate(int X, int Y)
{
    public Coordinate(string algebraic) : this(
        8 - int.Parse(algebraic[1].ToString()),
        algebraic[0] - 'a'
    )
    {
    }

    public static Coordinate operator +(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.X + b.X, a.Y + b.Y);
    }

    public static Coordinate operator -(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.X - b.X, a.Y - b.Y);
    }

    public static Coordinate operator *(Coordinate a, int scalar)
    {
        return new Coordinate(a.X * scalar, a.Y * scalar);
    }
}