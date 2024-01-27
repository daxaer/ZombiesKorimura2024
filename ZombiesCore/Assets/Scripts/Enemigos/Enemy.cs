using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : RecyclableObject, Damageable, ITarget, InterfaceKnockback
{
    public string IdEnemigo => _idEnemigo;

    public Vector3 CurrentPosition => transform.position;


    [SerializeField] protected private float _minDistanceParaAtacar;
    [SerializeField] protected private float _rangoDeVision;
    [SerializeField] protected private string _idEnemigo;
    [SerializeField] protected private Transform _target;
    [SerializeField] protected private int _vidaEnemigo;
    [SerializeField] protected private float _velocidadMovimiento;
    [SerializeField] protected private int _damageEnemy;
    [SerializeField] protected private SpawnLoot _loot;
    [SerializeField] protected Rigidbody rb;
    protected private EnemyStatesConfiguration _enemyStatesConfiguration;
    protected private TargetFinder _targetsFinder;

    public abstract void DoDamage(int damage);

    public delegate void _EnemyDead();
    public static event _EnemyDead EnemyDeadEvent;
    //knockaback
    [Range(0.001f, 0.1f)][SerializeField] protected float stillThreshold = 0.05f;

    public virtual void UseEventEnemyDeadEvent()
    {
        EnemyDeadEvent?.Invoke();
        _loot.SpawnLootProbability();
    }

    public virtual void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    public abstract void GetKnockback(Vector3 force);
    
}
