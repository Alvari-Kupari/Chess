using Chess.Pieces;

namespace Chess.Board;

public class ChessBoardInitializer
{
    private const int BOARD_SIZE = 8;
    public King WhiteKing {get;} = new(Colour.WHITE);
    public King BlackKing {get;} = new(Colour.BLACK);

    public ChessPiece?[,] InitBoard()
    {
        ChessPiece?[,] board = new ChessPiece?[BOARD_SIZE, BOARD_SIZE];

        for (int i = 0; i < BOARD_SIZE; i++)
        {
            board[1, i] = new Pawn(Colour.BLACK);
            board[6, i] = new Pawn(Colour.WHITE);
        }

        board[0, 0] = new Rook(Colour.BLACK);
        board[7, 0] = new Rook(Colour.WHITE);

        board[0, 1] = new Horse(Colour.BLACK);
        board[7, 1] = new Horse(Colour.WHITE);

        board[0, 2] = new Bishop(Colour.BLACK);
        board[7, 2] = new Bishop(Colour.WHITE);

        board[0, 3] = new Queen(Colour.BLACK);
        board[7, 3] = new Queen(Colour.WHITE);

        board[0, 4] = BlackKing;
        board[7, 4] = WhiteKing;

        board[0, 5] = new Bishop(Colour.BLACK);
        board[7, 5] = new Bishop(Colour.WHITE);

        board[0, 6] = new Horse(Colour.BLACK);
        board[7, 6] = new Horse(Colour.WHITE);

        board[0, 7] = new Rook(Colour.BLACK);
        board[7, 7] = new Rook(Colour.WHITE);

        return board;
    }
}