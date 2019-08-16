using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

namespace Defender
{
    public class DefenderAcademy : Academy
    {
        [SerializeField] private float m_MoveSpeed;
        [SerializeField] private float m_MaxMoveSpeed;
        [SerializeField] private GameObject m_BulletObject;
        public float moveSpeed => m_MoveSpeed;
        public float maxMoveSpeed => m_MaxMoveSpeed;
        public GameObject bulletObject => m_BulletObject;
    }
}
