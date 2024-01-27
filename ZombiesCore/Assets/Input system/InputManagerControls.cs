using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerControls : MonoBehaviour
{
    [SerializeField] private bool _useJoystick;
    [SerializeField] private GameObject[] _interfazMovil;
    private PlayerMovement _playerMovement;
    private Personaje _personaje;
    public bool _interactuando;
    private Collider interactuableColision;


    public delegate void _PersonajeAction();

    public static event _PersonajeAction PersonajeAction;

    private Controles controles;

    private void Awake()
    {
        _interfazMovil = GameObject.FindGameObjectsWithTag("Movil");
        _playerMovement = GetComponent<PlayerMovement>();
        _personaje = GetComponent<Personaje>();


        controles = new Controles();
        if (!_useJoystick)
        {
            for (int i = 0; i < _interfazMovil.Length; i++)
            {
                Destroy(_interfazMovil[i]);
            }
        }
    }

    private void OnEnable()
    {
        controles.Gameplay.Enable();
        controles.Gameplay.Move.performed += MovePlayer;
        controles.Gameplay.Move.canceled += MovePlayer;
        controles.Gameplay.Action.performed += ActionPlayer;
        controles.Gameplay.Action.canceled += ActionPlayer;
        controles.Gameplay.Run.performed += RunPlayer;
        controles.Gameplay.Run.canceled += RunPlayer;
        controles.Gameplay.CambiarCamara.performed += CambiarCamarasAccion;
        controles.Gameplay.CambiarCamara.canceled += CambiarCamarasAccion;
    }

    private void CambiarCamarasAccion(InputAction.CallbackContext context)
    {
        var cambiarCamaraPresionado = context.ReadValueAsButton();
        if (cambiarCamaraPresionado)
        {
            var camera3eraPersona = CambiadorCamaras.TerceraPersonaCamara;
            var cameraPrimeraPersona = CambiadorCamaras.PrimeraPersonaCamara;

            CambiadorCamaras.CambiarCamara(CambiadorCamaras.EstaCamaraActiva(camera3eraPersona) ? cameraPrimeraPersona : camera3eraPersona);
            
        }

    }

    private void ActionPlayer(InputAction.CallbackContext obj)
    {
        var presButton = obj.ReadValueAsButton();
        if (!presButton)
        {
            if (_interactuando)
            {
                interactuableColision.GetComponent<Interactuable>().Interaccion();
            }
            else
            {
                PersonajeAction?.Invoke();
            }
        }
    }
    private void RunPlayer(InputAction.CallbackContext obj)
    {
        _playerMovement.RunPlayer(obj.ReadValueAsButton());
    }

    private void MovePlayer(InputAction.CallbackContext obj)
    {
        var movementDir = obj.ReadValue<Vector2>();
        _playerMovement.AsignarDireccion(movementDir);

    }

    private void OnDisable()
    {
        controles.Gameplay.Disable();
        controles.Gameplay.Move.performed -= MovePlayer;
        controles.Gameplay.Move.canceled -= MovePlayer;
        controles.Gameplay.Action.performed -= ActionPlayer;
        controles.Gameplay.Action.canceled -= ActionPlayer;
        controles.Gameplay.Run.performed -= RunPlayer;
        controles.Gameplay.Run.canceled -= RunPlayer;
        controles.Gameplay.CambiarCamara.performed -= CambiarCamarasAccion;
        controles.Gameplay.CambiarCamara.canceled -= CambiarCamarasAccion;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Interactuable")) return;
        if (_useJoystick)
        {
            //logica de cuando use joystick
        }

        _interactuando = true;
        interactuableColision = other;
        other.GetComponent<Interactuable>()?.ActivarUI(_interactuando);
        other.GetComponent<Interactuable>().Personaje = _personaje;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Interactuable")) return;
        if (_useJoystick)
        {
            //logica de cuando use joystick
        }

        interactuableColision = null;
        _interactuando = false;
        other.GetComponent<Interactuable>().ActivarUI(_interactuando);
    }
}