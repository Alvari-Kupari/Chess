using System.Text.RegularExpressions;
using Chess.Board;
using Chess.Game.Actions;
using Chess.Pieces;

namespace Chess.PGN;

public class Parser(ChessBoard board)
{
    private readonly static Dictionary<char, Type> PieceTypes = new()
    {
        ['R'] = typeof(Rook),
        ['Q'] = typeof(Queen),
        ['B'] = typeof(Bishop),
        ['N'] = typeof(Horse),
        ['K'] = typeof(King),
        ['P'] = typeof(Pawn)
    };

    private readonly static Dictionary<char, Type> PawnPromotions = new()
    {
        ['R'] = typeof(Rook),
        ['Q'] = typeof(Queen),
        ['B'] = typeof(Bishop),
        ['N'] = typeof(Horse),
    };

    public IAction Parse(string pgn, Colour turn)
    {
        pgn = pgn.Trim();

        if (pgn == "1-0")
        {
            return new SurrenderAction(Colour.BLACK);
        } else if (pgn == "0-1")
        {
            return new SurrenderAction(Colour.WHITE);
        } else if (pgn == "1/2-1/2")
        {
            return new DrawAction();
        }

        if (pgn.StartsWith("O-O"))
        {
            return new CastlingAction(turn, pgn == "O-O-O");
        }

        bool isCheck = false;
        bool isCheckMate = false;

        if (pgn[^1] == '+')
        {
            isCheck = true;
            pgn = pgn[..^1];
        } else if (pgn[^1] == '#')
        {
            isCheckMate = true;
            pgn = pgn[..^1];
        }
        Type? pawnPromoted = null;

        if (Regex.IsMatch(pgn[^2..], @"=[A-Z]{1}\z"))
        {
            pawnPromoted = PawnPromotions[pgn[^1]];
        }

        Coordinate to = new(pgn[^2..]);
        pgn = pgn[..^2];

        bool isCapture = false;
        if (pgn.Length > 0 && pgn[^1] == 'x')
        {
            isCapture = true;
            pgn = pgn[..^1];
        }

        char? specifiedFile = null;
        int? specifiedRank = null;

        if (pgn.Length > 0 && char.IsNumber(pgn[^1])) {
            specifiedRank = pgn[^1];
            pgn = pgn[..^1];
        } else if (pgn.Length > 0 && char.IsLower(pgn[^1]))
        {
            specifiedFile = pgn[^1];
            pgn = pgn[..^1];
        }

        char pieceChar = 'P';

        if (pgn.Length > 0)
        {
            pieceChar = pgn[^1];
            pgn = pgn[..^1];
        }

        Type pieceType = PieceTypes[pieceChar];

        return new PGNMove(
            isCheck, 
            isCheckMate, 
            to, 
            isCapture, 
            specifiedFile, 
            specifiedRank, 
            pieceType,
            pawnPromoted).GetAction(board);
    }
}