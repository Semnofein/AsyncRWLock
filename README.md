A reader/writer lock, that supports single writers and multiple readers with allowed await asynchronous operations inside.<br/>
**Attention 1!** Upgradable read lock is elemental evil imho, thats why i do not support it.<br/>
**Attention 2!** If you need async operations inside rwlock, seems that u do something wrong. But sometimes it is really necessary for reasons beyond your control.<br/>
**Attention 3!** It is better not to use in places with high performance.<br/><br/>

And forgive me in advance for my English, it’s not my native.

**howto**:
```csharp
private IAsyncRWLock _rwLock = new AsyncRWLock();

public async Task SomeReadMethodAsync()
{
  using var readLock = await _rwLock.EnterReadAsync();
  await DoReadWorkAsync();
}

public async Task SomeWriteMethodAsync()
{
  using var writeLock = await _rwLock.EnterWriteAsync();
  await DoWriteWorkAsync();
}

```
