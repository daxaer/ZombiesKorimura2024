using UnityEngine;
using UnityEngine.UI;

public class InteractuablePowerUp : Interactuable
{
    
    private bool _powerUpUnlock = false;
    [SerializeField] private int _powerUp;
    [SerializeField] private int _powerUpPrice;

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
}