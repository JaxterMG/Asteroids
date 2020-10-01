using UnityEngine;

namespace Audio
{
    [System.Serializable]
    public class ClipChain
    {
        public ClipType Type;
        public AudioClip[] Clips;

        public AudioClip GetRandomClip()
        {
            int randomIndex = Random.Range(0, Clips.Length);

            return Clips[randomIndex];
        }
    }
}