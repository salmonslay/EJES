using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image _bar; // a unity image that will be filled with the timer progress

    private float _startTime;
    private bool _isRunning;

    public bool IsRunning => _isRunning;

    public float TimeLeft { get; set; }

    public float StartTime => _startTime;

    public void StartTimer(float time)
    {
        _startTime = time;
        TimeLeft = time;
        _isRunning = true;
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    public void ContinueTimer()
    {
        _isRunning = true;
    }

    private void Update()
    {
        if (!_isRunning) return;

        TimeLeft -= Time.deltaTime;

        if (_bar)
            _bar.fillAmount = TimeLeft / _startTime;

        if (TimeLeft <= 0)
        {
            _isRunning = false;
            TimeLeft = 0;
            if (_bar)
                _bar.fillAmount = 0; // unsure if fillAmount clamps itself or not

            // if (GameManager.Instance)
            //     GameManager.Instance.CurrentMinigame?.EndMinigame(false);
            // TODO 07/10: END MINIGAME
        }
    }
}