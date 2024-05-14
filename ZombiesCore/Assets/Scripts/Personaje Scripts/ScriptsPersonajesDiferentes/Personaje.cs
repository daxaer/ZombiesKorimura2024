using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class StatsPersonaje 
{
    [SerializeField] private int _vidaMax;
    [SerializeField] private int _vidaActual;
    [Tooltip("Velocidad de movimiento Maxima")][SerializeField] private float _velocidadMax;
    [Tooltip("Velocidad de movimiento Maxima")][SerializeField] private float _velocidadMovimiento;
    [SerializeField] internal int _dineroActual;
    private int _dineroMaximo = 9999;
    [SerializeField] private int _dineroInicial;
    [SerializeField] private bool _moneyMultiplier;
    [SerializeField] private int _experienciaPorZombieGanada;
    private int _experiencia;
    private int _nivel;
    private static readonly int[] ExperienciaPorNivel = {100,150,200,250,300,350,400 } ;
    private bool _armaEspecialObtenida;

    #region LevelSystem

    public void AddExperiencia(int experienciaRecibida, int multiplicador = 1)
    {
        _experiencia += experienciaRecibida * multiplicador;
        while (_experiencia>= GetExperienciaParaSiguienteNivelAutomatico(_nivel))
        {
            _experiencia -= GetExperienciaParaSiguienteNivelAutomatico(_nivel);
            _nivel++;
        }
    }

    public int ExperienciaPorZombieGanada => _experienciaPorZombieGanada;
    public int DineroInicial => _dineroInicial;
    public bool MoneyMultiplier
    {
        get => _moneyMultiplier;
        set => _moneyMultiplier = value;
    }

    public int DineroMaximo => _dineroMaximo;
    public int VidaMax
    {
        get => _vidaMax;
        set => _vidaMax = value;
    }
    public int VidaActual
    {
        get => _vidaActual;
        set
        {
            _vidaActual = value;
            if (VidaActual <= 0)
            {
                _armaEspecialObtenida = false; //TODO: CAMBIAR PARA QUE CUANDO Te maten si eres zombie, SI te reviven conservas tu inventario
            }
        }
    }

    public int Experiencia
    {
        get=> _experiencia;
        set=> _experiencia = value;
    }
    public bool ArmaEspecialObtenida { get => _armaEspecialObtenida; set { _armaEspecialObtenida = value; } }

    public int Nivel => _nivel;
    public float VelocidadMovimiento
    {
        get => _velocidadMovimiento;
        set => _velocidadMovimiento = value;
    }
    public float VelocidadMax
    {
        get => _velocidadMax;
        set => _velocidadMax = value;
    }
    public float ExperienciaNormalizada => (float) _experiencia / GetExperienciaParaSiguienteNivelAutomatico(_nivel);
    public int GetExperienciaParaSiguienteNivelAutomatico(int nivel)
    {
        return nivel * 11;
    }

    /// <summary>
    /// Para hacer uso de ella, asegurarse que ya esten puestas de manera manual el nivel en el arreglo
    /// </summary>
    /// <param name="nivel"></param>
    /// <returns></returns>
    public int GetExperienciaParaSiguienteNivelManual(int nivel)
    {
        if (nivel < ExperienciaPorNivel.Length)
        {
            return ExperienciaPorNivel[nivel];
        }

        throw new Exception("Este nivel no existe : " + nivel);
    }
    public bool EsMaximoNivel => EsMaximoNivelf(_nivel);
    
    public bool EsMaximoNivelf(int nivel)
    {
        return nivel == ExperienciaPorNivel.Length - 1;
    }

   
    #endregion

}

public abstract class Personaje : MonoBehaviour, Damageable, ITarget
{
    public bool encontrandoEnemigo = false;
    [SerializeField] protected internal StatsPersonaje _statsPersonaje;
    [SerializeField] private protected ArmaSystem _armaSystem;
    public InterfaceArma Arma => _arma;
    private protected InterfaceArma _arma;
    private protected RecyclableObject _target;

    private protected FactoriaMainGameplay _factoriaActualGameplay;
    public string IdPersonaje => _idPersonaje;
    [SerializeField] private string _idPersonaje;

    public Transform PosicionArma => _posicionArma;

    public Vector3 CurrentPosition => transform.position;

    [SerializeField] private Transform _posicionArma;
    private protected EnemyStatesConfiguration _enemyStatesConfiguration;
    private protected TargetFinder _targetsFinder;

    public abstract void ConfigurarFactoriaGameplay(FactoriaMainGameplay factoriaActualGameplay);
    public abstract void HandlePersonajeAction();

    public void DoDamage(int damage)
    {
        if (_statsPersonaje.VidaActual == 0)
            return;
        _statsPersonaje.VidaActual -= damage;
        if (_statsPersonaje.VidaActual <= 0)
        {
            Debug.Log("Muerto");
            StopCoroutine("Recuperacion");
            _statsPersonaje.VelocidadMovimiento = 0;
            CursorManager.Instance.Damage(_statsPersonaje.VidaActual);
        }
        else
        {
            CursorManager.Instance.Damage(_statsPersonaje.VidaActual);
            StopCoroutine("Recuperacion");
            StartCoroutine("Recuperacion");
        }
    }
    IEnumerator Recuperacion()
    {
        Debug.Log("vida actual" + _statsPersonaje.VidaActual);
        yield return new WaitForSeconds(4f);
        _statsPersonaje.VidaActual = _statsPersonaje.VidaMax;
        CursorManager.Instance.Damage(_statsPersonaje.VidaActual);
    }

    private void Start()
    {
        InitStats();
    }
    private void InitStats()
    {
        _statsPersonaje.VelocidadMax = _statsPersonaje.VelocidadMax;
        _statsPersonaje.VelocidadMovimiento = _statsPersonaje.VelocidadMovimiento;
        _statsPersonaje.VidaActual = _statsPersonaje.VidaMax;
        _statsPersonaje.Experiencia = 0;
        _statsPersonaje._dineroActual = _statsPersonaje.DineroInicial;
        UIManager.Instance.UpdateMoney(_statsPersonaje._dineroActual);
    }
    
    public void StartMoneyX2()
    {
        StopCoroutine(MoneyX2());
        StartCoroutine(MoneyX2());
    }
    IEnumerator MoneyX2()
    {
        _statsPersonaje.MoneyMultiplier = true;
        yield return new WaitForSeconds(30f);
        _statsPersonaje.MoneyMultiplier = false;
    }

    public void AddMoney(int money)
    {
        if(_statsPersonaje.MoneyMultiplier)
        {
            _statsPersonaje._dineroActual += money * 2;
        }
        else
        {
            _statsPersonaje._dineroActual += money;
        }
        Debug.Log("SumarDinero");
        UIManager.Instance.UpdateMoney(_statsPersonaje._dineroActual);
    }
    public abstract void SetArma(InterfaceArma arma);
    public abstract void OnTriggerStay(Collider other);
}
