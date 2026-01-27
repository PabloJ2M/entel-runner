namespace Unity.Pool
{
    public class SpawnerPointByPattern : PoolManagerObjects
    {
        private float _seedMultiply = 1f;
        public override float SpeedMultiply => _seedMultiply;

        public void SetSpeed(float value) => _seedMultiply = value;
        public void OnSpawn(string item, float worldDistance)
        {
            var obj = GetPrefab(item) as PoolObjectOnSpline;
            obj.SetDistance(worldDistance * SpeedMultiply + WorldOffset);
        }
    }
}