namespace AsyncRWLock.Completions;

using System.Threading.Tasks;

/// <summary>
/// Represents producer side of <see cref="Task{TResult}"/> that waits for exit of write lock.
/// </summary>
internal sealed class WriteCompletion : Completion
{
    /// <inheritdoc />
    public override bool IsWrite => true;
}