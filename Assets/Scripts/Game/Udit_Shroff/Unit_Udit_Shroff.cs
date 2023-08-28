using AI;
using Game;
using Graphs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Udit_Shroff
{
    public class Unit_Udit_Shroff : Unit
    {
        #region Properties

        public new Team_Udit_Shroff Team => base.Team as Team_Udit_Shroff;

        #endregion

        protected override Unit SelectTarget(List<Unit> enemiesInRange)
        {
            
            return enemiesInRange[Random.Range(0, enemiesInRange.Count)];
        }
    }
}