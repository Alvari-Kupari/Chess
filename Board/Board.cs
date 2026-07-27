using System.Collections;
using System.Text;
using Chess.Pieces;

namespace Chess.Board;

public class ChessBoard(ChessBoardInitializer initializer) : IEnumerable<ChessPiece>
{
    private readonly ChessPiece?[,] board = initializer.InitBoard();


    public ChessPiece? this[(int, int) coord]
    {
        get => board[coord.Item1, coord.Item2];
        set => board[coord.Item1, coord.Item2] = value;
    }

    public bool IsOccupied((int, int) coord) {
        return this[coord] is not null;
    }


    public bool IsOutsideBounds((int, int) coord)
    {
        return coord.Item1 < 0 
            || coord.Item1 >= board.GetLength(0) 
            || coord.Item2 < 0 
            || coord.Item2 >= board.GetLength(1);
    }

    public (int, int)? FindPiece(ChessPiece needle) {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                var piece = board[i, j];
                if (piece == needle) {
                    return (i, j);
                }

            }
        }
        return null;
    }

    public override string ToString()
    {
        StringBuilder sb = new();

        for (int i = 0; i < board.GetLength(0); i++)
        {
            StringBuilder row = new();
            for (int j = 0; j < board.GetLength(1); j++)
            {
                var piece = board[i, j];
                row.Append(piece?.Symbol ?? ' ');
            }
            sb.Append(row);
            sb.Append('\n');
        }
        return sb.ToString();
    }

    public IEnumerator<ChessPiece> GetEnumerator()
    {
        foreach(var piece in board) {
            if (piece is null) continue;
            yield return piece;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

