using System.Collections.Generic;
using UnityEngine;

public class DrinkManager : MonoBehaviour
{
    // Static instance to access the Singleton globally
    private static DrinkManager _instance;
    // Public property to access the instance
    public static DrinkManager Instance
    {
        get
        {
            // If the instance is null, try to find an existing instance in the scene
            if (_instance == null)
            {
                _instance = FindObjectOfType<DrinkManager>();

            }
            return _instance;
        }
    }

    public List<int> winningNumbers;
    public List<int> chosenNumbers;


    private void Awake()
    {
        // If an instance already exists, destroy this one
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scene loads
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate if any
        }
    }

    public void GenerateNubers()
    {
        System.Random random = new();
        winningNumbers = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            winningNumbers.Add(random.Next(1, 7)); // Generates numbers from 1 to 6 inclusive
        }
    }

    public bool ChooseDrink(int num)
    {
        if(chosenNumbers.Count == 3) { AllChosen(); return false; }
        chosenNumbers.Add(num);
        return true;
    }

    private void AllChosen()
    {
        //choose random mechanic
    }
}
