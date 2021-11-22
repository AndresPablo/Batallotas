using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Epic Battles/Faction")]
public class FactionData : ScriptableObject
{
    public new string name;
    public TroopRoster[] Troops;
}
