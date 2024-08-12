using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _launchPoint;

    public void FireProjectile() {
        GameObject projectile = Instantiate(_projectilePrefab, _launchPoint.position, _projectilePrefab.transform.rotation);
        Vector3 originalScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(originalScale.x * transform.localScale.x > 0 ? 1 : -1, originalScale.y, originalScale.z);
        projectile.transform.Rotate(0,0, projectile.transform.localScale.x > 0 ? 0 : -90); // need to do this rotation as the original arrow sprite is not horizontal
    }
}
