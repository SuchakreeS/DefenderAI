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
        public enum TeamType
        {
            A,
            B
        }
        [SerializeField] TeamType m_Team;
        [SerializeField] GameObject m_ShieldObject;
        [SerializeField] Transform m_BulletTransform;
        [SerializeField] DefenderArena m_DefenderArena;
        private DefenderAcademy academy;
        private DefenderArena arena;
        private Rigidbody rb;
        private bool isReload;
        private bool isShield;
        private bool isSwitchShield;
        private IDisposable reloadWeaponDisposable;
        private IDisposable switchShieldDisposable;
        
        // -------------------------------------------------------------------------------
        // Override Function
        public override void InitializeAgent()
        {
            academy = FindObjectOfType<DefenderAcademy>();
            arena = m_DefenderArena;
            rb = transform.GetComponent<Rigidbody>();
            isReload = false;
            isShield = m_ShieldObject.gameObject.activeSelf;
            isSwitchShield = false;
        }
        public override void CollectObservations()
        {

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
            int battleAction = Mathf.FloorToInt(act[1]);

            // Move action
            if(moveAction == 1)
                direction = transform.right * 1;
            else if (moveAction == 2)
                direction = transform.right * -1;
            
            // Battle action
            if(battleAction == 1 && !isSwitchShield)
            {
                isSwitchShield = true;
                isShield = !isShield;
                m_ShieldObject.gameObject.SetActive(isShield);
                
                switchShieldDisposable?.Dispose();
                switchShieldDisposable = Observable.Timer(TimeSpan.FromSeconds(0.2)).Subscribe(_ => isSwitchShield = false);
            }
            else if (battleAction == 2 && !isReload && !isShield)
            {
                isReload = true;
                var bulletRb = m_BulletTransform.gameObject.GetComponent<Rigidbody>();
                bulletRb.AddForce(m_BulletTransform.forward * 500f);
                reloadWeaponDisposable?.Dispose();
                reloadWeaponDisposable = Observable.Timer(TimeSpan.FromSeconds(2)).Subscribe(_ => isReload = false);
            }

            
            // Final action
            rb.AddForce(direction * academy.moveSpeed, ForceMode.VelocityChange);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, academy.maxMoveSpeed);
        }

        public override void AgentReset()
        {

        }

        // --------------------------------------------------------------------------------

        private void OnCollisionEnter(Collision c)
        {
            if (c.gameObject.CompareTag("Wall"))
            {
                rb.velocity = Vector3.zero;
            }
            Debug.Log("Hit the wall");
        }
        private UniRx.IObservable<Unit> OnSwitchShield()
        {
            return Observable.Create<Unit>
            (
                _observer => 
                {
                    IDisposable dis = null;
                    _observer.OnNext(Unit.Default);
                    return Disposable.Create(() => dis.Dispose());
                }
            );
        }
    }
}
