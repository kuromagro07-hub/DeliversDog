using TrueTrackSystem;
using UnityEngine;


public class GameCycle : MonoBehaviour
{
    
    [SerializeField] private TimerView timerView;
    [SerializeField] RailCreateManager railCreateManager;

    [SerializeField] float SetLimitTime = 120f;
    private Timer timer;
    private UIPresenterManager uiPresenterManager;

    void Start()
    {

        railCreateManager.Generate();
        // --- コアロジックのタイマー ---
        timer = new Timer(SetLimitTime);
        timer.Start();

        // --- MVPの構築 ---
        var timerModel = new TimerModel();
        var timerPresenter = new TimerPresenter(timerModel, timerView);

        // --- UIManager経由で操作する ---
        uiPresenterManager = new UIPresenterManager(timerPresenter);
    }

    void Update()
    {
        // コアのタイマーを進める
        timer.Update(Time.deltaTime);

        // UIに反映（Presenter → Viewへ）
        uiPresenterManager.UpdateTimer(timer.CurrentTime);
    }


}
