using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractuablePuerta : Interactuable
{
    private bool _puertaCerrada = true;
    [SerializeField] private int _precioDesbloqueoPuerta;
    [SerializeField] private GameObject[] spawns;
    [SerializeField] private GameObject UI;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Animator[] _animation;

    private void Awake()
    {
        _priceText.text = _precioDesbloqueoPuerta.ToString() + " $";
    }
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

            for (int i = 0; i < _animation.Length - 1; i++)
            {
                _animation[i].enabled = true;
            }
            this.gameObject.GetComponent<Animator>().enabled = true;
            _puertaCerrada = false;
        }
    }
    private void unlockSpawn()
    {
        for (int i = 0; i < spawns.Length; i++)
        {
            spawns[i].gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _puertaCerrada)
        {
            UI.gameObject.SetActive(true);
            UI.gameObject.GetComponent<Animator>().Play("Open");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && _puertaCerrada)
        {
            UI.gameObject.GetComponent<Animator>().Play("Close");
        }
    }
}