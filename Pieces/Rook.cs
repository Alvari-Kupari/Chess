using Chess.Board;
using Chess.Move;

namespace Chess.Pieces;

public class Rook(Colour colour) : TrackedPiece(colour)
{
    protected override char WhiteSymbol => '♖';

    protected override char BlackSymbol => '♜';

    public override ISet<Coordinate> GetPossibleMoves(Coordinate source, ChessBoard board)
    {
        return GetMovesInDirection(
            start: source,
            board,
            directions: [
                (new Coordinate(0, 1), int.MaxValue),
                (new Coordinate(0, -1), int.MaxValue),
                (new Coordinate(-1, 0), int.MaxValue),
                (new Coordinate(1, 0), int.MaxValue),
            ]
        );
    }
}