using System.Linq;
using UnityEngine;

using Unity.Pool;
public class SpawnPaternBuilder : MonoBehaviour
{
    [SerializeField] private SO_SpawnPatern _patern;
    [SerializeField] private float _threshold;

    [ContextMenu("Create Patern")]
    private void ReadPatern()
    {
        var list = GetComponentsInChildren<SpawnPaternMark>();
        _patern.sequence.Clear();

        foreach (var item in list)
        {
            _patern.sequence.Add(new SpawnInfo()
            {
                poolObjectName = item.name,
                laneIndex = item.line,
                distance = item.transform.position.x
            });
        }

        _patern.sequence = _patern.sequence.OrderBy(x => x.distance).ToList();
        _patern.totalDistance = _patern.sequence.Last().distance + _threshold;
    }
}