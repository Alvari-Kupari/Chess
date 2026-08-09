namespace Chess.Move;

public record Coordinate(int X, int Y)
{
    public Coordinate(string algebraic) : this(
        algebraic[0] - 'a',
        8 - int.Parse(algebraic[1].ToString()))
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