using UnityEngine;

public class AtackBehaviour : StateMachineBehaviour
{
    [SerializeField] Transform playerPosicion;
    [SerializeField] private int _damageToApply = 1;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPosicion = GameObject.FindGameObjectWithTag("Player").transform;
        var isDamagaeble = playerPosicion.GetComponent<Damageable>();
        isDamagaeble?.DoDamage(_damageToApply);
    }
}