using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;        
    public float detectionRange = 5f;  
    public float moveSpeed = 2f;     

    private bool isChasing = false;  

    void Update()
    {
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        
        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

       
        if (isChasing)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}