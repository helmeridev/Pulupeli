using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private playerController playerMovement;
    [SerializeField] private Timer timer;

    [Header("Countdown Times (seconds)")]
    [SerializeField] private float easyTime = 600f;
    [SerializeField] private float mediumTime = 300f;
    [SerializeField] private float hardTime = 120f;

    private void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<playerController>();
        playerMovement.FreezeMovement();
    }
    public void StartEasy()
    {
        timer.ConfigureAsCountdown(easyTime);
        timer.StartTimer();
        playerMovement.EnableMovement();
        HideMenu();
    }

    public void StartMedium()
    {
        timer.ConfigureAsCountdown(mediumTime);
        timer.StartTimer();
        playerMovement.EnableMovement();
        HideMenu();
    }

    public void StartHard()
    {
        timer.ConfigureAsCountdown(hardTime);
        timer.StartTimer();
        playerMovement.EnableMovement();
        HideMenu();
    }

    public void StartSpeedrun()
    {
        timer.ConfigureAsStopwatch();
        timer.StartTimer();
        playerMovement.EnableMovement();
        HideMenu();
    }

    private void HideMenu() => gameObject.SetActive(false);
}