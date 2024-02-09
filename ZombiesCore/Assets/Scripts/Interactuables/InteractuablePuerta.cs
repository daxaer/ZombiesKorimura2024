using UnityEngine;
using UnityEngine.UI;

public class InteractuablePuerta : Interactuable
{
    
    private bool _puertaCerrada = true;
    [SerializeField] private int _precioDesbloqueoPuerta;
    [SerializeField] private GameObject[] spawns;
    [SerializeField] private GameObject Puerta;

    public override void Interaccion()
    {
        Interactuando();
    }

    private void Interactuando()
    {
        var dineroJugador = _personaje._statsPersonaje._dineroActual;
        if (dineroJugador >= _precioDesbloqueoPuerta && _puertaCerrada)
        {
            unlockSpawn();
            _personaje._statsPersonaje._dineroActual -= _precioDesbloqueoPuerta;
            Debug.Log("Abierto");
            _personaje.GetComponent<InputManagerControls>()._interactuando = false;
            gameObject.tag = "Untagged";
            _puertaCerrada = false;
            if (_admiracionInButton != null)
                _admiracionInButton.gameObject.SetActive(false);
            _admiracionInUI?.gameObject.SetActive(false);
            if(Puerta != null)
            {
                Puerta.SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }
    private void unlockSpawn()
    {
        for (int i = 0; i < spawns.Length; i++)
        {
            spawns[i].gameObject.SetActive(true);
        }
    }
    
}