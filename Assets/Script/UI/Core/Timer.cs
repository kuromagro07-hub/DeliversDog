using UnityEngine;


public interface ITimer
{
    float CurrentTime { get; }
    bool IsRunning { get; }
    void Start();
    void Stop();
    void Reset();
    void Update(float deltaTime);
}

public class Timer : ITimer
{
    
    private float currentTime;
    private bool isRunning;
    private float limitTime;

    public float CurrentTime => currentTime;
    public bool IsRunning => isRunning;
    public bool IsFinished => currentTime <= 0f;

    public Timer(float _limitSeconds)
    {
        limitTime = _limitSeconds;
        currentTime = limitTime;
    }

    public void Start()
    {
        isRunning = true;
    }

    public void Stop() => isRunning = false;
    public void Reset() { currentTime = limitTime; }

    public void Update(float _deltaTime)
    {
        if (!isRunning) return;

        currentTime -= _deltaTime;

        if (currentTime < 0f)
        {
            currentTime = 0f;
            isRunning = false;
        }
    }
}
