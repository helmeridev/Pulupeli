using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Shop : MonoBehaviour
{
    [Header("Cost")]
    [SerializeField] public int materialsNeeded = 10;

    [Header("References")]
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private TextMeshProUGUI promptText;

    [Header("Behavior")]
    [SerializeField] public bool singleUse = false;

    public UnityEvent onSpendSuccess;
    public UnityEvent onInsufficient;

    private bool inRange;
    private bool used;

    private void Reset()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void OnEnable()
    {
        UpdatePrompt(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            inRange = true;
            UpdatePrompt(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            inRange = false;
            UpdatePrompt(false);
        }
    }

    private void Update()
    {
        if (!inRange || used || inventory == null) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventory.TrySpend(materialsNeeded))
            {
                onSpendSuccess?.Invoke();
                if (singleUse)
                {
                    used = true;
                    UpdatePrompt(false);
                }
            }
            else onInsufficient?.Invoke();
        }
    }

    private void UpdatePrompt(bool show)
    {
        if (promptText == null) return;

        if (show && !used)
        {
            promptText.gameObject.SetActive(true);
            promptText.text = $"Press E to spend {materialsNeeded}";
        }
        else promptText.gameObject.SetActive(false);
    }
}
