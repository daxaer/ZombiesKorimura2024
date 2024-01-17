using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class BackButton : MonoBehaviour
{
    [SerializeField] private EventTrigger jugarEvent;
    [SerializeField] private EventTrigger ajustesEvent;
    [SerializeField] private EventTrigger informesEvent;
    [SerializeField] private EventTrigger salirEvent;
    [SerializeField] private GameObject layoutAjustes;
    //[SerializeField] private Volume volume;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && gameObject.activeInHierarchy)
        {
            CerrarPopUp();
        }
    }

    public void CerrarPopUp()
    {
       layoutAjustes.SetActive(false);
        //volume.enabled = false;
        jugarEvent.enabled = true;
        ajustesEvent.enabled = true;
        informesEvent.enabled = true;
        salirEvent.enabled = true;
    }
}
