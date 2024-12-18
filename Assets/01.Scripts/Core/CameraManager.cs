using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace Leo.Core
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class CameraManager : MonoSingleton<CameraManager>
    {
        private CinemachineImpulseSource _impulseSource;
        private bool _isShaking = false;

        protected override void Awake()
        {
            base.Awake();
            _impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        public void ShakeCamera(float force)
        {
            GenerateImpulse(force);
        }
        
        public void ShakeCamera(float force, float time)
        {
            StartCoroutine(ShakeCameraCoroutine(force, time));
        }

        private IEnumerator ShakeCameraCoroutine(float force, float time)
        {
            if (_isShaking) yield break;
            _isShaking = true;
            float elapsedTime = 0;
            while (elapsedTime < time)
            {
                GenerateImpulse(force);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _isShaking = false;
        }

        private void GenerateImpulse(float force)
        {
            _impulseSource.GenerateImpulse(force);
        }
    }
}