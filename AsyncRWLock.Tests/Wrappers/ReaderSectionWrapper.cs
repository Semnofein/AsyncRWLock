namespace AsyncRWLock.Tests.Wrappers;

using System;

using global::AsyncRWLock.Sections;

public class ReaderSectionWrapper : EmulatorBase, IReaderSection, ISectionDates
{
    private readonly IReaderSection _readerSection;

    public ReaderSectionWrapper(IReaderSection readerSection)
    {
        _readerSection = readerSection;
    }

    public DateTime InitDate { get; set; }

    public DateTime EnterDate { get; set; }

    public DateTime ExitDate { get; set; }

    protected override RecordType RecordType => RecordType.Read;

    public void Dispose()
    {
        ExitDate = DateTime.Now;
        _readerSection.Dispose();
    }
}