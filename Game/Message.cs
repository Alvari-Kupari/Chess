namespace Chess.Game;

public enum ChessMessage
{
    ERROR_OUT_OF_BOUNDS,
    CHECK_BLACK,
    CHECK_WHITE,
    CHECKMATE_BLACK,
    CHECKMATE_WHITE,
    ERROR_WHITE_CANT_MOVE_A_BLACK_PIECE,
    ERROR_BLACK_CANT_MOVE_A_WHITE_PIECE,
    ERROR_SOURCE_IS_EMPTY,
    ERROR_WHITE_INVALID_MOVE_BECAUSE_CHECK, // either not getting yourself out of check, or putting yourself into check
    ERROR_BLACK_INVALID_MOVE_BECAUSE_CHECK,
    ERROR_INVALID_MOVE, // general purpose like bishop trying to move vertically, eating own colour, etc

}

public static class MessageMeanings
{
    public static bool IsError(this ChessMessage message)
    {
        return message.ToString().ToLower().StartsWith("error");
    }

    public static bool IsCheckMate(this ChessMessage message)
    {
        return message == ChessMessage.CHECKMATE_BLACK || message == ChessMessage.CHECKMATE_WHITE;
    }

    public static bool IsCheck(this ChessMessage message)
    {
        return message == ChessMessage.CHECK_BLACK || message == ChessMessage.CHECK_WHITE;
    }
}