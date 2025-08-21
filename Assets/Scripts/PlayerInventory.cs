using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] public TextMeshProUGUI materialCounterText;

    [Header("State (read-only)")]
    [SerializeField] public int materialCount;

    public UnityEvent<int> OnCountChanged;

    public int Count => materialCount;

    public void Add(int amount)
    {
        if (amount <= 0) return;
        materialCount += amount;
        UpdateUI();
        OnCountChanged?.Invoke(materialCount);
    }

    public bool TrySpend(int amount)
    {
        if (amount <= 0) return true;
        if (materialCount >= amount)
        {
            materialCount -= amount;
            UpdateUI();
            OnCountChanged?.Invoke(materialCount);
            return true;
        }
        return false;
    }

    public void ResetCount()
    {
        materialCount = 0;
        UpdateUI();
        OnCountChanged?.Invoke(materialCount);
    }

    private void UpdateUI()
    {
        if (materialCounterText != null)
            materialCounterText.text = materialCount.ToString();
    }
}