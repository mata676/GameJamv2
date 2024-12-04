using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkPart : MonoBehaviour
{
    public int number;
    public GameObject emptyCup;
    public void OnMouseDown()
    { 
        if (!DrinkManager.Instance.ChooseDrink(number)) return;
        GameObject go = Instantiate(emptyCup);
        go.transform.position = transform.position;
        Destroy(gameObject);
    }
}
