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
    private readonly bool wasPreviouslyEnpassantEatable;
    private readonly ChessBoard board;

    public PieceMove(Coordinate from, Coordinate to, ChessBoard board, ChessPiece? pawnPromoted = null)
    {
        From = from;
        To = to;
        this.board = board;

        ToPiece = board[to];
        FromPiece = board[from];

        board[to] = pawnPromoted is null ? FromPiece : pawnPromoted;
        board[from] = null;

        if (FromPiece is CastleablePiece castleablePiece)
        {
            hadPreviouslyMoved = castleablePiece.HasMoved;
            castleablePiece.HasMoved = true;
        }

        if (FromPiece is Pawn pawn)
        {
            hadPreviouslyMoved = pawn.HasMoved;
            wasPreviouslyEnpassantEatable = pawn.EnPassantEatable;
            pawn.HasMoved = true;

            if (!hadPreviouslyMoved && Math.Abs(from.X - to.X) == 2)
            {
                pawn.EnPassantEatable = true;
            } else
            {
                pawn.EnPassantEatable = false;
            }
        }

    }

    protected override void Rollback()
    {
        board[From] = FromPiece;
        board[To] = ToPiece;
        
        if (FromPiece is CastleablePiece castleablePiece)
        {
            castleablePiece.HasMoved = hadPreviouslyMoved;
        }
        if (FromPiece is Pawn pawn)
        {
            pawn.HasMoved = hadPreviouslyMoved;
            pawn.EnPassantEatable = wasPreviouslyEnpassantEatable;
        }
    }
}