using UnityEngine;

namespace Util.ScriptableObjects.Audio
{
    public abstract class AudioEvent : ScriptableObject
    {
        public abstract void Play(AudioSource source);
    }
}