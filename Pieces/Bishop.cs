using Chess.Board;
using Chess.Move;

namespace Chess.Pieces;

class Bishop(Colour colour) : ChessPiece(colour)
{
    protected override char WhiteSymbol => '♗';
    protected override char BlackSymbol => '♝';

    public override ISet<Coordinate> GetPossibleMoves(Coordinate source, ChessBoard board)
    {
        return GetMovesInDirection(
            start: source,
            board,
            directions: [
                // diagonal
                (new Coordinate(1, 1), int.MaxValue),
                (new Coordinate(1, -1), int.MaxValue),
                (new Coordinate(-1, 1), int.MaxValue),
                (new Coordinate(-1, -1), int.MaxValue),
            ]
        );
    }
}