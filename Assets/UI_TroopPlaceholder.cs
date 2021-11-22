using UnityEngine.UI;
using UnityEngine;

public class UI_TroopPlaceholder : MonoBehaviour
{
    [SerializeField]Text nameText;
    [SerializeField]Text amountText;
    SpriteRenderer areaSprite;

    void Start()
    {
        
    }

    public void Setup(string name, int amountOfUnits)
    {
        nameText.text = name;
        amountText.text = "("+amountOfUnits+")";
    }


}
