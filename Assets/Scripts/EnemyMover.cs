using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public enum CurveType { Quadratic, Cubic }

    [Header("Path Settings")]
    public CurveType curveType;
    public Transform p0, p1, p2, p3; // p3 is only used for Cubic
    public float travelDuration = 3f;

    private float elapsedTime = 0f;

    public void InitializeQuadratic(Transform start, Transform control, Transform target, float duration)
    {
        curveType = CurveType.Quadratic;
        p0 = start;
        p1 = control;
        p2 = target;
        travelDuration = duration;
    }

    public void InitializeCubic(Transform start, Transform control1, Transform control2, Transform target, float duration)
    {
        curveType = CurveType.Cubic;
        p0 = start;
        p1 = control1;
        p2 = control2;
        p3 = target;
        travelDuration = duration;
    }

    private void Update()
    {
        if (p0 == null || p2 == null) return;

        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / travelDuration); 

        Vector3 currentPosition = Vector3.zero;
        Vector3 moveDirection = Vector3.zero;

        if (curveType == CurveType.Quadratic)
        {
            currentPosition = Bezier.Quadratic(p0.position, p1.position, p2.position, t);
            moveDirection = Bezier.QuadraticTangent(p0.position, p1.position, p2.position, t);
        }
        else if (curveType == CurveType.Cubic)
        {
            currentPosition = Bezier.Cubic(p0.position, p1.position, p2.position, p3.position, t);
            moveDirection = Bezier.CubicTangent(p0.position, p1.position, p2.position, p3.position, t);
        }

        transform.position = currentPosition;
        
        if (moveDirection != Vector3.zero)
        {
            transform.up = moveDirection; 
        }

        if (t >= 1f)
        {
            OnReachTarget();
        }
    }

    private void OnReachTarget()
    {
        PlayerHPBar hpBar = FindFirstObjectByType<PlayerHPBar>();
        if (hpBar != null)
        {
            hpBar.TakeDamage(1f);
        }
        Destroy(gameObject);
    }
}