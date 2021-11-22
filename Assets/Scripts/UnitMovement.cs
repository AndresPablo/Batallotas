using UnityEngine;
using Pathfinding;

public class UnitMovement : MonoBehaviour
{
    [HideInInspector]public AIPath aiPath;
    public Transform target;

    IAstarAI ai;

    void OnEnable () {
        ai = GetComponent<IAstarAI>();
        if (ai != null) ai.onSearchPath += Update;
        GetComponent<UnitLogic>().OnStateChange += CheckState;
    }

    void OnDisable () {
        if (ai != null) ai.onSearchPath -= Update;
    }

    void CheckState(UNITSTATE state)
    {
        if(state == UNITSTATE.ATTACK || state == UNITSTATE.DEAD || state == UNITSTATE.FLEE)
        {
            aiPath.enabled = false;
            ai.isStopped = true;
        }
        else{
            aiPath.enabled = true;
            ai.isStopped = false;
        }
    }

    void Update () {
        // Flid side based on direction
        CheckFacing();
        if (target != null && ai != null) ai.destination = target.position;
    }

    void CheckFacing()
    {
        if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }else if(aiPath.desiredVelocity.x <=  -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
