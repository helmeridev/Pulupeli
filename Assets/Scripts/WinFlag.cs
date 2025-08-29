using UnityEngine;

public class WinFlag : MonoBehaviour
{
	[SerializeField] private Timer timer;

	private bool goodEndTriggered = false;

	void Update()
	{
		if (!goodEndTriggered && transform.childCount == 0)
		{
			goodEndTriggered = true;
			if (timer != null) timer.ShowGoodEnding();
			else Debug.LogWarning("WinFlag: Timer reference not set");
		}
	}
}