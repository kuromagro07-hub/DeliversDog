using UnityEngine;

public class TimerPresenter
{
    private readonly TimerModel model;
    private readonly ITimerView view;
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="_core"></param>
    /// <param name="_model"></param>
    /// <param name="_view"></param>
    public TimerPresenter(TimerModel _model, ITimerView _view)
    {
        model = _model;
        view  = _view;
    }

    // GameCycle からは Update を呼ぶ（または UIManager 経由）
    public void Tick(float _deltaTime)
    {
        model.SetTime(_deltaTime);
        view.UpdateTimeDisplay(model.CurrentTime);
    }

}
