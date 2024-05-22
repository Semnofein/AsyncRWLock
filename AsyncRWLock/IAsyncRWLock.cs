namespace AsyncRWLock;

using System.Threading.Tasks;

using global::AsyncRWLock.Sections;

/// <summary>
/// Reader/Writer lock, that supports single writers and multiple readers with allowed await asynchronous operations inside.
/// </summary>
public interface IAsyncRWLock
{
    /// <summary>
    /// Creates a <see cref="Task"/>, that enters a read lock.
    /// </summary>
    /// <returns>
    /// Instance of a <see cref="Task{TResult}"/>, that enters a read lock and
    /// returns an interface that provides the ability to exit the read lock by calling Dispose().
    /// </returns>
    Task<IReaderSection> EnterReadAsync();

    /// <summary>
    /// Creates a <see cref="Task"/>, that enters a write lock.
    /// </summary>
    /// <returns>
    /// Instance of a <see cref="Task{TResult}"/>, that enters a write lock and
    /// returns an interface that provides the ability to exit the write lock by calling Dispose().
    /// </returns>
    Task<IWriterSection> EnterWriteAsync();
}