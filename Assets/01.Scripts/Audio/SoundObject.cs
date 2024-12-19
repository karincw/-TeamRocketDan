using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Leo.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundObject : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;
        [FormerlySerializedAs("_isPlayObAwake")] [FormerlySerializedAs("_isBgm")] [SerializeField] private bool _isPlayOnAwake;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = _audioClip;
        }

        private void Start()
        {
            if (_isPlayOnAwake)
            {
                Play();
            }
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