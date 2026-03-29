using TMPro;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    public string text;
    public Color color;
    public float despawnTime;
    [SerializeField] private float moveUpSpeed;

    private float timeUntilDespawn;
    private bool active;
    [HideInInspector] public ScorePopupPool pool;
    
    public void Enable()
	{
		//gameObject.SetActive(true);
		timeUntilDespawn = despawnTime;
		textMesh.text = text;
		textMesh.color = color;
        active = true;
	}

	void Update()
    {
        if (!active) return;

        transform.position += Vector3.up * moveUpSpeed * Time.deltaTime;
        textMesh.color = new Color(
            textMesh.color.r,
            textMesh.color.g,
            textMesh.color.b,
            timeUntilDespawn / despawnTime
            );

        timeUntilDespawn -= Time.deltaTime;
        if (timeUntilDespawn < 0 )
        {
            active = false;
            pool.Return(this);
        }
    }
}
