namespace Unity.Cinemachine
{
    public class CinemachineEvents : CinemachineCameraEvents
    {
        private CinemachineBrain _brain;

        private void Start() => _brain = CinemachineBrain.GetActiveBrain(0);

        public void ActiveCamera()
        {
            CinemachineCamera lastCam = _brain.ActiveVirtualCamera as CinemachineCamera;
            lastCam.Priority = 0;
            EventTarget.Priority = 100;
        }
    }
}