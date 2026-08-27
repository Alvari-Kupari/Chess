using Chess.Board;
using Chess.Move;

namespace Chess.Pieces;

public class King(Colour colour) : TrackedPiece(colour)
{
    protected override char WhiteSymbol => '♔';

    protected override char BlackSymbol => '♚';

    public override ISet<Coordinate> GetPossibleMoves(Coordinate source, ChessBoard board)
    {
        return GetMovesInDirection(
            start: source,
            board,
            directions: [
                // linear
                (new Coordinate(0, 1), 1),
                (new Coordinate(0, -1), 1),
                (new Coordinate(-1, 0), 1),
                (new Coordinate(1, 0), 1),

                // diagonal
                (new Coordinate(1, 1), 1),
                (new Coordinate(1, -1), 1),
                (new Coordinate(-1, 1), 1),
                (new Coordinate(-1, -1), 1),
            ]
        );
    }
}