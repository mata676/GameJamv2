using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject model;
    [TextArea]
    public string description;
    public bool isThrowable; // Czy przedmiot można rzucić
    public bool isMeleeWeapon; // Czy przedmiot może być użyty jako broń do walki wręcz
    public  int damage = 10; // Obrażenia w walce wręcz
    public bool isThrown = false;
}