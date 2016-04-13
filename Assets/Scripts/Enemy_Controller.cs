using UnityEngine;
using System.Collections;

public class Enemy_Controller : MonoBehaviour
{
    [Header("Enemy Information")]
    public Transform enemy_Transform;
    public NavMeshAgent enemy_NavMeshAgent;

    [Header("Target Information")]
    public Transform player_Transform;

    [Header("Enemy Variables")]
    public float enemy_Health;
    public float enemy_CurrentHealth;

    void Start()
    {
        player_Transform = GameObject.FindGameObjectWithTag("Player").transform;

        enemy_CurrentHealth = enemy_Health;
    }

    void Update()
    {
        enemy_NavMeshAgent.destination = player_Transform.position;
    }

    public void DamageTaken(float damage)
    {
        enemy_CurrentHealth -= damage;

        if (enemy_CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}