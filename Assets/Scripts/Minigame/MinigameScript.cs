using UnityEngine;

public class MinigameScript : MonoBehaviour
{
    public GameObject minigameUI; 
    private bool isInMinigame = false;
    private playerController playerMovement;

    void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<playerController>();
        minigameUI.SetActive(false);
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
        minigameUI.SetActive(true);

        playerMovement.FreezeMovement();


    }

    void ExitMinigame()
    {
        isInMinigame = false;
        minigameUI.SetActive(false);

        playerMovement.EnableMovement();


    }
}
