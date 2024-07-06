using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _launchPoint;

    public void FireProjectile() {
        GameObject projectile = Instantiate(_projectilePrefab, _launchPoint.position, _projectilePrefab.transform.rotation);
        Vector3 orginalScale = projectile.transform.localScale;
        projectile.transform.localScale = new Vector3(orginalScale.x * transform.localScale.x > 0 ? 1 : -1, orginalScale.y, orginalScale.z);
    }
}
