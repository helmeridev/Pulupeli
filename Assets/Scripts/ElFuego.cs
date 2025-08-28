using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class RequiresHeldItem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private TextMeshProUGUI promptText;


    [Header("Events")]
    public UnityEvent onItemUsed;
    public UnityEvent onNoItemHeld;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            UpdatePrompt(true);
        }
        if (!other.CompareTag(playerTag)) return;

        Extinguisher player = other.GetComponent<Extinguisher>();
        if (player == null) return;

        // Check if player is holding an item
        if (player.heldItem != null)
        {
            // Use item if player presses E
            if (Input.GetKeyDown(KeyCode.E))
            {
                // "Consume" the held item by disabling/destroying it
                player.heldItem.SetActive(false);
                player.heldItem = null;

                onItemUsed?.Invoke();
                Destroy(gameObject);
            }
        }
        else
        {
            onNoItemHeld?.Invoke();
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            UpdatePrompt(false);
        }
    }

    private void UpdatePrompt(bool show)
        {
            if (promptText == null) return;

            if (show)
            {
                promptText.gameObject.SetActive(true);
                promptText.text = $"Press E to Extinguish fire";
            }
            else promptText.gameObject.SetActive(false);
        }
}
