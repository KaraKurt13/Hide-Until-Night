using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class  EnemyAIState : MonoBehaviour
{
    public abstract EnemyAIState EnemyAction();
    public AIDestinationSetter destination;
}
