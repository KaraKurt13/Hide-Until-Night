using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DayTimeState.eveningHasBegun += GenerateEveningEnemies;
        EveningTimeState.nightHasBegun += GenerateNightEnemies;
    }

    private void GenerateEveningEnemies()
    {

    }

    private void GenerateNightEnemies()
    {

    }

}
