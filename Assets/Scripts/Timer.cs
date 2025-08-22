using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    [SerializeField] private bool countDown = false;
    [SerializeField] private float startTimeSeconds = 60f;

    private float timeRemaining;
    private float elapsedTime;
    private bool isRunning;

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("Timer: No TextMeshProUGUI assigned!");
        }

        if (countDown)
        {
            timeRemaining = startTimeSeconds;
            UpdateDisplay(timeRemaining);
        }
        else
        {
            elapsedTime = 0f;
            UpdateDisplay(0f);
        }
    }

    void Update()
    {
        if (!isRunning) return;

        if (countDown)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0f)
            {
                timeRemaining = 0f;
                StopTimer();
                // TODO: Game over event
            }
            UpdateDisplay(timeRemaining);
        }
        else
        {
            elapsedTime += Time.deltaTime;
            UpdateDisplay(elapsedTime);
        }
    }

    private void UpdateDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);
        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
    
    public void StartTimer() => isRunning = true;
    public void StopTimer() => isRunning = false;
    public void ResetTimer()
    {
        if (countDown) timeRemaining = startTimeSeconds;
        else elapsedTime = 0f;
        UpdateDisplay(countDown ? timeRemaining : 0f);
    }

    public void ConfigureAsCountdown(float seconds)
    {
        countDown = true;
        startTimeSeconds = seconds;
        ResetTimer();
    }

    public void ConfigureAsStopwatch()
    {
        countDown = false;
        ResetTimer();
    }
}