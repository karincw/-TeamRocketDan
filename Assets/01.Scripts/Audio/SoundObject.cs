using System;
using UnityEngine;

namespace Leo.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundObject : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private bool _isBgm;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = _audioClip;
        }

        private void Start()
        {
            if (_isBgm)
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