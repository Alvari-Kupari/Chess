using Chess.Board;

namespace Chess.Pieces;

class Horse(Colour colour) : ChessPiece(colour)
{
    protected override char WhiteSymbol => '♘';

    protected override char BlackSymbol => '♞';

    public override ISet<Coordinate> GetPossibleMoves(Coordinate source, ChessBoard board)
    {
        return GetMovesInDirection(
            start: source,
            board,
            directions: [
                (new Coordinate(1, 2), 1),
                (new Coordinate(1, -2), 1),
                (new Coordinate(-1, 2), 1),
                (new Coordinate(-1, -2), 1),

                (new Coordinate(2, 1), 1),
                (new Coordinate(2, -1), 1),
                (new Coordinate(-2, 1), 1),
                (new Coordinate(-2, -1), 1),
            ]
        );
    }
}