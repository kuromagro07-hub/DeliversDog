using UnityEngine;

public class TimerModel
{
    public float CurrentTime { get; private set; }
    public void SetTime(float _time) => CurrentTime = _time;
}
