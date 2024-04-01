
using UnityEngine;
using UnityEngine.InputSystem;

//POSIBLE CLASE ABSTRACTA PARA FUTURAS BALAS
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Bala : RecyclableObject
{
    //retroceso
    [SerializeField] private float _knockbackStrength;
    [SerializeField] ParticleSystem.MinMaxCurve _distanceFallOff;
    [SerializeField] Vector3 _distanceStart;
    [SerializeField] Vector3 _distancetraveled;
    [SerializeField] bool ApliKnockback;
    [SerializeField] float stunTime;

    [SerializeField] private float _velocidadBala;
    private Vector3 _targetPosition;
    private Vector3 _targetDireccion;

    [Tooltip("El tiempo que durara viva la bala en la escena si no choca con ningun enemigo")]
    [SerializeField] private float _tiempoDeVidaBala = 5.0f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        //gameObject.transform.rotation = gameObject.GetComponentInParent<Transform>().rotation;
    }

    internal override void Init()
    {

    }
    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + _targetDireccion * (_velocidadBala * Time.fixedDeltaTime));
        //var moveDirection = (_targetDireccion).normalized;
        //_rigidbody.MovePosition(transform.position + moveDirection * (_velocidadBala * Time.fixedDeltaTime));
    }

    internal override void Release()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("tocando enemigo");
        if (other.CompareTag("Enemy") && ApliKnockback)
        {
            
            Rigidbody enemyrb = other.GetComponent<Rigidbody>();
            if (enemyrb != null)
            {
                Debug.Log("knockback");
                other.GetComponent<Enemy>().StarKnockback(stunTime, transform.position, _knockbackStrength);
            }
        }
        var objetoDamageable = other.gameObject.GetComponent<Damageable>();
        objetoDamageable?.DoDamage(1);
        CancelInvoke(nameof(Reciclar));
        Reciclar();
    }


    private void OnEnable()
    {
        _distanceStart = transform.position;
        Invoke(nameof(Reciclar), _tiempoDeVidaBala);
    }
    public Vector3 GetKnockbackStrength(Vector3 direction, float distance)
    {
        return _knockbackStrength * _distanceFallOff.Evaluate(distance) * direction; 
    }
    public void ConfigureTarget()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit raycastHit,1000f,CursorManager.Instance.LayerSuelo))
        {
          
            _targetDireccion = raycastHit.point - transform.position;
            
            _targetDireccion.y = 0; 
            _targetDireccion.Normalize();
        }
    }

    public float CalcularDistancia()
    {
        return Vector3.Distance(_distanceStart, transform.position);
    }
}
    //public void ConfigureTarget(RecyclableObject target)
    //{
    //    //_targetPosition = target.transform.position;
    //    //_targetDireccion = _targetPosition - transform.position;
    //}
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("tocando enemigo");
    //    if(collision.collider.CompareTag("Enemy") && ApliKnockback)
    //    {
            
    //    }
    //    var objetoDamageable = collision.gameObject.GetComponent<Damageable>();
    //    //Vector3 force = GetKnockbackStrength(-collision.impulse.normalized, CalcularDistancia());
    //    objetoDamageable?.DoDamage(1);
    //    CancelInvoke(nameof(Reciclar));
    //    Reciclar();
    //}
