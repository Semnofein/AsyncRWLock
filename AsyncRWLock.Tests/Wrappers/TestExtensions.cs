namespace AsyncRWLock.Tests.Wrappers;

using System;
using System.Collections.Generic;

using global::AsyncRWLock.Sections;

public static class TestExtensions
{
    public static void EmulateRead(this IReaderSection rs) => (rs as IEmulator).LogRecord();

    public static void EmulateWrite(this IWriterSection ws) => (ws as IEmulator).LogRecord();

    public static IEnumerable<(DateTime, RecordType, ISectionDates)> GetAll(this ISectionDates sd) => (sd as IEmulator).GetAll();
}