namespace Unity.Pool
{
    public class SpawnerPoint : PoolManagerObjectsByDistance
    {
        protected override void OnSpawn()
        {
            var obj = GetPrefabRandom() as PoolObjectOnSpline;
            obj.SetDistance(_gameManager.WorldDistance * SpeedMultiply + WorldOffset);
        }
    }
}