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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeUntilDespawn = despawnTime;
        textMesh.text = text;
        textMesh.color = color;
    }

    // Update is called once per frame
    void Update()
    {
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
            Destroy(gameObject);
        }
    }
}
