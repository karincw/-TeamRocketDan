using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace Leo.Core
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
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
        
        public void ShakeCamera(float force, float time)
        {
            StartCoroutine(ShakeCameraCoroutine(force, time));
        }

        private IEnumerator ShakeCameraCoroutine(float force, float time)
        {
            float elapsedTime = 0;
            while (elapsedTime < time)
            {
                GenerateImpulse(force);
                elapsedTime += Time.deltaTime;
                Debug.Log(elapsedTime + " / " + time);
                yield return null;
            }
        }

        private void GenerateImpulse(float force)
        {
            _impulseSource.GenerateImpulse(force);
        }
    }
}