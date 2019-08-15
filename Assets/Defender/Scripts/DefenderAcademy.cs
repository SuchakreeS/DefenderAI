using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

namespace Defender
{
    public class DefenderAcademy : Academy
    {
        [SerializeField] private float m_AgentMoveSpeed;
        public float moveSpeed => m_AgentMoveSpeed;
    }
}
