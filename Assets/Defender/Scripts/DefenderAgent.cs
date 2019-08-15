using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

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
        private DefenderAcademy _DefenderAcademy;
        private Rigidbody _Rb;
        
        // -------------------------------------------------------------------------------
        // Override Function
        public override void InitializeAgent()
        {
            _DefenderAcademy = FindObjectOfType<DefenderAcademy>();
            _Rb = transform.GetComponent<Rigidbody>();
        }
        public override void CollectObservations()
        {

        }
        
        public override void AgentAction(float[] vectorAction, string textAction)
        {
            Vector3 direction = Vector3.zero;
            int action = Mathf.FloorToInt(vectorAction[0]);
            switch(action)
            {
                case 0:
                    // do notting 
                    break;
                case 1:
                    direction = transform.right * 1;
                    break;
                case 2:
                    direction = transform.right * -1;
                    break;
            }

            _Rb.AddForce(direction * _DefenderAcademy.moveSpeed, ForceMode.VelocityChange);
        }

        public override void AgentReset()
        {

        }

        // --------------------------------------------------------------------------------

        void OnCollisionEnter(Collision c)
        {
            if (c.gameObject.CompareTag("Wall"))
            {
                _Rb.velocity = Vector3.zero;
            }
            Debug.Log("Hit the wall");
        }
    }
}
