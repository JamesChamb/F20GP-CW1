using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        animator.SetInteger("IdleAnim", Random.Range(0, 3));
    }

}
