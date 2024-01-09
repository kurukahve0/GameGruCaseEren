using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Case_1;
using UnityEngine;

namespace Case_2
{


    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private AudioSource audioSource;
        private float pitchCounter=.8f; 



        public void PlaySound(SoundType soundType,bool isPercent)
        {
            if (isPercent)
            {
                pitchCounter += .2f;
            }
            else
            {
                pitchCounter = .8f;
            }

            audioSource.pitch = pitchCounter;
            var clip = GameManager.Instance.GameData.SoundData.First(x => x.SoundType == soundType).AudioClip;
            audioSource.PlayOneShot(clip);
            
        }


    }
}
