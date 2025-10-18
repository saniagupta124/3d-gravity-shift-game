using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Controls door visibility by toggling between different model states.
 * Shows one model while hiding others when player exits trigger zone.
 */
public class Door1 : MonoBehaviour
{
    [SerializeField] GameObject modelShow;
    [SerializeField] GameObject modelHide1;
    [SerializeField] GameObject modelHide2;
    [SerializeField] GameObject modelHide3;


    private void OnTriggerExit(Collider other)
    {
        modelShow.SetActive(true);
        modelHide1.SetActive(false);
        modelHide2.SetActive(false);
        modelHide3.SetActive(false);
    }
}
