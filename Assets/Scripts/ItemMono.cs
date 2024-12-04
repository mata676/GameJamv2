using UnityEngine;

public class ItemMono : MonoBehaviour
{
    public ItemData itemData;

    private void OnCollisionEnter(Collision collision)
    {
        if(itemData.isThrowable && itemData.isThrown)
        {
            Debug.Log($"Thrown item collided with: {collision.gameObject.name}");

            // Sprawdź, czy trafiony obiekt ma tag "Enemy"
            if (collision.gameObject.CompareTag("Rival"))
            {
                // Jeśli obiekt ma komponent EnemyHealth, zadaj obrażenia
                if (collision.gameObject.TryGetComponent<Rival>(out Rival enemy))
                {
                    enemy.RivalDamage(itemData.damage);
                    Debug.Log($"Enemy hit! Dealt {itemData.damage} damage.");
                }
            }
            
            // Zniszcz rzucony przedmiot po kolizji
            itemData.isThrown = false;
        }
    }
}
