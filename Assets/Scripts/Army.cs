using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Army
{
    public string name;
    public List<TroopLogic> troops = new List<TroopLogic>();
    public List<UnitLogic> units = new List<UnitLogic>();

    public List<int> foes = new List<int>();
    public List<int> allies = new List<int>();
    public Player player;


    void Start()
    {

    }

    public void RemoveUnit(UnitLogic unit){
        units.Remove(unit);
    }

    public void RegisterUnit(UnitLogic unit)
    {
        foreach(int i in allies)
        {
            if(unit.team == i)
                units.Add(unit);
        }
    }

    public void RegisterTroop(TroopLogic troop)
    {
        foreach(int i in allies)
        {
            if(troop.teamNumber == i)
                troops.Add(troop);
        }
    }
}

[System.Serializable]
public struct Troops {

}

[System.Serializable]
public struct TroopRoster {
    public string name;
    public int cost;
    public GameObject prefab;
}
