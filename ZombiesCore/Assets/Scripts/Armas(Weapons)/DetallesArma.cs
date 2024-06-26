using System.Collections;
using UnityEngine;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif
public enum RarezaArma
{
    COMUN,
    ESPECIAL
}
[System.Serializable]
public abstract class DetallesArma : MonoBehaviour, InterfaceArma
{

    private protected FactoriaArmas _factoriaArmas;
    [SerializeField] private protected Transform _balaSpawnReference;
    [SerializeField] private int _idArma;
    [SerializeField] private float  _velocidadDisparo;
    [SerializeField] private RecyclableObject _bala;
    [SerializeField] private string _descripcion;
    [HideInInspector] public bool RequiereNivel;
    [SerializeField] private RarezaArma _rarezaArma;
    [HideInInspector] public int NivelRequerido;
    //recarga y cargador
    [SerializeField] public bool _recargando;
    [SerializeField] private float _tiempoRecarga;
    [SerializeField] private float _tiempoRecargaAutomatica;
    [SerializeField] private float _maxBalas;
    [SerializeField] private float _actualBalas;
    [SerializeField] private float _maxBalasCargador;
    [SerializeField] private float _actualbalasCargador;
    [SerializeField] private float _siguienteDisparo;

    [SerializeField] private GameObject _municion;
    [SerializeField] private GameObject _cargador;

    public int IdArma { get => _idArma; }
    public string Descripcion { get => _descripcion; }

    public float VelocidadDisparo { get => _velocidadDisparo;}
    public RecyclableObject Bala { get => _bala;}

    //public abstract void Atacar(/*RecyclableObject targetDisparo*/);
    public virtual void Atacar()
    {
        if (ActualBalasCargador <= 0) return;
        if (Recargando) return;
        CancelInvoke(nameof(RecargaAutomatica));
        if (!(Time.time > _siguienteDisparo)) return;
        _siguienteDisparo = Time.time + VelocidadDisparo;
        CursorManager.Instance.ActivarMira();
        _factoriaArmas.CrearBala(IdArma, _balaSpawnReference);
        ActualBalasCargador--;
        Cargador.GetComponent<TextMeshProUGUI>().text = ActualBalasCargador + "/" + MaxBalasCargador;
        Invoke(nameof(RecargaAutomatica), TiempoRecargaAutomatica);
        if (ActualBalasCargador <= 0)
        {
            StartCoroutine(nameof(Recargar));
        }
    }

    public virtual void ConfigurarFactoriaArmas(FactoriaArmas factoriaArmas)
    {
        _factoriaArmas = factoriaArmas;
        _municion = GameObject.Find("municion_text");
        _cargador = GameObject.Find("cargador_text");
        _municion.GetComponent<TextMeshProUGUI>().text = ActualBalas + "/" + MaxBalas;
        _cargador.GetComponent<TextMeshProUGUI>().text = ActualBalasCargador + "/" + MaxBalasCargador;
    }
    public abstract IEnumerator Recargar();
    
    public abstract void RecargaAutomatica();

    public Transform BalaSpawnReference { get => _balaSpawnReference; }

    //recarga y cargador
    public float MaxBalas { get => _maxBalas;  set => _maxBalas = value;}
    
    public float ActualBalas { get => _actualBalas; set => _actualBalas = value; }
    
    public float MaxBalasCargador { get => _maxBalasCargador; set => _maxBalasCargador = value; }
    
    public float ActualBalasCargador { get => _actualbalasCargador; set => _actualbalasCargador = value; }
    
    public float TiempoRecarga { get => _tiempoRecarga; set => _tiempoRecarga = value; }
    
    public float TiempoRecargaAutomatica { get => _tiempoRecargaAutomatica; set => _tiempoRecargaAutomatica = value; }
    
    public bool Recargando { get => _recargando; set => _recargando = value;}

    public float SiquienteDisparo { get => _siguienteDisparo; set => _siguienteDisparo = value; }
    public GameObject Municion { get => _municion; set => _municion = value; }
    public GameObject Cargador { get => _cargador; set => _cargador = value; }
}

