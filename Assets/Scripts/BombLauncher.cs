using UnityEngine;

public class BombLauncher : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _launchPoint;

    public void FireBomb() {
        GameObject projectile = Instantiate(_projectilePrefab, _launchPoint.position, _projectilePrefab.transform.rotation);
        Vector3 originalScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(originalScale.x * transform.localScale.x > 0 ? 1 : -1, originalScale.y, originalScale.z);
    }
}
