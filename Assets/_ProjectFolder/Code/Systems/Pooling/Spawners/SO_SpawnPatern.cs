using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Pool
{
    [Serializable]
    public struct SpawnInfo
    {
        public string poolObjectName;
        public int laneIndex;
        public float distance;
    }

    [CreateAssetMenu(fileName = "SpawnPatern", menuName = "Scriptable Objects/SpawnPatern")]
    public class SO_SpawnPatern : ScriptableObject
    {
        public float totalDistance;
        public List<SpawnInfo> sequence;
    }
}