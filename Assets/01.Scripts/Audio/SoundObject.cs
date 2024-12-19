using UnityEngine;

namespace Leo.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundObject : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void Play()
        {
            _audioSource.Play();
        }
        
        public void Stop()
        {
            _audioSource.Stop();
        }
    }
    
}