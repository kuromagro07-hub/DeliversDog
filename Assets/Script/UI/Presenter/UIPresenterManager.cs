using UnityEngine;

public class UIPresenterManager
{
    // 後々、複数のPresenterを管理
    private TimerPresenter timerPresenter;
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="_timerPresenter"></param>
    public UIPresenterManager(TimerPresenter _timerPresenter)
    {
        timerPresenter = _timerPresenter;
    }
    
    public void UpdateTimer(float _time)
    {
        timerPresenter.Tick(_time);
    }
}
