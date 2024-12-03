using UnityEngine;

public class Rival : MonoBehaviour
{
    public GameObject player;
    public int hp;
    private float minSpeed = 2f;
    private float maxSpeed = 5f;


    void Update()
    {
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        Vector3 newPos = transform.position + direction * randomSpeed * Time.deltaTime;
        transform.position = newPos;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && hp > 0)
        {
            Vector3 pushback = transform.position - player.transform.position;
            pushback.Normalize();
            Rigidbody rivalRb = GetComponent<Rigidbody>();
            rivalRb.AddForce(pushback * 15f, ForceMode.Impulse);
            Debug.Log("PIERDOLNALEM!");
        }
        else if(hp < 0)
        {
            minSpeed = 0;
            maxSpeed = 0;
            Debug.Log("YOU DIED!");
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
