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
        public IntReactiveProperty scoreA = new IntReactiveProperty();
        public IntReactiveProperty scoreB = new IntReactiveProperty();
        public float time;
        public DefenderAcademy academy;

        private int MaxScore;
        
        // ---------------------------------------------------------------------------------------------------
        // Unity Function
        private void Start()
        {
            academy = FindObjectOfType<DefenderAcademy>();
            MaxScore = (int) academy.resetParameters["MaxScore"];

            // Score Notification
            scoreA.Subscribe(_ => CheckScore()).AddTo(this);
            scoreB.Subscribe(_ => CheckScore()).AddTo(this);

            // Add time when Playing
            Observable.EveryUpdate().Where(_ => isPlaying).Subscribe
            (
                _ => time += Time.deltaTime
            ).AddTo(this);

            // Game has Begin
            isPlaying = true;
        }
        
        // ---------------------------------------------------------------------------------------------------
        // Public Function
        public void ResetArena()
        {
            isPlaying = false;
            scoreA.SetValueAndForceNotify(0);
            scoreB.SetValueAndForceNotify(0);
            MaxScore = (int) academy.resetParameters["MaxScore"];
            time = 0f;
        }

        public void AddScore(TeamType team)
        {
            if(team == TeamType.A)
            {
                scoreA.SetValueAndForceNotify(scoreA.Value + 1);
            }
            else if(team == TeamType.B)
            {
                scoreB.SetValueAndForceNotify(scoreB.Value + 1);
            }
        }
        // ---------------------------------------------------------------------------------------------------
        // Private function
        private void CheckScore()
        {
            if(scoreA.Value >= MaxScore)
            {
                Debug.Log("Team A Win !!!!");
                isPlaying = false;
            }
            else if(scoreB.Value >= MaxScore)
            {
                Debug.Log("Team B Win !!!!");
                isPlaying = false;
            }
        }
    }
}
