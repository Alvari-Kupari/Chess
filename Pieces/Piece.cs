using Chess.Board;
using Chess.Move;

namespace Chess.Pieces;

public abstract class ChessPiece(Colour Colour)
{
    public Colour Colour {get;} = Colour;
    protected abstract char WhiteSymbol {get;}
    protected abstract char BlackSymbol {get;}
    
    protected bool IsEnemy(Coordinate coord, ChessBoard board)
    {
        return board[coord]?.Colour != Colour;
    }

    protected ISet<Coordinate> GetMovesInDirection(
        Coordinate start, 
        ChessBoard board,
        params (Coordinate, int)[] directions)
    {
        var moves = new HashSet<Coordinate>();

        foreach (var (dxy, maxIterations) in directions)
        {
            int i = 0;
            var coord = start + dxy;
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
                coord += dxy;
                i++;
            }
        }

        return moves;

    }

    public abstract ISet<Coordinate> GetPossibleMoves(Coordinate source, ChessBoard board); 


    public char Symbol => Colour == Colour.BLACK ? BlackSymbol : WhiteSymbol;
}