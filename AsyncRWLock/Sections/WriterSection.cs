namespace AsyncRWLock.Sections;

using System;

using global::AsyncRWLock.Completions;

/// <inheritdoc cref="IWriterSection" />
internal class WriterSection : SectionBase, IWriterSection
{
    /// <inheritdoc />
    public WriterSection(Completion completion, Action<Completion> release) : base(completion, release)
    {
    }
}