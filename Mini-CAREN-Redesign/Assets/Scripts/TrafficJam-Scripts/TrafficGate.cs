using UnityEngine;

public class TrafficGate : MonoBehaviour
{
    [SerializeField] private int maxAngle;
    [SerializeField] private int rotationSpeed;
    
    private bool inPosition;
    private bool open = false;

    // Update is called once per frame
    void Update()
    {
        if (inPosition)
            return;

        if  (open)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            if (transform.rotation.eulerAngles.z > maxAngle)
            {
				transform.rotation = Quaternion.Euler(
					transform.rotation.eulerAngles.x,
					transform.rotation.eulerAngles.y,
					maxAngle
					);

                inPosition = true;
			}
                
		}
        else
        {
			transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);

			if (transform.rotation.eulerAngles.z > 270)
            {
				transform.rotation = Quaternion.Euler(
					transform.rotation.eulerAngles.x,
					transform.rotation.eulerAngles.y,
					0
					);
                
                inPosition = true;
			}
		}
    }

    [ContextMenu("Open")]
	private void Open()
    {
        open = true;
        inPosition = false;
    }

	[ContextMenu("Close")]
	private void Close()
    {
        open = false;
        inPosition = false;
    }
}
