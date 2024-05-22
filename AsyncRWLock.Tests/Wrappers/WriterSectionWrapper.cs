namespace AsyncRWLock.Tests.Wrappers;

using System;

using global::AsyncRWLock.Sections;

public class WriterSectionWrapper : EmulatorBase, IWriterSection, ISectionDates
{
    private readonly IWriterSection _writerSection;

    public WriterSectionWrapper(IWriterSection writerSection)
    {
        _writerSection = writerSection;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }

    public DateTime InitDate { get; set; }

    public DateTime EnterDate { get; set; }

    public DateTime ExitDate { get; set; }

    protected override RecordType RecordType => RecordType.Write;

    public void Dispose()
    {
        ExitDate = DateTime.Now;
        _writerSection.Dispose();
    }
}