namespace Chess.Game;

public abstract class ChessTransaction : IDisposable
{
    private bool completed = false;
    public void Commit()
    {
        completed = true;
    }

    protected abstract void Rollback();

    public void Dispose()
    {
        if (completed) return;
        completed = true;
        Rollback();
        GC.SuppressFinalize(this);
    }

}