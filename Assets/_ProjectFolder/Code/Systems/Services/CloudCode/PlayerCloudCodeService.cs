using System.Threading.Tasks;

namespace Unity.Services.CloudCode
{
    public class PlayerCloudCodeService : UnityServiceBehaviour
    {
        protected override string _localDataID => "";

        public ServerTimeUTC ServerTimeUTC { get; private set; }

        protected override void OnSignInCompleted() { }
        protected override void OnSignOutCompleted() { }

        public async Task GetServerTime() => ServerTimeUTC = await CloudCodeService.Instance.CallEndpointAsync<ServerTimeUTC>("get-time");
        public void ClearServerTime() => ServerTimeUTC = new();
    }
}