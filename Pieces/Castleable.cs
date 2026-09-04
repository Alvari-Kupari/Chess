namespace Chess.Pieces;

public abstract class CastleablePiece(Colour colour) : ChessPiece(colour)
{
    public bool HasMoved {get; set;} = false;
}