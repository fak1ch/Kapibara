using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

public class RealtimeNavMeshBaker : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _surface;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _navMeshSize = new Vector3(200, 200, 200);

    private NavMeshData _navMeshData;
    private List<NavMeshBuildSource> _sources = new List<NavMeshBuildSource>();

    public void BuildNavMesh(bool Async)
    {
        _navMeshData = new NavMeshData();
        NavMesh.AddNavMeshData(_navMeshData);

        Bounds navMeshBounds = new Bounds(_target.transform.position, _navMeshSize);
        List<NavMeshBuildMarkup> markups = new List<NavMeshBuildMarkup>();

        List<NavMeshModifier> modifiers;
        if (_surface.collectObjects == CollectObjects.Children)
        {
            modifiers = new List<NavMeshModifier>(GetComponentsInChildren<NavMeshModifier>());
        }
        else
        {
            modifiers = NavMeshModifier.activeModifiers;
        }

        for (int i = 0; i < modifiers.Count; i++)
        {
            if (((_surface.layerMask & (1 << modifiers[i].gameObject.layer)) == 1)
                && modifiers[i].AffectsAgentType(_surface.agentTypeID))
            {
                markups.Add(new NavMeshBuildMarkup()
                {
                    root = modifiers[i].transform,
                    overrideArea = modifiers[i].overrideArea,
                    area = modifiers[i].area,
                    ignoreFromBuild = modifiers[i].ignoreFromBuild
                });
            }
        }

        if (_surface.collectObjects == CollectObjects.Children)
        {
            NavMeshBuilder.CollectSources(transform, _surface.layerMask, _surface.useGeometry, _surface.defaultArea, markups, _sources);
        }
        else
        {
            NavMeshBuilder.CollectSources(navMeshBounds, _surface.layerMask, _surface.useGeometry, _surface.defaultArea, markups, _sources);
        }

        _sources.RemoveAll(source => source.component != null && source.component.gameObject.GetComponent<NavMeshAgent>() != null);

        if (Async)
        {
            NavMeshBuilder.UpdateNavMeshDataAsync(_navMeshData, _surface.GetBuildSettings(), _sources, new Bounds(_target.transform.position, _navMeshSize));
        }
        else
        {
            NavMeshBuilder.UpdateNavMeshData(_navMeshData, _surface.GetBuildSettings(), _sources, new Bounds(_target.transform.position, _navMeshSize));
        }
    }
}
