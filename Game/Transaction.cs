namespace Chess.Game;

public abstract class ChessTransaction : IDisposable
{
    private bool completed = false;
    private bool executed = false;

    public void Commit()
    {
        completed = true;
    }
    public void Execute()
    {
        ExecuteTransaction();
        executed = true;
    }
    protected abstract void ExecuteTransaction();
    protected abstract void Rollback();

    public void Dispose()
    {
        if (!executed) return;
        if (completed) return;
        completed = true;
        Rollback();
        GC.SuppressFinalize(this);
    }

}