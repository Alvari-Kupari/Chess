using System.Collections;
using System.Text;
using Chess.Pieces;

namespace Chess.Board;

public class ChessBoard(ChessBoardInitializer initializer) : IEnumerable<(ChessPiece, Coordinate)>
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

    public int GetBoundary(int axis)
    {
        return board.GetLength(axis);
    }

    public Coordinate? FindPiece(ChessPiece needle) 
    {
        var (piece, location) = this.FirstOrDefault(x => x.Item1 == needle);
        return piece is null ? null : location;
    }

    public T? FindPiece<T>(Colour? side = null) where T : ChessPiece
    {
        return board.OfType<T>().FirstOrDefault(piece => side is null || piece.Colour == side);
    }

    public ISet<T> FindPieces<T>(Colour? side = null) where T : ChessPiece
    {
        return board.OfType<T>().Where(piece => side is null || piece.Colour == side).ToHashSet();
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

    public IEnumerator<(ChessPiece, Coordinate)> GetEnumerator()
    {
        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                var piece = board[row, col];
                if (piece is null) continue;
                yield return (piece, new Coordinate(row, col));
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

