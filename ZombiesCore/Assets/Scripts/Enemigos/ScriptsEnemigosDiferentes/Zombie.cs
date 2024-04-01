
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Enemy
{
    Coroutine knockbackCoroutine;
    private void Start()
    {
        //StartState(_enemyStatesConfiguration.GetInitialState());
        EncontrarEnemigoMasCercano.Instance.AddEnemigo(this);

    }

    public override void DoDamage(int damage)
    {
        _vidaEnemigo -= damage;
        if (_vidaEnemigo <= 0)
        {
            //if (knockbackCoroutine != null) // Verifica si la corrutina está en ejecución
            //{
            //    StopCoroutine(knockbackCoroutine); // Detiene la corrutina
            //}
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
        StopCoroutine("ApliKnockback");
        getKnockback = false;
    }

   
    //private IEnumerator KnockBack(Vector3 force)
    //{
    //    yield return null;
    //    agent.enabled = false;
    //    rb.useGravity = true;
    //    rb.isKinematic = false;
    //    rb.AddForce(force);

    //    yield return new WaitForFixedUpdate();
    //    float knockbacktime = Time.time;
    //    yield return new WaitUntil(() => rb.velocity.magnitude < stillThreshold /*|| Time.time > knockbacktime + MaxKnockbacktime*/);
    //    yield return new WaitForSeconds(0.25f);

    //    rb.velocity = Vector3.zero;
    //    rb.angularVelocity = Vector3.zero;  
    //    rb.useGravity = false;
    //    rb.isKinematic= true;
    //    agent.Warp(transform.position);
    //    agent.enabled = true;

    //    yield return null;
    //}

}
