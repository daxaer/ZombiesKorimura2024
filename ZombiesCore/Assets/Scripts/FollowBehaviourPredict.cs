using UnityEngine.AI;
using UnityEngine;

public class FollowBehaviourPredict : StateMachineBehaviour
{
    private NavMeshAgent enemigo;
    [SerializeField] Transform playerPosicion;
    public float speed;
    [SerializeField] private bool useMovementPrediction;
    [SerializeField] [Range(-1,1)]private float MovementPredictTreshold = 0;
    [SerializeField] [Range(0.25f,2f)]private float MovementPredictionTime = 1f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPosicion = GameObject.FindGameObjectWithTag("Player").transform;
        enemigo = animator.GetComponent<NavMeshAgent>();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemigo.isActiveAndEnabled)
        {
            if (!useMovementPrediction)
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
            else
            {
                float timeToPlayer = Vector3.Distance(playerPosicion.transform.position, enemigo.transform.position) / enemigo.speed; 
                if (timeToPlayer < MovementPredictionTime)
                {
                    timeToPlayer = MovementPredictionTime;
                }
                Vector3 targetPosition = playerPosicion.transform.position + playerPosicion.GetComponent<PlayerMovement>().AverageVelocity * timeToPlayer;
                Vector3 directionToTarget = (targetPosition - enemigo.transform.position).normalized;
                Vector3 directionToPlayer = (playerPosicion.transform.position - enemigo.transform.position).normalized;    

                float dot = Vector3.Dot(directionToPlayer, directionToTarget);

                if(dot < MovementPredictTreshold)
                {
                    targetPosition = playerPosicion.transform.position;
                }

                enemigo.SetDestination(targetPosition);
            }
           
           
        }
    }
}
