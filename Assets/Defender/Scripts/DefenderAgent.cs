using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using UniRx;
using System;

namespace Defender
{
    public class DefenderAgent : Agent
    {
        
        [SerializeField] TeamType m_Team;
        [SerializeField] GameObject m_ShieldObject;
        [SerializeField] DefenderArena m_DefenderArena;
        private DefenderAcademy academy;
        private DefenderArena arena;
        private Rigidbody rb;
        private bool isReload;
        private WeaponController weapon;
        private ShieldAnimation shield;
        private IDisposable switchShieldDisposable;
        private RayPerception rayPer;
        private TeamType randomTeam;
        private AttackAction previousAtt;
        private AttackAction currentAtt;
    
        private float[] rayAngles = { 0f, 45f, 90f, 135f, 180f, 110f, 70f };
        private string[] detectableObjectsA = { "AgentB", "GoalB", "BulletB", "Wall"};
        private string[] detectableObjectsB = { "AgentA", "GoalA", "BulletA", "Wall"};
        
        // -------------------------------------------------------------------------------
        // Override Function
        public override void InitializeAgent()
        {
            academy = FindObjectOfType<DefenderAcademy>();
            arena = m_DefenderArena;
            rb = transform.GetComponent<Rigidbody>();
            rayPer = GetComponent<RayPerception3D>();
            isReload = false;
            weapon = gameObject.GetComponent<WeaponController>();
            weapon.Init();
            shield = m_ShieldObject.GetComponent<ShieldAnimation>();
            shield.Init();
        }
        public override void CollectObservations()
        {
            float rayDistance = 10f;
            string[] detectableObjects = {};
            if (m_Team == TeamType.A)
            {
                detectableObjects = detectableObjectsA;
            }
            else if (m_Team == TeamType.B)
            {
                detectableObjects = detectableObjectsB;
            }
            AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
        }
        
        public override void AgentAction(float[] vectorAction, string textAction)
        {
            if(arena.isPlaying)
            {
                ActionAgent(vectorAction);
            }
        }
        private void ActionAgent(float[] act)
        {
            Vector3 direction = Vector3.zero;
            int moveAction = Mathf.FloorToInt(act[0]);
            currentAtt = (AttackAction) Mathf.FloorToInt(act[1]);

            // Move action
            if(moveAction == 1)
                direction = transform.right * 1;
            else if (moveAction == 2)
                direction = transform.right * -1;
            
            // Battle action
            if(currentAtt == AttackAction.Shield)
            {
                shield.SwitchShield();
            }
            else if (currentAtt == AttackAction.Fire && !shield.IsOpen)
            {
                weapon.Fire();
            }
            // Final action
            rb.AddForce(direction * academy.moveSpeed, ForceMode.VelocityChange);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, academy.maxMoveSpeed);

            previousAtt = currentAtt;
        }
        public override void AgentReset()
        {
            weapon.Refresh();
        }

        // --------------------------------------------------------------------------------
        // Public Function
        public TeamType GetTeam() => m_Team;
        public DefenderArena GetArena() => arena;
        
        // --------------------------------------------------------------------------------
        // Private Function
        private void OnCollisionEnter(Collision c)
        {
            if (c.gameObject.CompareTag("Wall"))
            {
                rb.velocity = Vector3.zero;
            }
        }
    }
}
