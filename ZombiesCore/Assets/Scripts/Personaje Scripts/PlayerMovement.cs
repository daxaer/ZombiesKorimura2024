using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    private Vector3 _myDirection;
    private Personaje _miJugador;
    private bool _playerRun;
    private bool _rechargeRun;
    private float _delayRun;
    private float _currentStamina;
    private float _maXStamina;

    private void Start()
    {
        _rechargeRun = false;
        _maXStamina = 5;
        _currentStamina = 5;
        _playerRigidbody = GetComponent<Rigidbody>();
        _myDirection = Vector3.zero;
        _miJugador = GetComponent<Personaje>();
    }

    
    private void FixedUpdate()
    {
        if (_playerRun && _currentStamina > 0 && !_rechargeRun)
        {
            if (_myDirection == Vector3.zero)
            {

            }
            else
            {
                _playerRigidbody.velocity = _myDirection.normalized * (_miJugador._statsPersonaje.VelocidadMax);
                _currentStamina -= Time.fixedDeltaTime;
            }
        }
        else
        {
            if (_currentStamina <= 0)
            {
                _delayRun = 2f;
                _rechargeRun = true;
            }
            _playerRigidbody.velocity = _myDirection.normalized * (_miJugador._statsPersonaje.VelocidadMovimiento);
            _currentStamina += Time.fixedDeltaTime;
            _currentStamina = Mathf.Min(_currentStamina, _maXStamina);
            if (_currentStamina <= 0 && !_rechargeRun)
            {
                _delayRun = 2f;
                _rechargeRun = true;
            }
        }
        if (_rechargeRun)
        {
            _delayRun -= Time.deltaTime;
            if (_delayRun < 0)
            {
                _rechargeRun = false;
            }
        }
    }

    public void RunPlayer(bool runPlayer)
    {
        _playerRun = runPlayer;
    }

    public void AsignarDireccion(Vector2 direccion)
    {
        _myDirection.x = direccion.x;
        _myDirection.z = direccion.y;
    }
}
