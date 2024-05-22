namespace AsyncRWLock.Sections;

using System;

using global::AsyncRWLock.Completions;

/// <inheritdoc cref="IReaderSection" />
internal class ReaderSection : SectionBase, IReaderSection
{
    /// <inheritdoc />
    public ReaderSection(Completion completion, Action<Completion> release) : base(completion, release)
    {
    }
}