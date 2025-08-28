using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public string correctItemID; // Match with DraggableItem.itemID
    public MinigameManager minigameManager;

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem draggedItem = eventData.pointerDrag.GetComponent<DraggableItem>();

        if (draggedItem != null)
        {
            if (draggedItem.itemID == correctItemID)
            {
                Debug.Log("Correct item placed!");
                draggedItem.MarkAsPlaced();

                Destroy(draggedItem.gameObject);

                minigameManager.CheckWinCondition();
            }
            else
            {
                Debug.Log("Wrong item!");
            }
        }
    }
}
