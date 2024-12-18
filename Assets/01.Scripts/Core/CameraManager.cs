using Unity.Cinemachine;

public class CameraManager : MonoSingleton<CameraManager>
{
    private CinemachineImpulseSource _impulseSource;

    protected override void Awake()
    {
        base.Awake();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }
    
    public void ShakeCamera(float force)
    {
        GenerateImpulse(force);
    }
    
    private void GenerateImpulse(float force)
    {
        _impulseSource.GenerateImpulse(force);
    }
}
