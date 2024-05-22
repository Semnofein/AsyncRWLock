namespace AsyncRWLock.Tests.Wrappers;

using System;
using System.Collections.Generic;

public abstract class EmulatorBase : IEmulator
{
    private readonly List<DateTime> _dates;

    protected EmulatorBase() => _dates = new ();

    protected abstract RecordType RecordType { get; }

    public void LogRecord() => _dates.Add(DateTime.Now);

    public IEnumerable<(DateTime, RecordType, ISectionDates)> GetAll()
    {
        var sd = (ISectionDates)this;
        var recordType = RecordType;
        foreach (var date in _dates)
            yield return (date, recordType, sd);
    }
}