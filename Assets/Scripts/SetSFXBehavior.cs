using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSFXBehavior : StateMachineBehaviour
{
    [SerializeField] private AudioClip _sfxSource;
    [SerializeField] private float _volume = 1f;
    [SerializeField] private bool _playOnEnter, _playOnExit, _playDelayed;

    [SerializeField] private float _playDelay = 0.25f;
    private float _timeSinceStart = 0f;
    private bool _hasDelayedAudioPlayed = false; 

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playOnEnter) {
            AudioSource.PlayClipAtPoint(_sfxSource, animator.gameObject.transform.position, _volume);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playDelayed && !_hasDelayedAudioPlayed) {
            _timeSinceStart += Time.deltaTime;
            if (_timeSinceStart > _playDelay) {
                AudioSource.PlayClipAtPoint(_sfxSource, animator.gameObject.transform.position, _volume);
                _hasDelayedAudioPlayed = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playOnExit) {
            AudioSource.PlayClipAtPoint(_sfxSource, animator.gameObject.transform.position, _volume);
        }
    }
}
