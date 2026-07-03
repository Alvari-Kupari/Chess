using Chess.Board;

namespace Chess.Pieces;

public abstract class ChessPiece(Colour Colour)
{
    public Colour Colour {get;} = Colour;
    protected abstract char WhiteSymbol {get;}
    protected abstract char BlackSymbol {get;}
    
    protected bool IsEnemy((int, int) coord, ChessBoard board)
    {
        return board[coord]?.Colour != Colour;
    }

    protected ISet<(int, int)> GetMovesInDirection(
        (int, int) start, 
        ChessBoard board,
        params ((int, int), int)[] directions)
    {
        var moves = new HashSet<(int, int)>();

        foreach (var ((dx, dy), maxIterations) in directions)
        {
            int i = 0;
            var coord = (start.Item1 + dx, start.Item2 + dy);
            while (i < maxIterations && !board.IsOutsideBounds(coord))
            {
                if (board.IsOccupied(coord))
                {
                    if (IsEnemy(coord, board))
                    {
                        moves.Add(coord);
                    }
                    break;
                }
                moves.Add(coord);
                coord = (coord.Item1 + dx, coord.Item2 + dy);
                i++;
            }
        }

        return moves;

    }
    /// <summary>
    /// Gives information to the piece as needed. This is useful for the rook and pawn and king, for castling and en-paissant mechanics.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="board"></param>
    public virtual void OnMove((int, int) from, (int, int) to, ChessBoard board)
    {
    }

    public abstract ISet<(int, int)> GetPossibleMoves((int, int) source, ChessBoard board); 


    public char Symbol => Colour == Colour.BLACK ? BlackSymbol : WhiteSymbol;
}