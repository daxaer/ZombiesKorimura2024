
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Enemy
{
    NavMeshAgent agent;
    private void Start()
    {
        //StartState(_enemyStatesConfiguration.GetInitialState());
        agent = GetComponent<NavMeshAgent>();
        EncontrarEnemigoMasCercano.Instance.AddEnemigo(this);

    }

    public override void DoDamage(int damage)
    {
        _vidaEnemigo -= damage;
        if (_vidaEnemigo <= 0)
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
    public override void GetKnockback(Vector3 force)
    {
        if(gameObject.activeSelf)
        {
            StartCoroutine(KnockBack(force));
        }
    }

    private IEnumerator KnockBack(Vector3 force)
    {
        yield return null;
        agent.enabled = false;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(force);

        yield return new WaitForFixedUpdate();
        float knockbacktime = Time.time;
        yield return new WaitUntil(() => rb.velocity.magnitude < stillThreshold /*|| Time.time > knockbacktime + MaxKnockbacktime*/);
        yield return new WaitForSeconds(0.25f);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;  
        rb.useGravity = false;
        rb.isKinematic= true;
        agent.Warp(transform.position);
        agent.enabled = true;

        yield return null;
    }

}
