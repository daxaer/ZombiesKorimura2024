using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractuablePowerUp : Interactuable
{
    
    private bool _powerUpUnlock = false;
    [SerializeField] private int _powerUp;
    [SerializeField] private int _powerUpPrice;
    [SerializeField] private GameObject UI;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Renderer _render;
    public void Awake()
    {
        _priceText.text = _powerUpPrice.ToString();
    }
    public override void Interaccion()
    {
        Interactuando();
    }

    private void Interactuando()
    {
        var dineroJugador = _personaje._statsPersonaje._dineroActual;
        if (dineroJugador >= _powerUpPrice && !_powerUpUnlock)
        {
            _personaje._statsPersonaje._dineroActual -= _powerUpPrice;
            _personaje.GetComponent<InputManagerControls>()._interactuando = false;
            gameObject.tag = "Untagged";
            _powerUpUnlock = true;
            UI.gameObject.GetComponent<Animator>().Play("Close");
            _render.material.color = Color.black;
            switch (_powerUp)
            {
                case 1:
                    _personaje._statsPersonaje.VidaMax += _personaje._statsPersonaje.VidaMax + 2;
                    break;

                case 2:
                    _playerMovement.InfinityStamina();
                    break;

                default:
                    _personaje._statsPersonaje.VidaMax += _personaje._statsPersonaje.VidaMax + 2;
                    break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_powerUpUnlock)
        {
            UI.gameObject.SetActive(true);
            UI.gameObject.GetComponent<Animator>().Play("Open");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_powerUpUnlock)
        {
            UI.gameObject.GetComponent<Animator>().Play("Close");
        }
    }
}