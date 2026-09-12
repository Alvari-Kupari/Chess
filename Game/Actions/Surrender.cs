using Chess.Board;
using Chess.Pieces;

namespace Chess.Game.Actions;

public class SurrenderAction(Colour sideSurrendering) : IAction
{
    public Colour SideSurrendering {get;} = sideSurrendering;
    public TurnResult Execute(ChessBoard board, Colour turn)
    {
        return TurnResult.SURRENDER;
    }   
}
