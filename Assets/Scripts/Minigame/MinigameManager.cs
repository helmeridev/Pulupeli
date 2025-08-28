using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public GameObject minigameUI;
    public GameObject[] itemPrefabs; // Array of 2 prefabs (index 0 = ID 1, index 1 = ID 2)
    public Transform spawnParent; // UI container (Canvas or Panel)
    public Rect spawnArea = new Rect(-200, -150, 400, 300); // Defines random spawn bounds (x, y, width, height)

    public int minItems = 3;
    public int maxItems = 6;

    public DraggableItem[] items;
    public playerController playerController;

    public void StartMinigame()
    {
        ClearExistingItems();    // Remove old items
        SpawnRandomItems();      // Spawn new ones
        minigameUI.SetActive(true);
    }

    void SpawnRandomItems()
    {
        int itemCount = Random.Range(minItems, maxItems + 1);
        items = new DraggableItem[itemCount];

        for (int i = 0; i < itemCount; i++)
        {
            int prefabIndex = Random.Range(0, itemPrefabs.Length);
            GameObject itemObj = Instantiate(itemPrefabs[prefabIndex], spawnParent);

            // Random position within spawnArea
            RectTransform rect = itemObj.GetComponent<RectTransform>();
            float randomX = Random.Range(spawnArea.xMin, spawnArea.xMax);
            float randomY = Random.Range(spawnArea.yMin, spawnArea.yMax);
            rect.anchoredPosition = new Vector2(randomX, randomY);

            DraggableItem draggable = itemObj.GetComponent<DraggableItem>();
            draggable.itemID = (prefabIndex + 1).ToString(); // ID 1 or 2
            items[i] = draggable;
        }
    }

    void ClearExistingItems()
    {
        foreach (Transform child in spawnParent)
        {
            if (child.GetComponent<DraggableItem>() != null)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void CheckWinCondition()
    {
        foreach (DraggableItem item in items)
        {
            if (!item.isPlacedCorrectly)
                return;
        }

        Debug.Log("All items placed correctly! Minigame complete.");
        ExitMinigame();
    }

    public void ExitMinigame()
    {
        minigameUI.SetActive(false);
        playerController.EnableMovement();
    }
}
