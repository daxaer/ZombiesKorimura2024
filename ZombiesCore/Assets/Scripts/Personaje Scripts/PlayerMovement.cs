using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(0.1f,5f)] private float historicalPositionDuration = 1f;
    [SerializeField][Range(0.001f, 1f)] private float historicalPositionInterval = 0.1f;
    private Queue<Vector3> historicalVelocities;
    private float LastPositionTime;
    private int MaxQueueSize;

    [SerializeField] private float _maxStamina;
    private Rigidbody _playerRigidbody;
    private Vector3 _myDirection;
    private Personaje _miJugador;
    private bool _playerRun;
    private bool _rechargeRun;
    private float _delayRun;
    private float _currentStamina;
    private bool _estaminaInfinita;

    [SerializeField]private Animator _animator;
    public Vector3 AverageVelocity
    {
        get
        {
            Vector3 average = Vector3.zero;
            foreach(Vector3 velocity in historicalVelocities)
            {
                average += velocity;
            }
            average.y = 0;

            return average / historicalVelocities.Count;
        }
    }
    private void Awake()
    {
        MaxQueueSize = Mathf.CeilToInt(1f / historicalPositionInterval * historicalPositionDuration);
        historicalVelocities = new Queue<Vector3>(MaxQueueSize);
        _estaminaInfinita = false;
    }
    private void Start()
    {
        _rechargeRun = false;
        _currentStamina = _maxStamina;
        _playerRigidbody = GetComponent<Rigidbody>();
        _myDirection = Vector3.zero;
        _miJugador = GetComponent<Personaje>();
    }
    private void Update()
    {
        if(historicalVelocities.Count > MaxQueueSize)
        {
            historicalVelocities.Dequeue();
        }
        historicalVelocities.Enqueue(_playerRigidbody.velocity);
        LastPositionTime = Time.time;

        _animator.SetFloat("x", _myDirection.x);
        _animator.SetFloat("y", _myDirection.z);
        _animator.SetFloat("velocidad", _playerRigidbody.velocity.magnitude);

    }

    private void FixedUpdate()
    {
        if (_playerRun && (!_estaminaInfinita || _currentStamina > 0) && !_rechargeRun) // Agrega la condición !_estaminaInfinita || _currentStamina > 0
        {
            if (_myDirection == Vector3.zero)
            {

            }
            else
            {
                _playerRigidbody.velocity = _myDirection.normalized * (_miJugador._statsPersonaje.VelocidadMax);
                if (!_estaminaInfinita) // Solo disminuye la estamina si no es infinita
                {
                    _currentStamina -= Time.fixedDeltaTime;
                }
            }
        }
        else
        {
            if (!_estaminaInfinita && _currentStamina <= 0) // Agrega la condición !_estaminaInfinita
            {
                _delayRun = 2f;
                _rechargeRun = true;
            }
            _playerRigidbody.velocity = _myDirection.normalized * (_miJugador._statsPersonaje.VelocidadMovimiento);
            if (!_estaminaInfinita) // Solo incrementa la estamina si no es infinita
            {
                _currentStamina += Time.fixedDeltaTime;
                _currentStamina = Mathf.Min(_currentStamina, _maxStamina);
                if (_currentStamina <= 0 && !_rechargeRun)
                {
                    _delayRun = 2f;
                    _rechargeRun = true;
                }
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
    public void InfinityStamina( )
    {
        _estaminaInfinita = true;
    }

    public void AsignarDireccion(Vector2 direccion)
    {
        _myDirection.x = direccion.x;
        _myDirection.z = direccion.y;
    }
    
}
