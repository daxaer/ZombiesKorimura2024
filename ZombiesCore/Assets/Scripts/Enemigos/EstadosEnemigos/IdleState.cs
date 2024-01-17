
using System.Threading.Tasks;
using System;

public class IdleState : IEnemyState
{
    private readonly float _segundosDeEspera;

    public IdleState(float segundosDeEspera)
    {
        _segundosDeEspera = segundosDeEspera;
    }

    public async Task<StateResult> HazAccion(object data)
    {
        await Task.Delay(TimeSpan.FromSeconds(_segundosDeEspera));
        return new StateResult(EnemyStatesConfiguration.FindTargetState);
    }

    
}
