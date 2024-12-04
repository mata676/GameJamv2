using UnityEngine;
using UnityEngine.AI;

public class Rival : MonoBehaviour
{
    public GameObject player;
    public int hp;

    private NavMeshAgent navAgent;
    private float minSpeed = 5f; 
    private float maxSpeed = 7f;
    private bool notStunned = true;
    private float duration = 1f;
    private float timer = 0f;
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        if (navAgent == null)
        {
            Debug.LogError("Rival: NavMeshAgent component missing!");
            return;
        }

        navAgent.speed = Random.Range(minSpeed, maxSpeed); // Set random speed within bounds (consider adjusting based on NavMesh design)
    }

    void Update()
    {
        Debug.Log(notStunned);
        if (player != null && notStunned)
        {
            navAgent.SetDestination(player.transform.position);
        }


        if(notStunned == false)
        {
            timer += Time.deltaTime;
            if(timer >= duration)
            {
                notStunned = true;
                timer = 0f;

            }
        }

        Debug.Log($"{hp}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Rival HP : {hp}");
        if (collision.gameObject.tag == "Player" && hp > 0 && notStunned)
        {
            notStunned = false;
        }
        else if (hp <= 0)
        {
            Debug.Log("YOU WON! Rival died");
            Destroy(gameObject);
        }
    }

    public void RivalDamage(int points)
    {
        hp -= points;
    }
}