using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuableAvanzarTiempo : Interactuable
{
    public TransitionSettings Transicion;
    public float TiempoDemora;

    public override void Interaccion()
    {
        AvanzarTiempo();
    }

    

    private void AvanzarTiempo()
    {
        var TManager = TransitionManager.Instance();

        TManager.onTransitionCutPointReached += DiaNocheManager.Instance.AvanzarDia;
        TransitionManager.Instance().Transition(Transicion, TiempoDemora);
    }
   
}
