using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : RecyclableObject, Damageable, ITarget
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

    protected private EnemyStatesConfiguration _enemyStatesConfiguration;
    protected private TargetFinder _targetsFinder;

    public abstract void DoDamage(int damage);

    public delegate void _EnemyDead();
    public static event _EnemyDead EnemyDeadEvent;


    public virtual void UseEventEnemyDeadEvent()
    {
        EnemyDeadEvent?.Invoke();
        _loot.SpawnLootProbability();
    }

}
