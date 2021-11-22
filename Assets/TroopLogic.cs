using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Goodgulf.Formation;
using Goodgulf.Utilities;

public class TroopLogic : MonoBehaviour
{
    public UnitLogic leader;
    public List<UnitLogic> units = new List<UnitLogic>();
    List<GameObject> followers = new List<GameObject>();
    public int teamNumber;
    [SerializeField] Vector2 centerPoint;
    Formation formation;

    #region EVENTOS
    public delegate void TroopDelegate(TroopLogic troop);
    public static event TroopDelegate OnTroopSpawn;
    #endregion EVENTOS


    void Start()
    {
        if(OnTroopSpawn != null)
            OnTroopSpawn(this);
        // Agrega todas las unidades a la formacion
        formation = GetComponent<Formation>();

        foreach(UnitLogic u in units)
        {
            followers.Add(u.gameObject);
            formation.AddFormationObject(u.gameObject);
            u.gameObject.GetComponent<FormationFollower>().SetFormation(this.formation);
        }
        formation.CreateFormation(formation, 1, 2, followers , true);
    }

    void FillRanks(UnitLogic[] _units)
    {
        foreach(UnitLogic u in _units)
        {
            u.transform.SetParent(transform);
            units.Add(u);
        }
    }

    public void AddUnit(UnitLogic _unit)
    {
        _unit.transform.SetParent(transform);
        _unit.team = this.teamNumber;
        units.Add(_unit);
        
    }


    void Update()
    {
        
    }
}
