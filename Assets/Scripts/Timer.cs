using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] public TextMeshProUGUI timerText;
    [SerializeField] private GameObject tutorialText;
    private float tutorialTimer = 0f;
    private bool tutorialHidden = false;

    [Header("Timer Settings")]
    [SerializeField] private bool countDown = false;
    [SerializeField] private float startTimeSeconds = 60f;

    [Header("Ending Screens")]
    [SerializeField] private GameObject endingPanel;
    [SerializeField] private GameObject badEndImage;
    [SerializeField] private GameObject goodEndImage;

    [Header("Moon")]
    [SerializeField] private Transform moonTransform;
    [SerializeField] private Vector3 moonStartScale = Vector3.one;
    [SerializeField] private Vector3 moonEndScale = new Vector3(3f, 3f, 3f);
    [SerializeField] private float moonEffectThreshold = 30f;

    [Header("Camera Shake")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float shakeThreshold = 10f;
    [SerializeField] private float shakeIntensity = 0.1f;
    [SerializeField] private float shakeFrequency = 25f;

	private float timeRemaining;
    private float elapsedTime;
    private bool isRunning;
    private Vector3 cameraOriginalPos;

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

        if (endingPanel != null) endingPanel.SetActive(false);
        if (mainCamera != null) cameraOriginalPos = mainCamera.transform.localPosition;
        if (moonTransform != null) moonTransform.localScale = moonStartScale;
    }

    void Update()
    {
        if (!isRunning) return;

        tutorialTimer += Time.deltaTime;
        if (!tutorialHidden && tutorialTimer >= 15f)
        {
            tutorialHidden = true;
            if (tutorialText != null) tutorialText.SetActive(false);
        }

        if (countDown)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0f)
            {
                timeRemaining = 0f;
                StopTimer();
                ShowBadEnding();
            }
            UpdateDisplay(timeRemaining);
            HandleMoonEffect();
            HandleCameraShake();
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
    
    public void StartTimer()
    {
        isRunning = true;
        tutorialText.SetActive(true);
    }
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


    // === Endings ===
    private void ShowBadEnding()
    {
        if (endingPanel != null)
        {
            endingPanel.SetActive(true);
            if (badEndImage != null) badEndImage.SetActive(true);
            if (goodEndImage != null) goodEndImage.SetActive(false);
        }
    }

    public void ShowGoodEnding()
    {
        StopTimer();
        if (endingPanel != null)
        {
            endingPanel.SetActive(true);
			if (badEndImage != null) badEndImage.SetActive(false);
			if (goodEndImage != null) goodEndImage.SetActive(true);
		}
    }
    /* To trigger ShowGoodEnding(), call the method on Timer.cs by adding this to the script with the win condition:
        
        [SerializeField] private string playerTag = "Player";
        [SerialifeField] private GameTimer timer; // assign in Inspector

        // Add this to the win condition:
            timer.ShowGoodEnding();
    */


    // === Moon enlargement and camera shake ===
    private void HandleMoonEffect()
    {
        if (moonTransform == null) return;
        if (timeRemaining > moonEffectThreshold) return;

        float t = Mathf.InverseLerp(moonEffectThreshold, 0f, timeRemaining);
        moonTransform.localScale = Vector3.Lerp(moonStartScale, moonEndScale, t);
    }

    private void HandleCameraShake()
    {
        if (mainCamera == null) return;

        if (timeRemaining > shakeThreshold)
        {
            return;
        }

        Vector3 basePos = mainCamera.transform.position;

        float intensity = shakeIntensity;
        if (timeRemaining <= 10f) intensity *= 10f;

        float offsetX = (Mathf.PerlinNoise(Time.time * shakeFrequency, 0f) - 0.5f) * 2f * intensity;
        float offsetY = (Mathf.PerlinNoise(0f, Time.time * shakeFrequency) - 0.5f) * 2f * intensity;

        mainCamera.transform.position = basePos + new Vector3(offsetX, offsetY, 0f);
    }
}