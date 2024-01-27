using UnityEngine.AI;
using UnityEngine;

public class FollowBehaviour : StateMachineBehaviour
{
    private NavMeshAgent enemigo;
    [SerializeField] Transform playerPosicion;
    public float speed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPosicion = GameObject.FindGameObjectWithTag("Player").transform;
        enemigo = animator.GetComponent<NavMeshAgent>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemigo.isActiveAndEnabled)
        {
            enemigo.SetDestination(playerPosicion.position);
            var distancia = animator.transform.position - playerPosicion.transform.position;
            var distanciaAPlayer = distancia.magnitude;
            if (distanciaAPlayer < 1)
            {
                animator.SetTrigger("atack");
                Debug.Log("atacando");
            }
        }
    }
}
