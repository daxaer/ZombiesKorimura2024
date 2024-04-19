using System;
using System.Collections;
using UnityEngine;

public class Humano : Personaje
{
    private void OnEnable()
    {
        InputManagerControls.PersonajeAction += HandlePersonajeAction;
        Enemy.EnemyDeadEvent += HandleZombieDeadEvent;
    }

    private void OnDisable()
    {
        InputManagerControls.PersonajeAction -= HandlePersonajeAction;
        Enemy.EnemyDeadEvent -= HandleZombieDeadEvent;
    }

    public override void ConfigurarFactoriaGameplay(FactoriaMainGameplay factoriaActualGameplay)
    {
        _factoriaActualGameplay = factoriaActualGameplay;
    }

    public override void SetArma(InterfaceArma arma)
    {
        _arma = arma ?? throw new Exception($"el arma no se encontro{arma}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _statsPersonaje.VidaActual -= 100;
        }
    }

    public override void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;
        encontrandoEnemigo = true;
        _target = EncontrarEnemigoMasCercano.Instance.GetEnemigoMasCercano();
    }


    private void HandleZombieDeadEvent()
    {
        _target = null;
        encontrandoEnemigo = false;
        _statsPersonaje.AddExperiencia(_statsPersonaje.ExperienciaPorZombieGanada);
        
    }

    public override void HandlePersonajeAction()
    {
        //if (_target == null)
        //{
        //    return;
        //}
        _arma.Atacar(/*_target*/);
    }
}