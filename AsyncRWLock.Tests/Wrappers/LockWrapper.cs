namespace AsyncRWLock.Tests.Wrappers;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using global::AsyncRWLock.Sections;

public class LockWrapper : IAsyncRWLock
{
    private readonly AsyncRWLock _rwl;

    private readonly List<ISectionDates> _allDates;

    public LockWrapper()
    {
        _rwl = new AsyncRWLock();
        _allDates = new ();
    }

    public IReadOnlyCollection<ISectionDates> AllDates => _allDates;

    public async Task<IReaderSection> EnterReadAsync()
    {
        var initDate = DateTime.Now;
        var readerSection = await _rwl.EnterReadAsync().ConfigureAwait(false);
        var enterDate = DateTime.Now;
        var readerWrapper = new ReaderSectionWrapper(readerSection);
        readerWrapper.InitDate = initDate;
        readerWrapper.EnterDate = enterDate;
        _allDates.Add(readerWrapper);
        return readerWrapper;
    }

    public async Task<IWriterSection> EnterWriteAsync()
    {
        var initDate = DateTime.Now;
        var writerSection = await _rwl.EnterWriteAsync().ConfigureAwait(false);
        var enterDate = DateTime.Now;
        var writerWrapper = new WriterSectionWrapper(writerSection);
        writerWrapper.InitDate = initDate;
        writerWrapper.EnterDate = enterDate;
        _allDates.Add(writerWrapper);
        return writerWrapper;
    }
}