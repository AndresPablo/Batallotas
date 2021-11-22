using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLogic : MonoBehaviour
{
    public UnitStats baseStats;
    public int currentHealth;

    public delegate void OnHealthEvent();
    public delegate void GenericHealthEvent();
    public static OnHealthEvent OnUnitDeath;
    public GenericHealthEvent OnUnitDied;


    public void TakeDamage(DamageData dd)
    {
        currentHealth -= dd.amount;
        StartCoroutine("Flash",GetComponentInChildren<SpriteRenderer>().color);

        if(currentHealth <= 0)
        {
            Die();
            //SFX
            // ...
            //GFX
        }
    }

    void Die()
    {
        StopAllCoroutines();
        if(OnUnitDied != null)
        {
            OnUnitDied();
        }
        Destroy(gameObject);
    }

    IEnumerator Flash(Color defaultColor)
    {
        var renderer = GetComponentInChildren<SpriteRenderer>();
        for (int i = 0; i < 2; i++)
        {
            renderer.color = Color.red;
            yield return new WaitForSeconds(.05f);
            renderer.color = defaultColor;
            yield return new WaitForSeconds(.05f);
        }
    }
}
