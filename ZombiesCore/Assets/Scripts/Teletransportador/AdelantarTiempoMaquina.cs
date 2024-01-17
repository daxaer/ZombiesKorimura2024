//using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdelantarTiempoMaquina : MonoBehaviour
{
    //public TransitionSettings Transicion;
    public float TiempoDemora;

    private void OnEnable()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //var TManager = TransitionManager.Instance();

            //TManager.onTransitionCutPointReached += DiaNocheManager.Instance.AvanzarDia;
            //TransitionManager.Instance().Transition(Transicion, TiempoDemora);

        }
    }
    
    
}
