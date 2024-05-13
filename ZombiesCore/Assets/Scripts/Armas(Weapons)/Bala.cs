
using UnityEngine;
using UnityEngine.InputSystem;

//POSIBLE CLASE ABSTRACTA PARA FUTURAS BALAS
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Bala : RecyclableObject
{
    [SerializeField] private float _knockbackStrength;
    [SerializeField] bool ApliKnockback;
    [SerializeField] float stunTime;
    [SerializeField] float shotAngle;

    [SerializeField] private float _velocidadBala;
    [SerializeField] float velocidadMax;
    [SerializeField] float velocidadMin;
    [SerializeField] private int durabilidad;
    [SerializeField] private int durabilidadInicial;
    private Vector3 _targetDireccion;
    private Transform _posicionInicial;

    [Tooltip("El tiempo que durara viva la bala en la escena si no choca con ningun enemigo")]
    [SerializeField] private float _tiempoDeVidaBala = 5.0f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _posicionInicial = gameObject.GetComponentInParent<Transform>();
        //gameObject.transform.rotation = gameObject.GetComponentInParent<Transform>().rotation;
    }

    internal override void Init()
    {

    }
    private void FixedUpdate()
    {
        _velocidadBala = Random.Range(velocidadMin, velocidadMax);
        _rigidbody.MovePosition(transform.position + _targetDireccion * (_velocidadBala * Time.fixedDeltaTime));
        //var moveDirection = (_targetDireccion).normalized;
        //_rigidbody.MovePosition(transform.position + moveDirection * (_velocidadBala * Time.fixedDeltaTime));
    }

    internal override void Release()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IMoney moneyManager = GetComponent<IMoney>();
            if (moneyManager != null)
            {
                moneyManager.AddMoney(10);
            }

            if (ApliKnockback)
            {
                Rigidbody enemyrb = other.GetComponent<Rigidbody>();
                if (enemyrb != null)
                {
                    Debug.Log("knockback");
                    other.GetComponent<Enemy>().StarKnockback(stunTime, _posicionInicial.position, _knockbackStrength);
                }
            }
            durabilidad--;
            Debug.Log("durabilidad" + durabilidad);
            var objetoDamageable = other.gameObject.GetComponent<Damageable>();
            objetoDamageable?.DoDamage(1);
            if (durabilidad <= 0)
            {
                CancelInvoke(nameof(Reciclar));
                Reciclar();
            }
        }
    }


    private void OnEnable()
    {
        //_distanceStart = transform.position;
        Invoke(nameof(Reciclar), _tiempoDeVidaBala);
        durabilidad = durabilidadInicial;
    }
    //public Vector3 GetKnockbackStrength(Vector3 direction, float distance)
    //{
    //    return _knockbackStrength * _distanceFallOff.Evaluate(distance) * direction; 
    //}
    public void ConfigureTarget()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        
        if (Physics.Raycast(ray, out RaycastHit raycastHit,1000f,CursorManager.Instance.LayerSuelo))
        {
          
            _targetDireccion = raycastHit.point - transform.position;
            
            _targetDireccion.y = 0; 
            _targetDireccion.Normalize();

            float Angle = Random.Range(-shotAngle, shotAngle);
            _targetDireccion = Quaternion.Euler(0,Angle,0) * _targetDireccion;
            _targetDireccion.Normalize();
        }
    }

    //public float CalcularDistancia()
    //{
    //    return Vector3.Distance(_distanceStart, transform.position);
    //}
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
