using Chess.Board;
using Chess.Pieces;

namespace Chess.Game;

public class PieceMove : ChessTransaction
{
    public Coordinate To { get; }
    public Coordinate From { get; }
    public ChessPiece? ToPiece { get; }
    public ChessPiece? FromPiece {get; }
    private readonly bool hadPreviouslyMoved;
    private readonly ChessBoard board;

    public PieceMove(Coordinate from, Coordinate to, ChessBoard board)
    {
        From = from;
        To = to;
        this.board = board;

        ToPiece = board[to];
        FromPiece = board[from];

        board[to] = board[from];
        board[from] = null;

        if (FromPiece is TrackedPiece trackedPiece)
        {
            hadPreviouslyMoved = trackedPiece.HasMoved;
            trackedPiece.HasMoved = true;
        }
    }

    protected override void Rollback()
    {
        board[From] = board[To];
        board[To] = ToPiece;
        
        if (FromPiece is TrackedPiece trackedPiece)
        {
            trackedPiece.HasMoved = hadPreviouslyMoved;
        }
    }
}