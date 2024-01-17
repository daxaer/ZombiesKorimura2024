
using UnityEngine;

public class Zombie : Enemy
{
   
    private void Start()
    {
        //StartState(_enemyStatesConfiguration.GetInitialState());
        EncontrarEnemigoMasCercano.Instance.AddEnemigo(this);

    }

    public override void DoDamage(int damage)
    {
        _vidaEnemigo -= damage;
        if(_vidaEnemigo<= 0)
        {
            UseEventEnemyDeadEvent();
            Reciclar();

        }
    }

    internal override void Init()
    {
        //Inicializar stats zombie 
        
    }

    internal override void Release()
    {
        
    }
}
