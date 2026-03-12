using UnityEngine;

public class RoomCombatManager : MonoBehaviour
{
    public EnemyBaseScript[] enemies;
    public Transform player;

    private bool roomCleared = false;
    private bool fightStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !roomCleared)
        {
            StartFight();
        }
    }

    void StartFight()
    {
        fightStarted = true;

        foreach (EnemyBaseScript enemy in enemies)
        {
            enemy.ActivateEnemy(player);
        }
    }

    void Update()
    {
        if (fightStarted && !roomCleared)
        {
            bool allDead = true;

            foreach (EnemyBaseScript enemy in enemies)
            {
                if (!enemy.IsDead())
                {
                    allDead = false;
                }
            }

            if (allDead)
            {
                roomCleared = true;
                Debug.Log("Room Cleared!");
            }
        }
    }

    public void ResetRoom()
    {
        if (roomCleared)
            return;

        fightStarted = false;

        foreach (EnemyBaseScript enemy in enemies)
        {
            enemy.ResetEnemy();
        }
    }
}