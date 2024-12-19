using UnityEngine;
using UnityEngine.Audio;

namespace Karin.DialogSystem.Tree
{
    public class PlaySoundNode : ActionNode
    {
        public AudioClip soundClip;
        public AudioMixerGroup mixerGroup;

        protected override void OnStart()
        {
            blackBoard.audioSource.outputAudioMixerGroup = mixerGroup;
            blackBoard.audioSource.PlayOneShot(soundClip);
        }

        protected override NodeState OnUpdate()
        {
            return child.Update();
        }

        protected override void OnStop()
        {
        }
    }
}
