using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Defender
{
    public class DefenderArena : MonoBehaviour
    {
        [SerializeField]
        public bool isPlaying;
        public int scoreA;
        public int scoreB;
        public float time;
        private void Start()
        {
            isPlaying = true;

            Observable.EveryUpdate().Where(_ => isPlaying).Subscribe
            (
                _ => time += Time.deltaTime
            ).AddTo(this);
        }


        public void ResetArena()
        {
            isPlaying = false;
            scoreA = 0;
            scoreB = 0;
            time = 0f;
        }
    }
}
