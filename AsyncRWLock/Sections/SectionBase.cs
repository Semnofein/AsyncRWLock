namespace AsyncRWLock.Sections;

using System;
using System.Threading.Tasks;

using global::AsyncRWLock.Completions;

/// <summary>
/// Base type for implementations of interfaces that provide the abilities to exit a lock.
/// </summary>
internal abstract class SectionBase : IDisposable
{
    /// <summary>
    /// Delegate that invoked when the lock releases.
    /// </summary>
    private readonly Action<Completion> _release;

    /// <summary>
    /// Completion that produce a <see cref="Task"/> that waits for exit of a lock.
    /// </summary>
    private readonly Completion _completion;

    /// <summary>
    /// Initializes new instance of <see cref="SectionBase" />.
    /// </summary>
    /// <param name="completion">Completion that produce a <see cref="Task"/> that waits for exit of a lock.</param>
    /// <param name="release">Delegate that invoked when the lock releases.</param>
    protected SectionBase(Completion completion, Action<Completion> release)
    {
        _completion = completion;
        _release = release;
    }

    /// <summary>
    /// Releases a lock.
    /// </summary>
    public void Dispose() => _release(_completion);
}