using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : RecyclableObject, Damageable, ITarget//, InterfaceKnockback
{
    public string IdEnemigo => _idEnemigo;

    public Vector3 CurrentPosition => transform.position;

    [SerializeField] protected private float _minDistanceParaAtacar;
    [SerializeField] protected private float _rangoDeVision;
    [SerializeField] protected private string _idEnemigo;
    [SerializeField] protected private Transform _target;
    [SerializeField] protected private float _enemyLife;
    [SerializeField] protected private int _starEnemyLife;
    [SerializeField] protected private float _velocidadMovimiento;
    [SerializeField] protected private int _damageEnemy;
    [SerializeField] protected private SpawnLoot _loot;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected bool getKnockback;
    [SerializeField] protected float stun;
    [SerializeField] protected private NavMeshAgent agent;
    [SerializeField] protected private Vector3 impact;
    [SerializeField] protected private float KnockbackStrengh;
    [SerializeField] protected private AudioConfig[] audios;
    //protected private EnemyStatesConfiguration _enemyStatesConfiguration;
    //protected private TargetFinder _targetsFinder;

    public void  SetEnemyLife(float lifeMultiplier)
    {
        Debug.Log("Enemi life" + _enemyLife); 
        _enemyLife = _starEnemyLife * lifeMultiplier;
    }

    public abstract void DoDamage(int damage);

    public delegate void _EnemyDead();
    public static event _EnemyDead EnemyDeadEvent;
    
    //knockaback
    [Range(0.001f, 0.1f)][SerializeField] protected float stillThreshold = 0.05f;
    public virtual float Stun{ get => stun; set => stun = value; }

    public virtual void UseEventEnemyDeadEvent()
    {
        EnemyDeadEvent?.Invoke();
        PuntuacionManager.Instance.SetEnemigosDerrotados();
        _loot.SpawnLootProbability();
    }

    public virtual void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }
    public virtual bool GetKnockback()
    {
        return getKnockback;
    }
    public virtual void StarKnockback(float _stun, Vector3 _impact, float _knockbackStrength)
    {
        KnockbackStrengh = _knockbackStrength;
        impact = _impact;
        getKnockback = true;
        Stun = _stun;
        agent.enabled = false;
        StopCoroutine("ApliKnockback");
        StartCoroutine("ApliKnockback");
        
    }

    public void FixedUpdate()
    {
        if(getKnockback)
        {
            Vector3 knockbackDirection = impact - this.transform.position;
            knockbackDirection.Normalize();
            this.GetComponent<Rigidbody>().AddForce(knockbackDirection * (KnockbackStrengh * Time.fixedDeltaTime), ForceMode.Impulse);
            getKnockback = false;
        }
    }
    IEnumerator ApliKnockback()
    {
        yield return new WaitForSeconds(Stun);
        agent.enabled = true;
    }
}
