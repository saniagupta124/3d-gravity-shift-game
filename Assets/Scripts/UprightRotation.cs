using UnityEngine;

/*
 * Locks object rotation on X and Z axes while allowing free Y-axis rotation. 
 * Keeps objects upright while allowing them to turn horizontally.
 */
public class UprightRotation : MonoBehaviour
{
    // Target X rotation in degrees (typically 0 for upright)
    public float uprightX;
    // Target Z rotation in degrees(typically 0 for upright)
    public float uprightZ;

    private void Start()
    {
        uprightX = 0f;
        uprightZ = 0f;
    }

    private void Update()
    {
        Vector3 currentRotation = transform.eulerAngles;

        // Lock the X and Z rotation, keep Y value for horizontal rotation
        transform.rotation = Quaternion.Euler(uprightX, currentRotation.y, uprightZ);
    }
}
