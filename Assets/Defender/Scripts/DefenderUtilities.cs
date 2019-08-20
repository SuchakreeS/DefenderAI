using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defender;

namespace Defender
{
    public enum TeamType
    {
        None,
        A,
        B
    }
    public static class DefenderUtilities
    {
        public static TeamType GetOppositeTeam(TeamType _team)
        {
            if (_team == TeamType.A)
                return TeamType.B;
            else if (_team == TeamType.B)
                return TeamType.A;
            else
                return TeamType.None;
        }
        public static TeamType RandomTeam(this TeamType _teamType)
        {
            return (TeamType) UnityEngine.Random.Range(1, 3);
        }
    }
}

