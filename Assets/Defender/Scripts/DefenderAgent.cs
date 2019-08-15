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

        private DefenderAcademy _DefenderAcademy;
        
        // -------------------------------------------------------------------------------
        // Override Function
        public override void InitializeAgent()
        {
            
        }
        public override void CollectObservations()
        {

        }
        
        public override void AgentAction(float[] vectorAction, string textAction)
        {

        }

        public override void AgentReset()
        {

        }
        // --------------------------------------------------------------------------------
    }
}
