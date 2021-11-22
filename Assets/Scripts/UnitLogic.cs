using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitLogic : MonoBehaviour
{

    [HideInInspector]public UnitStats stats;
    [HideInInspector]public UnitMovement movement;
    [HideInInspector]public HealthLogic health;
    public UNITSTATE currentState;
    public UnitMood currentMood;

    public int team;

    [HideInInspector]public Player myPlayer;
    [HideInInspector]public Player enemyPlayer;

    float attackTimer;

    public UnitLogic target;

    public string myStateText {get; private set;}

    #region EVENTOS
    public delegate void UnitEvent(UnitLogic unit);
    public delegate void GenericEvent();
    public delegate void StateEvent(UNITSTATE _state);
    public event StateEvent OnStateChange;
    public event GenericEvent OnTargetFound;
    public event GenericEvent OnRangeOfTarget;
    public static event UnitEvent OnUnitSpawn;
    #endregion EVENTOS


    void Start()
    {
        health = GetComponent<HealthLogic>();
        health.baseStats = stats;
        health.currentHealth = stats.health;

        health.OnUnitDied += OnMyDeath;
        
        foreach(Player p in GameManager.Instance.players)
        {
            if(p.teamNumber == team)
                myPlayer = p;
            else
            {
                enemyPlayer = p;
            }
        }

        if(OnUnitSpawn != null)
            OnUnitSpawn(this);

        InvokeRepeating("ScanForEnemies", Random.Range(.1f,.3f), Random.Range(.1f,.3f));
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        if(target)
        {
            var distance = (transform.position - target.transform.position).magnitude;

            if(distance < stats.attackRange)
            {
                ChangeState(UNITSTATE.ATTACK);
                Attack();
                if(OnRangeOfTarget != null)
                    OnRangeOfTarget();
            }
        }
    }

    void FindClosestTarget()
    {
        UnitLogic[] enemies = enemyPlayer.Army.units.ToArray();
        List<UnitLogic> potentialEnemies = new List<UnitLogic>();

        if(enemies.Length <= 0)
            return;

        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector2 currentPosition = transform.position;
        foreach(UnitLogic u in enemies)
        {
            if(u == null) return;
            Vector2 directionToTarget = (Vector2)u.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = u.transform;
            }
        }

        if(bestTarget == null)
            return;
        target = bestTarget.transform.GetComponent<UnitLogic>();
        bestTarget.GetComponent<HealthLogic>().OnUnitDied += TargetDied;
        if(OnTargetFound != null)
            OnTargetFound();

        movement.target = target.transform;
        ChangeState(UNITSTATE.WALK);
        //CancelInvoke("ScanForEnemies");

        /* RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, stats.sightRange, Vector2.right);

        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector2 currentPosition = transform.position;
        foreach(RaycastHit2D potentialTarget in hits)
        {
            Vector2 directionToTarget = potentialTarget.point - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }*/
        
    }


    void TargetDied()
    {
        target.GetComponent<HealthLogic>().OnUnitDied -= TargetDied;
        movement.target = null;
        target = null;
        InvokeRepeating("ScanForEnemies", Random.Range(.1f,.3f), Random.Range(.1f,.3f));
    }

    void OnMyDeath()
    {
        health.OnUnitDied -= OnMyDeath;
        ChangeState(UNITSTATE.DEAD);
        if(target)
            target.GetComponent<HealthLogic>().OnUnitDied -= TargetDied;
        CancelInvoke();
        StopAllCoroutines();
        myPlayer.Army.RemoveUnit(this);
    }

    void ScanForEnemies(){
        if(target) return; // Puede que haya que cambiarlo porque se hiperfijan en el primero que agarra
        FindClosestTarget();
    }

    public void ChangeState(UNITSTATE state)
    {
        currentState = state;
        myStateText = currentState.ToString();

        if(currentState == UNITSTATE.ATTACK)
        {
            CancelInvoke();
            //movement.Toogle(false);
        }else
        if(currentState == UNITSTATE.WALK || currentState == UNITSTATE.RUN)
        {
            //movement.Toogle(true);
        }

        if(OnStateChange != null)
            OnStateChange(state);
    }

    void Attack()
    {
        if(attackTimer >= stats.attackCycle)
        {
            DamageData dd = new DamageData();
            dd.amount = stats.damage;
            target.health.TakeDamage(dd);
            attackTimer = 0;
        }
    }
}
