using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DayTimeState.eveningHasBegun += GenerateEveningEnemies;
    }

    private void GenerateEveningEnemies()
    {

    }

}
