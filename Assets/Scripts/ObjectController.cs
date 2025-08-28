using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [Header("Object to Reveal")]
    [SerializeField] private GameObject objectToReveal;

    [Header("Trigger Settings")]
    [SerializeField] private string targetTag = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag) && objectToReveal != null)
        {
            objectToReveal.SetActive(true);
            Destroy(gameObject);

            Extinguisher player = other.GetComponent<Extinguisher>();
            {
                player.HoldItem(objectToReveal);
            }
        }
    }
}
