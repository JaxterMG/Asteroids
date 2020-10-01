using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioSourceHelper : MonoBehaviour
    {

        [SerializeField] private AudioSource _engineSource;
        [SerializeField] AudioSource _explosionSource;
        [SerializeField] AudioSource _shotSource;
        [SerializeField] private ClipChain[] _clips;
        private static AudioSourceHelper _instance;
        public static AudioSourceHelper Instance => _instance;

        private void Awake()
        {
            _instance = this;
        }
        public bool IsPlaying()
        {
            return _engineSource.isPlaying;
        }
        public void StopClip()
        {
            _engineSource.Stop();
        }
        public void StartClip()
        {
            _engineSource.Play();
        }
        public void PlayOneShot(ClipType type)
        {
            if (_clips != null)
            {

                foreach (var clip in _clips)
                {
                    if (clip.Type == type)
                    {
                        if (clip.Type == ClipType.Explosion)
                        {
                            _explosionSource.Stop();
                            _explosionSource.PlayOneShot(clip.GetRandomClip());
                        }
                        else if (clip.Type == ClipType.Shot)
                        {
                            _shotSource.Stop();
                            _shotSource.PlayOneShot(clip.GetRandomClip());
                        }

                    }
                  
                }
            }


        }

    }
}
