using Chess.Board;
using Chess.Pieces;

namespace Chess.Game.Actions;

public interface IAction
{
    public abstract void Execute(ChessBoard board, Colour turn);
}