using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Turret1 : MonoBehaviour
{
    public Bullet1 bulletPrefab;
    public float range = 5f;
    public float fireCooldown = 1.5f;

    private LineRenderer lineRenderer;
    private float cooldownTimer;
    private Transform currentTarget; 

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.loop = true;
        
        DrawRadiusRing();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        
        currentTarget = FindClosestEnemy();

        if (currentTarget == null) return;

        float distanceToEnemy = Vector3.Distance(transform.position, currentTarget.position);

        if (distanceToEnemy <= range && cooldownTimer <= 0f)
        {
            cooldownTimer = fireCooldown;
            Fire();
        }
    }

    void Fire()
    {
        Vector3 fireDir = (currentTarget.position - transform.position).normalized;
        
        Bullet1 bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.Initialize(fireDir, currentTarget);
    }
    
    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closest = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                closest = enemy.transform;
            }
        }

        return closest;
    }

    void DrawRadiusRing()
    {
        int segments = 36;
        lineRenderer.positionCount = segments + 1;
        
        for (int i = 0; i <= segments; i++)
        {
            float rad = (i * (360f / segments)) * Mathf.Deg2Rad;
            Vector3 pos = transform.position + new Vector3(Mathf.Cos(rad) * range, Mathf.Sin(rad) * range, 0);
            lineRenderer.SetPosition(i, pos);
        }
    }
}