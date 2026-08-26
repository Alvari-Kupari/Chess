namespace Chess.Game;

public enum ChessMessage
{
    ERROR_OUT_OF_BOUNDS,
    CHECK,
    CHECKMATE,
    ERROR_CANT_MOVE_ENEMY,
    ERROR_SOURCE_IS_EMPTY,
    ERROR_CHECKING_SELF,
    ERROR_MUST_GET_OUT_OF_CHECK,
    ERROR_INVALID_MOVE, // general purpose like bishop trying to move vertically, eating own colour, etc

}

public static class MessageMeanings
{
    public static bool IsError(this ChessMessage message)
    {
        return message.ToString().ToLower().StartsWith("error");
    }
}