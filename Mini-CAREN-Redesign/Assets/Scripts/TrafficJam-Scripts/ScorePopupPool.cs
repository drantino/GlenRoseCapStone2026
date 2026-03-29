using System.Collections.Generic;
using UnityEngine;

public class ScorePopupPool : MonoBehaviour
{
	[SerializeField] private int numberOfObjects;
	[SerializeField] private GameObject scorePopupPrefab;

	public List<ScorePopup> inactiveScorePopups = new();
	public List<ScorePopup> activeScorePopups = new();

	private void Start()
	{
		if (numberOfObjects <= 0)
			throw new System.Exception("pool must contain at least one object");

		// instantiate objects
		for (int i = 0; i < numberOfObjects; i++)
		{
			ScorePopup popup = Instantiate(scorePopupPrefab).GetComponent<ScorePopup>();
			//popup.gameObject.SetActive(false);
			popup.pool = this;
			popup.transform.position = transform.position;

			inactiveScorePopups.Add(popup);
		}
	}

	public ScorePopup Get()
	{
		ScorePopup popup = null;

		if (inactiveScorePopups.Count > 0)
		{
			popup = inactiveScorePopups[0];
			inactiveScorePopups.RemoveAt(0);
			activeScorePopups.Add(popup);
		}
		else
		{
			popup = activeScorePopups[0];
			activeScorePopups.RemoveAt(0);
			activeScorePopups.Add(popup);
		}

		return popup;
	}

	public void Return(ScorePopup popup)
	{
		//popup.gameObject.SetActive(false);
		activeScorePopups.Remove(popup);
		inactiveScorePopups.Add(popup);
	}
}