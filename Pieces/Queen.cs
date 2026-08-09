using Chess.Board;
using Chess.Move;

namespace Chess.Pieces;

class Queen(Colour colour) : ChessPiece(colour)
{
    protected override char WhiteSymbol => '♕';

    protected override char BlackSymbol => '♛';

    public override ISet<Coordinate> GetPossibleMoves(Coordinate source, ChessBoard board)
    {
        return GetMovesInDirection(
            start: source,
            board,
            directions: [
                // linear
                (new Coordinate(0, 1), int.MaxValue),
                (new Coordinate(0, -1), int.MaxValue),
                (new Coordinate(-1, 0), int.MaxValue),
                (new Coordinate(1, 0), int.MaxValue),

                // diagonal
                (new Coordinate(1, 1), int.MaxValue),
                (new Coordinate(1, -1), int.MaxValue),
                (new Coordinate(-1, 1), int.MaxValue),
                (new Coordinate(-1, -1), int.MaxValue),
            ]
        );
    }
}