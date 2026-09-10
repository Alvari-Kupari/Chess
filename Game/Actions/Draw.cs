using Chess.Board;
using Chess.Pieces;

namespace Chess.Game.Actions;

public class DrawAction : IAction
{
    public TurnResult Execute(ChessBoard board, Colour turn)
    {
        return TurnResult.STALEMATE;
    }
}