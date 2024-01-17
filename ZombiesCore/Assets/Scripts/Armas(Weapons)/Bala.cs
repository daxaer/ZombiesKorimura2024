
using UnityEngine;
using UnityEngine.InputSystem;


//POSIBLE CLASE ABSTRACTA PARA FUTURAS BALAS
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Bala : RecyclableObject
{
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
        var objetoDamageable = other.GetComponent<Damageable>();
        objetoDamageable?.DoDamage(1);
        CancelInvoke(nameof(Reciclar));
        Reciclar();
    }

    private void OnEnable()
    {

        Invoke(nameof(Reciclar), _tiempoDeVidaBala);
    }
    //public void ConfigureTarget(RecyclableObject target)
    //{
    //    //_targetPosition = target.transform.position;
    //    //_targetDireccion = _targetPosition - transform.position;
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
        }
    }
}
