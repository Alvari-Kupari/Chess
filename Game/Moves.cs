using System.Collections;
using Chess.Board;
using Chess.Pieces;

namespace Chess.Game;

public class MoveRegistry: IEnumerable<KeyValuePair<ChessPiece, ISet<Coordinate>>>
{
    private readonly Dictionary<ChessPiece, ISet<Coordinate>> existingMoves;

    public MoveRegistry(ChessBoard board)
    {
        existingMoves = [];

        foreach (var (piece, location) in board)
        {
            if (piece is null) continue;

            existingMoves.Add(piece, piece.GetPossibleMoves(location, board));
        }
    }
    public ISet<Coordinate> GetByColour(Colour colour)
    {
        return existingMoves.Where(entry => entry.Key.Colour == colour).SelectMany(x => x.Value).ToHashSet();
    }

    public IEnumerator<KeyValuePair<ChessPiece, ISet<Coordinate>>> GetEnumerator()
    {
        return existingMoves.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public ISet<Coordinate> this[ChessPiece piece]
    {
        get => existingMoves[piece];
    }


}