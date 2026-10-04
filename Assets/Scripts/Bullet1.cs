using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 3f;
    public float hitThreshold = 0.5f; 

    private Vector3 direction = Vector3.zero;
    private Transform target;

    public void Initialize(Vector3 dir, Transform assignedTarget)
    {
        direction = dir.normalized;
        target = assignedTarget;
        
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (direction == Vector3.zero) return;
        
        transform.position += direction * speed * Time.deltaTime;
        
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            
            if (distance <= hitThreshold)
            {
                Destroy(target.gameObject); 
                Destroy(gameObject);        
            }
        }
    }
}
