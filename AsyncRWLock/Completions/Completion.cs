namespace AsyncRWLock.Completions;

using System.Threading.Tasks;

/// <summary>
/// Provides a convenient abstraction for <see cref="TaskCompletionSource{T}"/> implementstions
/// that represents producer side of <see cref="Task{TResult}"/> that creates with
/// <see cref="TaskCreationOptions.RunContinuationsAsynchronously"/> by default and waits for exit of read or write lock.
/// </summary>
internal abstract class Completion : TaskCompletionSource<int>
{
    /// <summary>
    /// Initializes new instance of <see cref="Completion" />.
    /// </summary>
    protected Completion() : base(TaskCreationOptions.RunContinuationsAsynchronously)
    {
    }

    /// <summary>
    /// Gets a value indicating that the waited lock is write lock, otherwise - read lock.
    /// </summary>
    public abstract bool IsWrite { get; }

    /// <summary>
    /// Sets the underlying <see cref="Task{TResult}"/> into
    /// <see cref="System.Threading.Tasks.TaskStatus.RanToCompletion">RanToCompletion</see> state.
    /// </summary>
    public void Complete() => SetResult(1);
}