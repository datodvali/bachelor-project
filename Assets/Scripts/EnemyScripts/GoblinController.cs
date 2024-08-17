using UnityEngine;

public class GoblinController : BaseGroundEnemy {

    [SerializeField] private DetectionZone _rangedAttackZone;

    protected override void UpdateImpl() {
        if (_rangedAttackZone.detectedColliders.Count > 0 ) {
            _animator.SetBool(AnimationNames.hasRangedAttackTarget, true);
        } else {
            _animator.SetBool(AnimationNames.hasRangedAttackTarget, false);
        }
    }
}