using UnityEngine;

public interface IBeybladeConfiguration<T>
{
    public T MaxHealth { get; set; }
    public T Attack { get; set; }
    public T Defense { get; set; }
    public T CurrentHealth { get; set; }
    public T RoundsPerMinute { get; set; }
}