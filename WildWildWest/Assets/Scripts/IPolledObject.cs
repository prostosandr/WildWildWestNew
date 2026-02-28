using System;
using UnityEngine;
public interface IPolledObject<TItem>
{
    public event Action<TItem> Deactivated;
}
