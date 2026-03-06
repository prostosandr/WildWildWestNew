using System;

public interface IPolledObject<TItem>
{
    public event Action<TItem> Deactivated;
}