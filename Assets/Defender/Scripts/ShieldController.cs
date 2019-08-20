using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defender
{
    public class ShieldController : MonoBehaviour
    {
        private DefenderArena arena;
        private TeamType team;

        public void Init(DefenderAgent _agent)
        {
            team = _agent.GetTeam();
            arena = _agent.GetArena();
        }

        public void OnCollisionEnter(Collision col)
        {
            if(col.gameObject.CompareTag("Goal"))
            {
                arena.AddScore(team);
                Destroy(gameObject);
            }
        }
    }
}
