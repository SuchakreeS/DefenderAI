using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if(team == TeamType.A)
            {
                gameObject.tag = "BulletA";
            }
            else if(team == TeamType.B)
            {
                gameObject.tag = "BulletB";
            }
        }

        public void OnTriggerEnter(Collider col)
        {
            if(col.gameObject.CompareTag("Goal"))
            {
                arena.AddScore(team);
                Destroy(gameObject);
            }
            else if (col.gameObject.CompareTag("Shield"))
            {
                Destroy(gameObject);
            }
        }
        
        private void SetBulletTag()
        {
            if(team == TeamType.A)
            {
                gameObject.tag = "BulletA";
            }
            else if(team == TeamType.B)
            {
                gameObject.tag = "BulletB";
            }
        }
    }
}