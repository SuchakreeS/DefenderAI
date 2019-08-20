using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;
using System;

namespace Defender
{
    public class DefenderArena : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_ScoreAText;
        [SerializeField] TextMeshProUGUI m_ScoreBText;
        [SerializeField] DefenderAgent AgentA;
        [SerializeField] DefenderAgent AgentB;
        public bool isPlaying;
        public TeamType winTeam;
        public IntReactiveProperty scoreA = new IntReactiveProperty();
        public IntReactiveProperty scoreB = new IntReactiveProperty();
        public float time;
        public DefenderAcademy academy;

        private int MaxScore;
        private TransformKeeping AgentATransform;
        private TransformKeeping AgentBTransform;
        private TeamType randomTeam;

        public struct TransformKeeping
        {
            private Vector3 position;
            private Quaternion rotation;
            private Vector3 localScale;
            public TransformKeeping(Transform _transform)
            {
                position = _transform.position;
                rotation = _transform.rotation;
                localScale = _transform.localScale;
            }
            public void Transfer(Transform _transform)
            {
                _transform.position = position;
                _transform.rotation = rotation;
                _transform.localScale = localScale;
            }

        }
        
        // ---------------------------------------------------------------------------------------------------
        // Unity Function
        private void Start()
        {
            academy = FindObjectOfType<DefenderAcademy>();
            MaxScore = (int) academy.resetParameters["MaxScore"];
            AgentATransform = new TransformKeeping(AgentA.transform);
            AgentBTransform = new TransformKeeping(AgentB.transform);
            randomTeam = randomTeam.RandomTeam();
            // Score Notification
            scoreA.Subscribe
            (
                _score => 
                {
                    m_ScoreAText.text = _score.ToString();
                    if(isPlaying)   CheckScore();
                }
            ).AddTo(this);
            scoreB.Subscribe
            (
                _score => 
                {
                    m_ScoreBText.text = _score.ToString();
                    if(isPlaying)   CheckScore();
                }
            ).AddTo(this);

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
            // Arena
            scoreA.SetValueAndForceNotify(0);
            scoreB.SetValueAndForceNotify(0);
            AgentATransform.Transfer(AgentA.transform);
            AgentBTransform.Transfer(AgentB.transform);
            MaxScore = (int) academy.resetParameters["MaxScore"];
            randomTeam = randomTeam.RandomTeam();
            time = 0f;
            winTeam = TeamType.None;

            // Agent
            AgentA.Done();
            AgentB.Done();

            // Restart Game
            isPlaying = true;
        }

        public void AddScore(TeamType team)
        {
            if (!isPlaying) return;

            if(team == TeamType.A)
            {
                scoreA.SetValueAndForceNotify(scoreA.Value + 1);
            }
            else if(team == TeamType.B)
            {
                scoreB.SetValueAndForceNotify(scoreB.Value + 1);
            }
            SetReward(team, 0.1f);
        }
        // ---------------------------------------------------------------------------------------------------
        // Private function
        private void CheckScore()
        {
            if(scoreA.Value >= MaxScore)
            {
                isPlaying = false;
                winTeam = TeamType.A;
                SetReward(winTeam, 1);
                ResetArena();
            }
            else if(scoreB.Value >= MaxScore)
            {
                isPlaying = false;
                winTeam = TeamType.B;
                SetReward(winTeam, 1);
                ResetArena();
            }
        }
        private void SetTramsform(Transform _src,Transform _des)
        {
            _des.transform.position = _src.transform.position;
            _des.transform.rotation = _src.transform.rotation;
            _des.transform.localScale = _src.transform.localScale;
        }
        private void SetReward(TeamType _team, float _reward)
        {
            if(randomTeam == _team && randomTeam == TeamType.A)
            {
                AgentA.SetReward(_reward);
            }
            else if(randomTeam == _team && randomTeam == TeamType.B)
            {
                AgentB.SetReward(_reward);
            }
            else if(randomTeam != _team && randomTeam == TeamType.A)
            {
                AgentA.SetReward(-_reward);
            }
            else if(randomTeam != _team && randomTeam == TeamType.B)
            {
                AgentB.SetReward(-_reward);
            }
        }
    }
}
