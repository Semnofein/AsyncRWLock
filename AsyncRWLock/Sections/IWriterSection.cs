namespace AsyncRWLock.Sections;

using System;

/// <summary>
/// Provides the ability to exit the write lock by calling <see cref="IDisposable.Dispose()"/>;
/// </summary>
public interface IWriterSection : IDisposable;