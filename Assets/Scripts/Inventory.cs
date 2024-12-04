using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    public ItemInstance itemInHand;
    public GameObject itemInHandObject;
    [SerializeField]
    public Transform itemPlacement;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        itemInHand = null;
        itemPlacement.localPosition = new Vector3(0, -0.5f, 1); // Dostosuj pozycję
        itemPlacement.localRotation = Quaternion.Euler(0, 0, 0); // Dostosuj rotację
    }

   public bool EquipItem(ItemData itemData)
    {
        if (itemInHand == null)
        {
            Debug.Log($"Equipping item: {itemData.itemName}");
            itemInHand = new ItemInstance(itemData);
            Debug.Log($"itemInHand now contains: {itemInHand.itemData.itemName}");
            itemInHandObject = Instantiate(itemInHand.itemData.model);
            itemInHandObject.transform.parent = Camera.main.transform;
            itemInHandObject.transform.localPosition = itemPlacement.localPosition;
            itemInHandObject.transform.localRotation = Quaternion.identity;
            itemInHand.itemData.isThrown = true;
            return true;
        }

        else
        {
            Debug.Log("Cannot equip item: hand is full!");
            return false;
        }
    }

    public void DropItem()
    {
        if (itemInHand != null)
        {
            Destroy(itemInHandObject);

            GameObject item = Instantiate(itemInHand.itemData.model);
            item.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
            item.AddComponent<Rigidbody>();

            itemInHand = null;
        }
    }
}

