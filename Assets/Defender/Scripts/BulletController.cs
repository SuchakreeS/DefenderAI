using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defender;

namespace Defender
{
    public class BulletController : MonoBehaviour
    {
        private DefenderArena arena;
        private TeamType team;

        public void Init(DefenderAgent _agent)
        {
            team = _agent.GetTeam();
            arena = _agent.GetArena();
        }

        public void OnTriggerEnter(Collider col)
        {
            if(col.gameObject.CompareTag("Goal"))
            {
                Debug.Log("Hit Goal");
                arena.AddScore(team);
                Destroy(gameObject);
            }
        }
    }
}