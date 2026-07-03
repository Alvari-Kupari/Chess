using Chess.Board;

namespace Chess.Pieces;

public class King(Colour colour) : ChessPiece(colour)
{
    protected override char WhiteSymbol => '♔';

    protected override char BlackSymbol => '♚';

    public override ISet<(int, int)> GetPossibleMoves((int, int) source, ChessBoard board)
    {
        return GetMovesInDirection(
            start: source,
            board,
            directions: [
                // linear
                ((0, 1), 1),
                ((0, -1), 1),
                ((-1, 0), 1),
                ((1, 0), 1),

                // diagonal
                ((1, 1), 1),
                ((1, -1), 1),
                ((-1, 1), 1),
                ((-1, -1), 1),
            ]
        );
    }
}