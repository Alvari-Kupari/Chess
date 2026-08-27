namespace Chess.Game;

using Chess.Board;
using Chess.Game.Actions;
using Chess.Pieces;

class ChessGame
{
    private Colour turn;
    public bool IsOver {get; private set;}
    public ChessBoard Board {get;}
    private readonly ChessBoardInitializer initializer = new();


    public ChessGame()
    {
        Board = new(initializer);
        turn = Colour.WHITE;
        IsOver = false;
    }

    public TurnResult HandleTurn(IAction action)
    {
        var result = action.Execute(Board, turn);

        if (!result.IsError()) turn = turn.Opposite();
        if (result.IsGameOver()) IsOver = true;
        return result;

    }

}