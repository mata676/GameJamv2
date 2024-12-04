using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private ItemMono itemInRange;
    private bool canSwing = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ItemMono>(out ItemMono item))
        {
            itemInRange = item;
            Debug.Log($"Item {item.itemData.itemName} in range.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ItemMono>(out ItemMono item))
        {
            if (item == itemInRange)
            {
                itemInRange = null;
                Debug.Log("Item out of range.");
            }
        }
    }

    public void OnInteract()
    {
        if (itemInRange != null)
        {
            Debug.Log($"Attempting to pick up {itemInRange.itemData?.itemName ?? "unknown item"}");

            if (Inventory.Instance != null && Inventory.Instance.EquipItem(itemInRange.itemData))
            {
                Debug.Log($"Picked up and destroying: {itemInRange.gameObject.name}");
                Destroy(itemInRange.gameObject);
                itemInRange = null;
            }

            else
            {
                Debug.Log("Inventory is full or EquipItem failed.");
            }
        }
        else
        {
            Debug.Log("No item in range to interact with.");
        }
    }

    private void ResetSwing()
    {
        canSwing = true;
    }

    public void OnSwing()
    {
        if (Inventory.Instance.itemInHand != null && Inventory.Instance.itemInHand.itemData.isMeleeWeapon)
        {
            Debug.Log("Swinging the stick!");
            int cooldown = 2;
            int swingRange = 5;
            canSwing = false;

            // Znajdź przeciwników w zasięgu
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, swingRange);

            foreach (var hitCollider in hitColliders)
            {
                // Sprawdź, czy obiekt ma tag "Rival"
                if (hitCollider.TryGetComponent<Rival>(out Rival rival))
                {
                    rival.RivalDamage(10);
                }
            }

            // Odczekaj czas odnowienia przed następnym swingiem
            Invoke(nameof(ResetSwing), cooldown);
        }

        if (Inventory.Instance.itemInHand != null && Inventory.Instance.itemInHand.itemData.isThrowable)
        {
                Debug.Log("Throwing item!");

                GameObject thrownItem = Instantiate(Inventory.Instance.itemInHand.itesmData.model);
                ItemMono clonedObject =  thrownItem.AddComponent<ItemMono>();

                clonedObject.itemData = Inventory.Instance.itemInHand.itemData;

                thrownItem.transform.position = Inventory.Instance.itemPlacement.position;
                thrownItem.transform.rotation = Inventory.Instance.itemPlacement.rotation;
                Rigidbody rb = thrownItem.GetComponent<Rigidbody>();
                rb.isKinematic = false; // Fizyczne sterowanie
                rb.AddForce(thrownItem.transform.forward * 15f, ForceMode.VelocityChange);

                Inventory.Instance.DropItem();

        }
    }

}
