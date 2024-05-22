namespace AsyncRWLock.Tests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using global::AsyncRWLock.Tests.Wrappers;

using NUnit.Framework;

[TestFixture]
public class AsyncRWLockTests
{
    [TestCase(9, 9, 15, 30_000)]
    [Timeout(35_000)] // +5s permissible error for overhead costs
    public async Task AsyncRWLock_WithLoggingWrapper_ShouldBeCorrectIntervals(
        int writeTasks,
        int slowReadTasks,
        int fastReadTasks,
        int minWorkTimeMs)
    {
        var writerTaskCount = writeTasks;
        var slowReadTaskCount = slowReadTasks;
        var fastReadTaskCount = fastReadTasks;
        var minWorkTime = TimeSpan.FromMilliseconds(minWorkTimeMs);

        var tasks = new List<Task>();
        using var cts = new CancellationTokenSource();
        var token = cts.Token;
        var rwl = new LockWrapper();

        tasks.AddRange(Enumerable.Range(0, writerTaskCount).Select(_ => EmulateWritInCycleAsync(rwl, token)));
        tasks.AddRange(Enumerable.Range(0, slowReadTaskCount).Select(_ => EmulateSlowReadInCycleAsync(rwl, token)));
        tasks.AddRange(Enumerable.Range(0, fastReadTaskCount).Select(_ => EmulateFastReadInCycleAsync(rwl, token)));

        cts.CancelAfter(minWorkTime);
        await Task.WhenAll(tasks).ConfigureAwait(false);
        var allDates = rwl.AllDates;
        List<(DateTime date, RecordType recordType, ISectionDates section)> allRecords = allDates.SelectMany(x => x.GetAll()).ToList();
        foreach (var date in allDates)
        {
            var localRecords = allRecords.Where(x => x.date >= date.EnterDate && x.date < date.ExitDate).ToList();
            if (date is ReaderSectionWrapper)
            {
                var hasWriteIntoInterval = localRecords.Any(x => x.recordType == RecordType.Write);
                Assert.False(hasWriteIntoInterval);
            }
            else
            {
                var hasNotSelfIntoInterval = localRecords.Any(x => !ReferenceEquals(x.section, date));
                Assert.False(hasNotSelfIntoInterval);
            }
        }
    }

    private async Task EmulateWritInCycleAsync(IAsyncRWLock rwl, CancellationToken token)
    {
        var random = new Random();
        while (!token.IsCancellationRequested)
        {
            await Task.Delay(random.Next(50, 150), default).ConfigureAwait(false);
            using var wl = await rwl.EnterWriteAsync().ConfigureAwait(false);
            wl.EmulateWrite();
            await Task.Delay(50, default).ConfigureAwait(false);
            wl.EmulateWrite();
        }
    }

    private async Task EmulateFastReadInCycleAsync(IAsyncRWLock rwl, CancellationToken token)
    {
        var random = new Random();
        while (!token.IsCancellationRequested)
        {
            await Task.Delay(random.Next(50, 100), default).ConfigureAwait(false);
            using var rl = await rwl.EnterReadAsync().ConfigureAwait(false);
            rl.EmulateRead();
        }
    }

    private async Task EmulateSlowReadInCycleAsync(IAsyncRWLock rwl, CancellationToken token)
    {
        var random = new Random();
        while (!token.IsCancellationRequested)
        {
            await Task.Delay(random.Next(50, 100), default).ConfigureAwait(false);
            using var rl = await rwl.EnterReadAsync().ConfigureAwait(false);
            rl.EmulateRead();
            await Task.Delay(200, default).ConfigureAwait(false);
            rl.EmulateRead();
        }
    }
}