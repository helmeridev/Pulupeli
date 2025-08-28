using UnityEngine;


public class Extinguisher : MonoBehaviour
{
    public GameObject heldItem;

    public void HoldItem(GameObject item)
    {
        heldItem = item;
        heldItem.SetActive(true);
    }
}
