using System.Collections;
using UnityEngine;

public class SetSFXBehavior : StateMachineBehaviour
{
    [SerializeField] private AudioClip _sfxSource;
    [SerializeField] private float _volume = 1f;
    [SerializeField] private bool _playOnEnter, _playOnExit, _playDelayed;

    [SerializeField] private float _playDelay = 0.25f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playOnEnter && !_playDelayed) {
            PlayClip(animator);
        } else if (_playDelayed) {
            RunWithDelay(animator);
        }
    }


    IEnumerator RunWithDelay(Animator animator) {
        yield return new WaitForSeconds(_playDelay);
        Debug.Log("delayed");
        PlayClip(animator);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playOnExit) {
            PlayClip(animator);
        }
    }

    private void PlayClip(Animator animator) {
        AudioSource.PlayClipAtPoint(_sfxSource, animator.gameObject.transform.position, _volume);
    }
}
