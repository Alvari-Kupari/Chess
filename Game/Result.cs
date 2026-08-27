namespace Chess.Game;

public enum TurnResult
{
    INVALID_MOVE,
    NORMAL,
    CHECK,
    CHECKMATE,
    STALEMATE
}

public static class MessageMeanings
{
    public static bool IsError(this TurnResult message)
    {
        return message.ToString().ToLower().StartsWith("invalid");
    }

    public static bool IsGameOver(this TurnResult message)
    {
        return message == TurnResult.CHECKMATE || message == TurnResult.STALEMATE;
    }
}