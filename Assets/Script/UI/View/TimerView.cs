using TMPro;
using UnityEngine;

public interface ITimerView
{
    void UpdateTimeDisplay(float _time);
}

public class TimerView : MonoBehaviour, ITimerView
{
    [SerializeField] private TextMeshProUGUI timeText;

    public void UpdateTimeDisplay(float _time)
    {
        int minutes = Mathf.FloorToInt(_time / 60f);
        int seconds = Mathf.FloorToInt(_time % 60f);

        timeText.text = $"{minutes:00}:{seconds:00}";
    }
}
