using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Minigame : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private string playerTag = "Player";
	[SerializeField] public float collectInterval = 1f;
	[SerializeField] private PlayerInventory inventory;

	private bool playerInside;
	private Coroutine collectRoutine;

	private void Reset()
	{
		var col = GetComponent<Collider2D>();
		col.isTrigger = true;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag(playerTag))
		{
			playerInside = true;
			collectRoutine = StartCoroutine(CollectLoop());
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag(playerTag))
		{
			playerInside = false;
			if (collectRoutine != null) StopCoroutine(collectRoutine);
		}
	}

	private IEnumerator CollectLoop()
	{
		WaitForSeconds wait = new WaitForSeconds(collectInterval);
		while (playerInside)
		{
			yield return wait;
			inventory?.Add(1);
		}
	}
}