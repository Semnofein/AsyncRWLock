namespace AsyncRWLock.Tests.Wrappers;

using System;

public interface ISectionDates
{
    DateTime InitDate { get; }

    DateTime EnterDate { get; }

    DateTime ExitDate { get; }
}