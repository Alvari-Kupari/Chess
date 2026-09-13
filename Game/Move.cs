using Chess.Board;
using Chess.Pieces;

namespace Chess.Game;

public class PieceMove : ChessTransaction
{
    public Coordinate To { get; }
    public Coordinate From { get; }
    public ChessPiece? ToPiece { get; }
    public ChessPiece? FromPiece {get; }
    public ChessPiece? PawnPromoted {get; }
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
        PawnPromoted = pawnPromoted;

        if (FromPiece is CastleablePiece castleablePiece)
        {
            hadPreviouslyMoved = castleablePiece.HasMoved;
        }

        if (FromPiece is Pawn pawn)
        {
            hadPreviouslyMoved = pawn.HasMoved;
            wasPreviouslyEnpassantEatable = pawn.EnPassantEatable;
        }
    }

    protected override void ExecuteTransaction()
    {
        board[To] = PawnPromoted is null ? FromPiece : PawnPromoted;
        board[From] = null;

        if (FromPiece is CastleablePiece castleablePiece)
        {
            castleablePiece.HasMoved = true;
        }

        if (FromPiece is Pawn pawn)
        {
            pawn.HasMoved = true;
            pawn.EnPassantEatable = !hadPreviouslyMoved && Math.Abs(From.X - To.X) == 2;
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