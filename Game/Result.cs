namespace Chess.Game;

public enum TurnResult
{
    ERROR_OUTSIDE_BOUNDS,
    ERROR_BAD_PROMOTION,
    ERROR_EMPTY_SOURCE,
    ERROR_END_PAWN_MUST_PROMOTE,
    ERROR_PIECE_CANT_MOVE_THERE,
    ERROR_CANT_CAPTURE_OWN_COLOUR,
    ERROR_SELF_DISCOVERED_ATTACK,
    ERROR_KING_UNCASTLEABLE,
    ERROR_ROOK_UNCASTLEABLE,
    ERROR_CASTLING_PATH_BLOCKED,
    ERROR_CASTLING_PATH_CHECKED,
    NORMAL,
    CHECK,
    CHECKMATE,
    STALEMATE,
    SURRENDER,
}

public static class MessageMeanings
{
    public static bool IsError(this TurnResult message)
    {
        return message.ToString().ToLower().StartsWith("error");
    }

    public static bool IsGameOver(this TurnResult message)
    {
        return message == TurnResult.CHECKMATE || message == TurnResult.STALEMATE;
    }
}