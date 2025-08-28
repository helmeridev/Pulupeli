using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Timer timer;

    [Header("Countdown Times (seconds)")]
    [SerializeField] private float easyTime = 600f;
    [SerializeField] private float mediumTime = 300f;
    [SerializeField] private float hardTime = 120f;

    public void StartEasy()
    {
        timer.ConfigureAsCountdown(easyTime);
        timer.StartTimer();
        HideMenu();
    }

    public void StartMedium()
    {
        timer.ConfigureAsCountdown(mediumTime);
        timer.StartTimer();
        HideMenu();
    }

    public void StartHard()
    {
        timer.ConfigureAsCountdown(hardTime);
        timer.StartTimer();
        HideMenu();
    }

    public void StartSpeedrun()
    {
        timer.ConfigureAsStopwatch();
        timer.StartTimer();
        HideMenu();
    }

    private void HideMenu() => gameObject.SetActive(false);
}