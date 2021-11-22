using UnityEngine;

public class UI_LaunchButton : MonoBehaviour
{
    public void OnClick()
    {
        GameManager.Instance.LaunchBattle();
        gameObject.SetActive(false);
    }
}
