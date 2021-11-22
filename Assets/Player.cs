using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public new string name;
    public int teamNumber;
    public FactionData myFaction;
    public Army Army;
    

    void Start()
    {
        TroopLogic.OnTroopSpawn += Army.RegisterTroop;
        UnitLogic.OnUnitSpawn += Army.RegisterUnit;
        Army.player = this;
    }

}
