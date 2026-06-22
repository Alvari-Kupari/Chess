using Chess.Board;

namespace Chess.Pieces;

class Queen(Colour colour) : ChessPiece(colour)
{
    protected override char WhiteSymbol => '♕';

    protected override char BlackSymbol => '♛';

    public override ISet<(int, int)> GetPossibleMoves((int, int) source, ChessBoard board)
    {
        return GetMovesInDirection(
            start: source,
            board,
            directions: [
                // linear
                ((0, 1), int.MaxValue),
                ((0, -1), int.MaxValue),
                ((-1, 0), int.MaxValue),
                ((1, 0), int.MaxValue),

                // diagonal
                ((1, 1), int.MaxValue),
                ((1, -1), int.MaxValue),
                ((-1, 1), int.MaxValue),
                ((-1, -1), int.MaxValue),
            ]
        );
    }
}