using UnityEngine;
using UnityEngine.AI;

public class AtackBehaviour : StateMachineBehaviour
{
    [SerializeField] Transform playerPosicion;
    [SerializeField] private int _damageToApply = 1;
    private NavMeshAgent agent;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPosicion = GameObject.FindGameObjectWithTag("Player").transform;
        var isDamagaeble = playerPosicion.GetComponent<Damageable>();
        isDamagaeble?.DoDamage(_damageToApply);
        agent = animator.GetComponent<NavMeshAgent>();

        Vector3 agentPosition = animator.GetComponent<Transform>().position;
        
        Vector3 direccion = playerPosicion.transform.position - agentPosition;
        direccion.Normalize();

        float movimientoX = direccion.x;
        float movimientoY = direccion.z;

        animator.SetFloat("movimientoX", movimientoX);
        animator.SetFloat("movimientoY", movimientoY);
    }
}