using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Goodgulf.Formation;


public class DeployTile : MonoBehaviour
{
    public int teamNumber;
    public GameObject troopPrefab;
    public GameObject formationPrefab;
    public GameObject liderPrefab;
    public GameObject unitPrefab;
    public FormationTypes formacion;
    [Range(1,100)]public int amountOfUnits = 10;
    [Range(1f,20f)] public float area = 5f;
    SpriteRenderer areaSprite;
    [SerializeField]UI_TroopPlaceholder visual;
    
    
    void Start()
    {
        areaSprite = GetComponent<SpriteRenderer>();
        GameManager.OnGameStart += Spawn;
        Resize();
        visual.Setup(unitPrefab.name, amountOfUnits);
    }

    void Spawn()
    {
        // Crea la tropa y toma el script
        TroopLogic troop = Instantiate(troopPrefab.GetComponent<TroopLogic>());
        troop.teamNumber = this.teamNumber;

        for (int i = 0; i < amountOfUnits; i++)
        {
            GameObject unitGo = Instantiate(unitPrefab, GetRandomPointInCircle(), Quaternion.identity);
            troop.AddUnit(unitGo.GetComponent<UnitLogic>());
            unitGo.GetComponent<UnitLogic>().team = teamNumber;
        }

        Destroy(this.gameObject);
    }

    Vector2 GetRandomPointInCircle()
    {
        Vector2 point = (Vector2)gameObject.transform.position + (Random.insideUnitCircle * area/2);
        return point;
    }

    
    void Resize()
    {
        transform.localScale = new Vector3(1,1,1) * area;
    }
}
