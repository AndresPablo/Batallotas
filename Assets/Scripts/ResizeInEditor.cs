using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ResizeInEditor : MonoBehaviour
{
    DeployTile tile;

    void Start()
    {
        tile=GetComponent<DeployTile>();        
    }


    void Update()
    {
        transform.localScale = new Vector3(1,1,1) * tile.area;
    }
}
