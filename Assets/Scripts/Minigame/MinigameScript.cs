using UnityEngine;

public class MinigameScript : MonoBehaviour
{
    public MinigameManager minigameManager;
    private bool isInMinigame = false;
    private playerController playerMovement;

    void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<playerController>();
        minigameManager.minigameUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EnterMinigame();
        }
    }

    void Update()
    {
        if (isInMinigame && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitMinigame();
        }
    }

    void EnterMinigame()
    {
        isInMinigame = true;
        playerMovement.FreezeMovement();

        minigameManager.StartMinigame(); // Randomize every time
    }

    void ExitMinigame()
    {
        isInMinigame = false;
        minigameManager.ExitMinigame();
        playerMovement.EnableMovement();
    }
}
