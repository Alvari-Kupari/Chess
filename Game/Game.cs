namespace Chess.Game;

using Chess.Board;
using Chess.Game.Actions;
using Chess.Pieces;

class ChessGame
{
    public Colour Turn {get; private set;}
    public bool IsOver {get; private set;}
    public ChessBoard Board {get;}
    private readonly ChessBoardInitializer initializer = new();


    public ChessGame()
    {
        Board = new(initializer);
        Turn = Colour.WHITE;
        IsOver = false;
    }

    public TurnResult HandleTurn(IAction action)
    {
        var result = action.Execute(Board, Turn);

        if (result.IsGameOver()) IsOver = true;
        if (!IsOver && !result.IsError()) Turn = Turn.Opposite();

        return result;

    }

}