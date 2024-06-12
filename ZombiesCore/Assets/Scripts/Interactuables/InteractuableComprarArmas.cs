using Audio;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractuableComprarArmas : Interactuable
{
    [SerializeField] private GameObject UI;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private int _precioArma;
    [SerializeField] private DetallesArma _arma;
    [SerializeField] private int _idArma = 0;
    private FactoriaArmas _factoriaArmas;
    [SerializeField] private AudioConfig audioSucces;
    [SerializeField] private AudioConfig audioFailed;

    public FactoriaArmas FactoriaArmas => _factoriaArmas;
    public void ConfigurarFactoriaArma(FactoriaArmas factoriaArmas)
    {
        _factoriaArmas = factoriaArmas;
    }
    public void Awake()
    {
        _priceText.text = _precioArma.ToString();
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
            AudioManager.Instance.PlayAudio2D(audioSucces);
            _personaje._statsPersonaje._dineroActual -= _precioArma;
            UIManager.Instance.UpdateMoney(_personaje._statsPersonaje._dineroActual);
            AsignarArma();
        }
        else
        {
            AudioManager.Instance.PlayAudio2D(audioFailed);
        }
    }
    private void AsignarArma()
    {
        _personaje.ArmaSystem.AsignarArma(_idArma, _factoriaArmas);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            UI.gameObject.SetActive(true);
            UI.gameObject.GetComponent<Animator>().Play("Open");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            UI.gameObject.GetComponent<Animator>().Play("Close");
        }
    }
}
