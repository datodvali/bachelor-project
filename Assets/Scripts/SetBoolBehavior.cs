using UnityEngine;

public class SetBoolBehavior : StateMachineBehaviour
{
    [SerializeField] private string _variableName;
    [SerializeField] private bool _updateOnStateEnter;
    [SerializeField] private bool _updateOnStateMachineEnter;
    [SerializeField] private bool _valueOnEnter;
    [SerializeField] private bool _valueOnExit;

    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash) {
        if (_updateOnStateMachineEnter) {
            animator.SetBool(_variableName, _valueOnEnter);
        }
    }

    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
        if (_updateOnStateMachineEnter) {
            animator.SetBool(_variableName, _valueOnExit);
        }
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_updateOnStateEnter) {
            animator.SetBool(_variableName, _valueOnEnter);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_updateOnStateEnter) {
            animator.SetBool(_variableName, _valueOnExit);
        }
    }
}
