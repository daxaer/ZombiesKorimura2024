using System.Threading.Tasks;
using UnityEngine.Assertions;

public class AttackToTargetState : IEnemyState
{
    private readonly int _damageToApply;

    public AttackToTargetState(int damageToApply)
    {
        _damageToApply = damageToApply;
    }
    public Task<StateResult> HazAccion(object data)
    {
        var target = data as Personaje;
        Assert.IsNotNull(target);
        var isDamagaeble = target.GetComponent<Damageable>();
        isDamagaeble?.DoDamage(_damageToApply);
        return Task.FromResult(new StateResult(EnemyStatesConfiguration.IdleState));
    }
}