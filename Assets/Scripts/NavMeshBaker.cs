using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting;

public class NavMeshBaker : MonoBehaviour
{
    NavMeshSurface nav;

    void Awake(){
        nav = gameObject.GetComponent<NavMeshSurface>();
        nav.BuildNavMesh();
    }
}
