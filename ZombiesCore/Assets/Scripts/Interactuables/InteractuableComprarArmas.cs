using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractuableComprarArmas : Interactuable
{
    [SerializeField] private int _precioArma;
    [SerializeField] private DetallesArma _arma;
    [SerializeField] private int _idArma = 0;
    private FactoriaArmas _factoriaArmas;

    public FactoriaArmas FactoriaArmas => _factoriaArmas;
    public void ConfigurarFactoriaArma(FactoriaArmas factoriaArmas)
    {
        _factoriaArmas = factoriaArmas;
    }

    public override void Interaccion()
    {
        Interactuando();
    }

    public void Interactuando()
    {
        Debug.Log("interactuando comprar armas");
        if(_personaje._statsPersonaje._dineroActual >= _precioArma)
        {
            Debug.Log("interactuando comprar armas");
            _personaje._statsPersonaje._dineroActual -= _precioArma;
            UIManager.Instance.UpdateMoney(_personaje._statsPersonaje._dineroActual);
            AsignarArma();
        }
    }
    private void AsignarArma()
    {
        _personaje.ArmaSystem.AsignarArma(_idArma, _factoriaArmas);
    }
}
