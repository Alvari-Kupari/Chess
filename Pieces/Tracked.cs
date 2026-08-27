namespace Chess.Pieces;

public abstract class TrackedPiece(Colour colour) : ChessPiece(colour)
{
    public bool HasMoved {get; set;} = false;
}