using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public enum MaterialType { Material1, Material2 }

    [Header("Cost")]
    [SerializeField] public int materialsNeeded = 10;
    [SerializeField] public MaterialType materialType = MaterialType.Material1;

    [Header("References")]
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Image shopImage; // UI Image instead of SpriteRenderer
    [SerializeField] private Sprite material1Sprite;
    [SerializeField] private Sprite material2Sprite;

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
        UpdateImage();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            inRange = true;
            UpdatePrompt(true);
            UpdateImage();
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
            bool success = false;

            // Deduct correct material
            if (materialType == MaterialType.Material1)
            {
                success = inventory.TrySpend(materialsNeeded);
            }
            else if (materialType == MaterialType.Material2)
            {
                if (inventory.material2Count >= materialsNeeded)
                {
                    inventory.material2Count -= materialsNeeded;

                    // Update UI manually
                    if (inventory.material2CounterText != null)
                        inventory.material2CounterText.text = inventory.material2Count.ToString();

                    success = true;
                }
            }

            if (success)
            {
                onSpendSuccess?.Invoke();
                if (singleUse)
                {
                    used = true;
                    UpdatePrompt(false);
                    Destroy(gameObject);
                }
            }
            else
            {
                onInsufficient?.Invoke();
            }
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

    private void UpdateImage()
    {
        if (shopImage == null) return;

        if (materialType == MaterialType.Material1 && material1Sprite != null)
            shopImage.sprite = material1Sprite;
        else if (materialType == MaterialType.Material2 && material2Sprite != null)
            shopImage.sprite = material2Sprite;
    }
}
