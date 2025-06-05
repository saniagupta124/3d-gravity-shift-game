using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1 : MonoBehaviour
{
    [SerializeField] GameObject modelShow;
    [SerializeField] GameObject modelHide1;
    [SerializeField] GameObject modelHide2;
    [SerializeField] GameObject modelHide3;


    private void OnTriggerExit(Collider other)
    {
        //NormGrav();
        modelShow.SetActive(true);
        modelHide1.SetActive(false);
        modelHide2.SetActive(false);
        modelHide3.SetActive(false);
    }
}
