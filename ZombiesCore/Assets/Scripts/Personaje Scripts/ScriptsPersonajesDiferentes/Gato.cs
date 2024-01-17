using System;
using UnityEngine;

public class Gato : Personaje
{
    private void OnEnable()
    {
        InputManagerControls.PersonajeAction += HandlePersonajeAction;
    }
    private void OnDisable()
    {

    }

    public override void ConfigurarFactoriaGameplay(FactoriaMainGameplay factoriaActualGameplay)
    {
        _factoriaActualGameplay = factoriaActualGameplay;

    }

    public override void SetArma(InterfaceArma arma)
    {
        if (arma == null)
        {
            throw new Exception($"el arma no se encontro{arma}");
        }
        _arma = arma;
    }

    //private void Update()
    //{

    //}

    public override void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _target = EncontrarEnemigoMasCercano.Instance.GetEnemigoMasCercano();
        }
    }

    public override void HandlePersonajeAction()
    {
        _arma.Atacar(/*_target*/);
    }

}
