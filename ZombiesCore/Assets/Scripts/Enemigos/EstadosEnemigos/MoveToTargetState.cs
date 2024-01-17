using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

public class MoveToTargetState : IEnemyState
{
    private readonly Enemy _enemy;
    private readonly Rigidbody _enemyRigidbody;
    private readonly float _sqrMinDistanceToAttack;
    private readonly float _movementSpeed;

    public MoveToTargetState(Enemy enemy, float minDistanceToAttack, float movementSpeed)
    {
        _enemy = enemy;
        _enemyRigidbody = _enemy.GetComponent<Rigidbody>();
        _movementSpeed = movementSpeed;
        _sqrMinDistanceToAttack = minDistanceToAttack * minDistanceToAttack;
    }
    public async Task<StateResult> HazAccion(object data)
    {
        var target = data as Personaje;
        Assert.IsNotNull(target);
        var distanceToTheTarget = target.CurrentPosition - _enemy.CurrentPosition;

        do
        {
            _enemyRigidbody.velocity = (distanceToTheTarget.normalized * (_movementSpeed * Time.deltaTime));
            await Task.Yield();
            distanceToTheTarget = (target.CurrentPosition - _enemy.CurrentPosition);
        } while (distanceToTheTarget.sqrMagnitude > _sqrMinDistanceToAttack);

        return new StateResult(EnemyStatesConfiguration.AttackingTargetState, target);
    }
}