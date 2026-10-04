using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Shared Target")]
    public Transform sharedTargetLocation; // P2 (Quadratic) & P3 (Cubic)

    [Header("Spawn Point 1 - Quadratic Bézier (3 Points)")]
    public Transform quadSpawnPoint;    // P0
    public Transform quadControlPoint;  // P1

    [Header("Spawn Point 2 - Cubic Bézier (4 Points)")]
    public Transform cubicSpawnPoint;   // P0
    public Transform cubicControlPoint1; // P1
    public Transform cubicControlPoint2; // P2

    [Header("Prefab & Timing")]
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float enemyTravelTime = 4f;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnQuadraticEnemy();
            SpawnCubicEnemy();
            timer = 0f;
        }
    }

    public void SpawnQuadraticEnemy()
    {
        GameObject enemyObj = Instantiate(enemyPrefab, quadSpawnPoint.position, Quaternion.identity);
        EnemyMover mover = enemyObj.GetComponent<EnemyMover>();
        mover.InitializeQuadratic(quadSpawnPoint, quadControlPoint, sharedTargetLocation, enemyTravelTime);
    }

    public void SpawnCubicEnemy()
    {
        GameObject enemyObj = Instantiate(enemyPrefab, cubicSpawnPoint.position, Quaternion.identity);
        EnemyMover mover = enemyObj.GetComponent<EnemyMover>();
        mover.InitializeCubic(cubicSpawnPoint, cubicControlPoint1, cubicControlPoint2, sharedTargetLocation, enemyTravelTime);
    }
    
    private void OnDrawGizmos()
    {
        if (sharedTargetLocation == null) return;

        // Draw Quadratic Path (Green)[cite: 9]
        if (quadSpawnPoint != null && quadControlPoint != null)
        {
            Gizmos.color = Color.green;
            Vector3 prevPt = quadSpawnPoint.position;
            for (int i = 1; i <= 20; i++)
            {
                float t = i / 20f;
                Vector3 pt = Bezier.Quadratic(quadSpawnPoint.position, quadControlPoint.position, sharedTargetLocation.position, t);
                Gizmos.DrawLine(prevPt, pt);
                prevPt = pt;
            }
        }
        
        if (cubicSpawnPoint != null && cubicControlPoint1 != null && cubicControlPoint2 != null)
        {
            Gizmos.color = Color.cyan;
            Vector3 prevPt = cubicSpawnPoint.position;
            for (int i = 1; i <= 20; i++)
            {
                float t = i / 20f;
                Vector3 pt = Bezier.Cubic(cubicSpawnPoint.position, cubicControlPoint1.position, cubicControlPoint2.position, sharedTargetLocation.position, t);
                Gizmos.DrawLine(prevPt, pt);
                prevPt = pt;
            }
        }
    }
}