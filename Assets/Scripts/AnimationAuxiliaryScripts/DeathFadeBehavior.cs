using UnityEngine;

public class DeathFadeBehavior : StateMachineBehaviour
{
    private float _timeElapsed = 0f;
    [SerializeField] private float _fadeTime = 0f;
    
    private SpriteRenderer _spriteRenderer;
    private GameObject _objToDestroy;
    private Color _startColor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _objToDestroy = animator.gameObject;
        _spriteRenderer = animator.GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeElapsed += Time.deltaTime;

        _spriteRenderer.color = new Color(_startColor.r, _startColor.b, _startColor.g, _startColor.a * (1 - _timeElapsed / _fadeTime));

        if (_timeElapsed > _fadeTime) {
            Destroy(_objToDestroy);
        }
    }
}
