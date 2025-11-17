using UnityEngine;

public enum BallType
{
    Cue,
    Stripes,
    Solids
}

public interface IBall
{
    public BallType Type { get; }
    public void OnCollide();
}
