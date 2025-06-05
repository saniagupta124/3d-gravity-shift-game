using UnityEngine;

public class UprightRotation : MonoBehaviour
{

    public float uprightX;
    public float uprightZ;

    private void Start()
    {
        uprightX = 0f;
        uprightZ = 0f;
    }

    private void Update()
    {
        // Get the current rotation of the child object (the collider)
        Vector3 currentRotation = transform.eulerAngles;

        // Lock the X and Z rotation, while keeping the Y rotation (rotation on Y-axis allowed)
        transform.rotation = Quaternion.Euler(uprightX, currentRotation.y, uprightZ);
    }
}
