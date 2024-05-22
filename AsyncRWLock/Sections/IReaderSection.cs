namespace AsyncRWLock.Sections;

using System;

/// <summary>
/// Provides the ability to exit the read lock by calling <see cref="IDisposable.Dispose()"/>;
/// </summary>
public interface IReaderSection : IDisposable;