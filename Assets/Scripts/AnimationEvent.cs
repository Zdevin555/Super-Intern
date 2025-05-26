using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : StateMachineBehaviour
{
    [SerializeField]
    private bool m_IsSkill;

    [SerializeField]
    private bool m_IsHurt;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_IsSkill)
        {
            animator.gameObject.GetComponent<PlayerManager>().IsSkill = false;
        }
        if (m_IsHurt)
        {
            animator.gameObject.GetComponent<PlayerManager>().IsInHurt = false;
        }
    }



}
