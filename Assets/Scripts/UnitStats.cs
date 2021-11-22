using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Epic Battles/Stats")]
public class UnitStats : ScriptableObject
{
    public float attackCycle = 1f;
    public float attackRange = 1;
    public int damage = 1;
    public int armor = 1;
    public int health = 2;
    public float sightRange = 2f;
    [Space]
    public int size = 1;
    
}