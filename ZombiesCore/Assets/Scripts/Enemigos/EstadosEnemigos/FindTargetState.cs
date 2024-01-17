using System.Threading.Tasks;
using UnityEngine;

public class FindTargetState : IEnemyState
{
    private readonly ITargetFinder _targetFinder;
    private readonly Enemy _enemy;
    private readonly float _sqrMaxDistance;

    public FindTargetState(Enemy enemy, float visionRange, ITargetFinder targetFinder)
    {
        _enemy = enemy;
        _sqrMaxDistance = visionRange * visionRange;
        _targetFinder = targetFinder;
    }

    public Task<StateResult> HazAccion(object data)
    {
        var targets = _targetFinder.FindTargets();
        var distMasCercana = _sqrMaxDistance;
        Personaje targetReal = null;
        foreach (var target in targets)
        {
            //if (target == _enemy)
            //{
            //    continue;
            //}

            var sqrDistanceToTheTarget = (target.CurrentPosition - _enemy.CurrentPosition).sqrMagnitude;

            if (sqrDistanceToTheTarget > _sqrMaxDistance)
            {
                continue;
            }
            if(sqrDistanceToTheTarget < distMasCercana)
            {
                distMasCercana = sqrDistanceToTheTarget;
                targetReal = target;
            }
        }
        if(targetReal!= null)
            return Task.FromResult(new StateResult(EnemyStatesConfiguration.MovingToTargetState, targetReal));
        return Task.FromResult(new StateResult(EnemyStatesConfiguration.IdleState));
    }
}