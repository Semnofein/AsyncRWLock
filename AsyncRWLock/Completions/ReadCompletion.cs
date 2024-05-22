namespace AsyncRWLock.Completions;

using System.Threading.Tasks;

/// <summary>
/// Represents producer side of <see cref="Task{TResult}"/> that waits for exit of read lock.
/// </summary>
internal sealed class ReadCompletion : Completion
{
    /// <inheritdoc />
    public override bool IsWrite => false;
}