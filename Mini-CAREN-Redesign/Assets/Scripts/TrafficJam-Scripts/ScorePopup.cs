using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScorePopup : MonoBehaviour
{
    public float despawnTime;
    [SerializeField] private float moveUpSpeed;

    public enum FeedbackType
    {
        Positive,
        Negative
    }
    
	public Image positiveImage;
    public Image negativeImage;

	private float timeUntilDespawn;
    private bool active;
    [HideInInspector] public ScorePopupPool pool;

	private void Start()
	{
		positiveImage.gameObject.SetActive(false);
        negativeImage.gameObject.SetActive(false);
	}

	public void Enable()
	{
		timeUntilDespawn = despawnTime;
        active = true;
	}

	void Update()
    {
        if (!active) return;

        transform.position += Vector3.up * moveUpSpeed * Time.deltaTime;

		positiveImage.color = new Color(
			1,
            1,
            1,
			despawnTime != 0 ? timeUntilDespawn / despawnTime : 0
			);

		negativeImage.color = new Color(
			1,
			1,
			1,
			despawnTime != 0 ? timeUntilDespawn / despawnTime : 0
			);

		timeUntilDespawn -= Time.deltaTime;
        if (timeUntilDespawn < 0 )
        {
            active = false;
            positiveImage.gameObject.SetActive(false);
            negativeImage.gameObject.SetActive(false);
            pool.Return(this);
        }
	}

    public void SetType(FeedbackType type)
    {
        positiveImage.gameObject.SetActive(type == FeedbackType.Positive);
        negativeImage.gameObject.SetActive(type == FeedbackType.Negative);
    }
}
