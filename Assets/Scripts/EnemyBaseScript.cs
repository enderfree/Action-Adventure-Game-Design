using UnityEngine;

public class EnemyBaseScript : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public float speed = 5f;
    public Transform player;

    private Vector3 startPosition;
    private bool activeEnemy = false;

    void Start()
    {
        startPosition = transform.position;
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        if (activeEnemy && player != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }
    }

    public void ActivateEnemy(Transform playerTarget)
    {
        player = playerTarget;
        activeEnemy = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetEnemy()
    {
        transform.position = startPosition;
        currentHealth = maxHealth;
        gameObject.SetActive(true);
        activeEnemy = false;
    }

    public bool IsDead()
    {
        return !gameObject.activeSelf;
    }
}