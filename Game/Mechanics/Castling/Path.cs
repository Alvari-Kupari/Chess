using Chess.Move;
using Chess.Pieces;

namespace Chess.Game.Mechanics.Castling;

public class CastlingPath
{
    private readonly List<Coordinate> castlingPath; // all squares between king and rook inclusive (starting from king)

    public CastlingPath(Coordinate kingLocation, Coordinate rookLocation)
    {
        castlingPath = [];
        Coordinate castlingDirection = rookLocation.Y > kingLocation.Y ? 
            new Coordinate(0, 1) : 
            new Coordinate(0, -1);

        for (Coordinate coord = kingLocation; coord != rookLocation; coord += castlingDirection)
        {
            castlingPath.Add(coord);
        }
    }

    public List<Coordinate> GetKingsPath()
    {
        return castlingPath[..3];
    }

    public List<Coordinate> GetRequiredEmptySquares()
    {
        return castlingPath[1..];
    }

    public Coordinate GetKingsDestination()
    {
        return castlingPath[2];
    }

    public Coordinate GetRooksDestination()
    {
        return castlingPath[1];
    }
}