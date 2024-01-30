using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 mousePosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;

            GameObject projectile = 
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Vector3 lookAtDirection = targetPosition - transform.position;
            float angle = Mathf.Atan2(lookAtDirection.y, lookAtDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            projectile.transform.rotation = rotation;

            StartCoroutine(MoveProjectile(projectile.transform, targetPosition));

            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Apply damage to the enemy
                enemy.TakeDamage(1);
            }
        }
    }

    IEnumerator MoveProjectile(Transform projectileTransform, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float totalTime = Vector3.Distance
            (projectileTransform.position, targetPosition) / projectileSpeed;

        while (elapsedTime < totalTime)
        {
            projectileTransform.position = Vector3.Lerp
                (projectileTransform.position, targetPosition, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        projectileTransform.position = targetPosition;

        Destroy(projectileTransform.gameObject);
    }

}