using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationBake : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;

    private void Awake()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
    }
    public void BakeNavMesh()
    {
        Debug.Log("Bake");
        navMeshSurface.BuildNavMesh();

    }
}
