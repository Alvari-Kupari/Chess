using Chess.Board;

namespace Chess.Pieces;

class Rook(Colour colour) : ChessPiece(colour)
{
    protected override char WhiteSymbol => '♖';

    protected override char BlackSymbol => '♜';

    public override ISet<(int, int)> GetPossibleMoves((int, int) source, ChessBoard board)
    {
        return GetMovesInDirection(
            start: source,
            board,
            directions: [
                ((0, 1), int.MaxValue),
                ((0, -1), int.MaxValue),
                ((-1, 0), int.MaxValue),
                ((1, 0), int.MaxValue),
            ]
        );
    }
}