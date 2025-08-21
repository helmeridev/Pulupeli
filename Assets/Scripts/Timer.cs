using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    [SerializeField] public bool startOnAwake = true;

    private float elapsedTime;
    private bool isRunning;

    void Awake()
    {
        if (timerText == null)
        {
            Debug.LogError("Timer: No TextMeshProUGUI assigned!");
        }

        if (startOnAwake)
        {
            StartTimer();
        }
    }

    void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);

        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
    
    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateDisplay(0f);
    }

    private void UpdateDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);
        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}