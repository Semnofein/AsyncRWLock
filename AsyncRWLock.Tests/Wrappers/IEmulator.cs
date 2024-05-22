namespace AsyncRWLock.Tests.Wrappers;

using System;
using System.Collections.Generic;

public interface IEmulator
{
    void LogRecord();

    IEnumerable<(DateTime, RecordType, ISectionDates)> GetAll();
}