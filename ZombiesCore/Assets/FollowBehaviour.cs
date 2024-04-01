using UnityEngine.AI;
using UnityEngine;

public class FollowBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Enemy enemi;
    [SerializeField] Transform playerPosicion;
    public float speed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPosicion = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        enemi = animator.GetComponent<Enemy>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(agent.isActiveAndEnabled && !enemi.GetKnockback())
        {
            agent.SetDestination(playerPosicion.position);
            var distancia = animator.transform.position - playerPosicion.transform.position;
            var distanciaAPlayer = distancia.magnitude;
            if (distanciaAPlayer < 1)
            {
                animator.SetTrigger("atack");
                Debug.Log("atacando");
            }
            Vector3 direccion = agent.velocity.normalized;

            float movimientoX = direccion.x;
            float movimientoY = direccion.z;

            animator.SetFloat("movimientoX", movimientoX);
            animator.SetFloat("movimientoY", movimientoY);
        }
        else if(enemi.GetKnockback())
        {
            animator.SetBool("isFollow", false);
        }
    }
}
