using UnityEngine.UI;
using UnityEngine;


public class UI_FactionRoster : MonoBehaviour
{
    public FactionData factionData;
    public GameObject buttonPrefab;


    void GenerateButtons()
    {
        GameObject go = Instantiate(buttonPrefab, transform);
    }
}
