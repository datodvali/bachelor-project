using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SetBoolBehavior : StateMachineBehaviour
{
    [SerializeField] private string _variableName;
    [SerializeField] private bool _updateOnStateEnter;
    [SerializeField] private bool _updateOnStateExit;
    [SerializeField] private bool _valueOnStateEnter;
    [SerializeField] private bool _valueOnStateExit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_updateOnStateEnter) {
            animator.SetBool(_variableName, _valueOnStateEnter);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_updateOnStateExit) {
            animator.SetBool(_variableName, _valueOnStateExit);
        }
    }
}
