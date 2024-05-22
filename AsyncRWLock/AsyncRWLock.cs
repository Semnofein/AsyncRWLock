namespace AsyncRWLock;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using global::AsyncRWLock.Completions;
using global::AsyncRWLock.Sections;

/// <inheritdoc />
public class AsyncRWLock : IAsyncRWLock
{
    /// <summary>
    /// A list containing completions of an actual exits of read or write locks.
    /// </summary>
    private readonly List<Completion> _list;

    /// <summary>
    /// Initializes new instance of <see cref="AsyncRWLock" />.
    /// </summary>
    public AsyncRWLock() => _list = new ();

    /// <inheritdoc />
    public async Task<IReaderSection> EnterReadAsync()
    {
        var current = new ReadCompletion();
        await WaitForExitOtherLocks(current, onlyWrite: true).ConfigureAwait(false);
        return new ReaderSection(current, Exit);
    }

    /// <inheritdoc />
    public async Task<IWriterSection> EnterWriteAsync()
    {
        var current = new WriteCompletion();
        await WaitForExitOtherLocks(current, onlyWrite: false).ConfigureAwait(false);
        return new WriterSection(current, Exit);
    }

    /// <summary>
    /// Creates a <see cref="Task"/>, that awaits all tasks (from all actual completions), that must be completed before entering current lock.
    /// </summary>
    /// <param name="current">Completion that produses a task that waits for current lock exit.</param>
    /// <param name="onlyWrite">Value indicates that the current lock must await only other write locks.</param>
    /// <returns>
    /// Instance of a <see cref="Task{TResult}"/>, that awaits all tasks, that must be completed before entering current lock.
    /// </returns>
    private async Task WaitForExitOtherLocks(Completion current, bool onlyWrite)
    {
        List<Completion> waited;
        lock (_list)
        {
            _list.Add(current);
            waited = onlyWrite
                ? _list.Where(x => x.IsWrite).ToList()
                : _list.Where(x => x != current).ToList();
        }

        await Task.WhenAll(waited.Select(x => x.Task)).ConfigureAwait(false);
    }

    /// <summary>
    /// Sets the underlying <see cref="Task{TResult}"/> of a <paramref name="completion" />
    /// into <see cref="System.Threading.Tasks.TaskStatus.RanToCompletion" /> state.
    /// </summary>
    /// <param name="completion">
    /// Completion that sets the <see cref="System.Threading.Tasks.TaskStatus.RanToCompletion" /> state
    /// to all prodused tasks that waits for exit of a released lock.</param>
    private void Exit(Completion completion)
    {
        completion.Complete();
        lock (_list)
            _list.Remove(completion);
    }
}