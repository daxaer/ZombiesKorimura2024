using UnityEngine;
using UnityEngine.UI;

public abstract class Interactuable : MonoBehaviour
{
    private protected Personaje _personaje;
    [SerializeField] protected Image _admiracionInButton;
    [SerializeField] protected Image _admiracionInUI;
    public Personaje Personaje
    {
        set => _personaje = value;
    }

    public abstract void Interaccion();

    public virtual void ActivarUI(bool interactuando)
    {
        if (interactuando)
        {
            _admiracionInUI?.gameObject.SetActive(true);
            if (_admiracionInButton != null) _admiracionInButton.gameObject.SetActive(true);
            interactuando = false;
        }
        else
        {
            if (_admiracionInButton != null)
            {
                _admiracionInButton.gameObject.SetActive(false);
            }

            _admiracionInUI?.gameObject.SetActive(false);
            interactuando = true;
        }
    }

    private static Interactuable _instance;
    public static Interactuable Instance => _instance;

    private void Awake()
    {
        if(_instance==null)
            _instance = this;
    }
}