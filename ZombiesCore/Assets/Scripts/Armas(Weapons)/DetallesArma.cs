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
    [SerializeField] private int _maxAmmo;
    [SerializeField] private int _currentAmmo;
    [SerializeField] private int _maxCharger;
    [SerializeField] private int _currentCharger;
    [SerializeField] private float _siguienteDisparo;
    [SerializeField] private float _perdigones;

    [SerializeField] private int _damageWeapon;

    public int IdArma { get => _idArma; }
    public int DamageWeapon { get => _damageWeapon; }
    public string Descripcion { get => _descripcion; }

    public float VelocidadDisparo { get => _velocidadDisparo;}
    public RecyclableObject Bala { get => _bala;}

    //public abstract void Atacar(/*RecyclableObject targetDisparo*/);
    public virtual void Atacar()
    {

    }

    public virtual void ConfigurarFactoriaArmas(FactoriaArmas factoriaArmas)
    {
        _factoriaArmas = factoriaArmas;
        UIManager.Instance.UpdateTotalAmmo(MaxAmmo, CurrentAmmo);
        UIManager.Instance.UpdateChargerAmmo(MaxCharger, CurrentCharger);
    }

    public virtual void RecoverMaxAmmo()
    {
        _maxAmmo = CurrentAmmo = MaxAmmo;
        CurrentCharger = MaxCharger;
        UIManager.Instance.UpdateTotalAmmo(MaxAmmo, MaxAmmo);
        UIManager.Instance.UpdateChargerAmmo(MaxCharger, MaxCharger);
    }
    public abstract IEnumerator Recargar();
    
    public abstract void RecargaAutomatica();

    public Transform BalaSpawnReference { get => _balaSpawnReference; }

    //recarga y cargador
    public int MaxAmmo { get => _maxAmmo;  set => _maxAmmo = value;}
          
    public int CurrentAmmo { get => _currentAmmo; set => _currentAmmo = value; }
           
    public int MaxCharger { get => _maxCharger; set => _maxCharger = value; }
           
    public int CurrentCharger { get => _currentCharger; set => _currentCharger = value; }
    
    public float TiempoRecarga { get => _tiempoRecarga; set => _tiempoRecarga = value; }
    
    public float TiempoRecargaAutomatica { get => _tiempoRecargaAutomatica; set => _tiempoRecargaAutomatica = value; }
    public float Perdigones { get => _perdigones; set => _perdigones = value; }

    public bool Recargando { get => _recargando; set => _recargando = value;}

    public float SiquienteDisparo { get => _siguienteDisparo; set => _siguienteDisparo = value; }
}

