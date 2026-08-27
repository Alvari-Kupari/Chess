using Chess.Board;
using Chess.Pieces;

namespace Chess.Game.Actions;

public interface IAction
{
    public abstract TurnResult Execute(ChessBoard board, Colour turn);
}