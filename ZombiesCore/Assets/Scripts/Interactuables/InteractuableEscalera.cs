using EasyTransition;
using UnityEngine;

public class InteractuableEscalera : Interactuable
{
    [SerializeField] private Transform posicionArribaEstructura;
    [SerializeField] private Transform posicionAbajoEstructura;
    private bool _estaEnTorre;
    public TransitionSettings Transicion;
    public float TiempoDemora;

    public override void Interaccion()
    {
        if (!_estaEnTorre)
            SubirEscaleras();
        else
            BajarEscaleras();

    }

    private void MoverPersonajeArriba()
    {
        Debug.Log("Moviendo Personaje arriba");
        _personaje.transform.position = posicionArribaEstructura.position;
        CambiadorCamaras.CambiarCamaraPrimeraPersona();
    }
    private void MoverPersonajeAbajo()
    {
        Debug.Log("Moviendo Personaje abajo");
        _personaje.transform.position = posicionAbajoEstructura.position;
        CambiadorCamaras.CambiarCamaraTerceraPersona();


    }

    private void ManejarTransicion(bool bajarEscaleras)
    {
        var TManager = TransitionManager.Instance();

        if (bajarEscaleras)
            TManager.onTransitionCutPointReached -= MoverPersonajeArriba;
        else
            TManager.onTransitionCutPointReached -= MoverPersonajeAbajo;

        if (bajarEscaleras)
            TManager.onTransitionCutPointReached += MoverPersonajeAbajo;
        else
            TManager.onTransitionCutPointReached += MoverPersonajeArriba;

        TManager.Transition(Transicion, TiempoDemora);

        Debug.Log(bajarEscaleras ? "Bajando Escaleras" : "Subiendo Escaleras");

        _estaEnTorre = !bajarEscaleras;
    }

    private void BajarEscaleras()
    {
        ManejarTransicion(true);
    }

    private void SubirEscaleras()
    {
        ManejarTransicion(false);
    }
}
