using UnityEngine.AI;
using UnityEngine;
using Unity.VisualScripting;
using System.Reflection.Emit;

public class FollowBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;
    private Enemy enemi;
    [SerializeField] Transform playerPosicion;
    float movimientoX;
    float movimientoY;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        playerPosicion = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        enemi = animator.GetComponent<Enemy>();
        rb = animator.GetComponent<Rigidbody>();
        rb.constraints |= RigidbodyConstraints.FreezeRotation;
        agent.enabled = true;
    }
    private void FixedUpdate()
    {

    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.isActiveAndEnabled && !enemi.GetKnockback())
        {
            agent.SetDestination(playerPosicion.position);
            var distancia = animator.transform.position - playerPosicion.transform.position;
            var distanciaAPlayer = distancia.magnitude;
            if (distanciaAPlayer < 1)
            {
                agent.enabled = false;
                animator.SetTrigger("atack");
                Debug.Log("atacando");
            }
            Vector3 direccion = agent.velocity.normalized;

            movimientoX = direccion.x;
            movimientoY = direccion.z;

            animator.SetFloat("movimientoX", movimientoX);
            animator.SetFloat("movimientoY", movimientoY);
        }
        else if (enemi.GetKnockback())
        {
            animator.SetBool("isFollow", false);
        }
    }
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Descongelar la posición al salir del estado
        rb.constraints |= RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }
}
