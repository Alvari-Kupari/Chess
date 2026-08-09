using System.Collections;
using System.Text;
using Chess.Move;
using Chess.Pieces;

namespace Chess.Board;

public class ChessBoard(ChessBoardInitializer initializer) : IEnumerable<ChessPiece>
{
    private readonly ChessPiece?[,] board = initializer.InitBoard();


    public ChessPiece? this[Coordinate coord]
    {
        get => board[coord.X, coord.Y];
        set => board[coord.X, coord.Y] = value;
    }

    public bool IsOccupied(Coordinate coord) {
        return this[coord] is not null;
    }

    public bool IsOutsideBounds(Coordinate coord)
    {
        var (x, y) = coord;
        return x < 0 
            || x >= board.GetLength(0) 
            || y < 0 
            || y >= board.GetLength(1);
    }

    public Coordinate? FindPiece(ChessPiece needle) {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                var piece = board[i, j];
                if (piece == needle) {
                    return new Coordinate(i, j);
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

