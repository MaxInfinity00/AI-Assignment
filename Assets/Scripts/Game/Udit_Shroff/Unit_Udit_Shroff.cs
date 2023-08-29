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
        protected override GraphUtils.Path GetPathToTarget()
        {
            return Team.GetShortestPath(CurrentNode, TargetNode);
        }
        
        protected override Unit SelectTarget(List<Unit> enemiesInRange)
        {
            Debug.Log("setting fire target");
            return ClosestEnemy;
        }
        
        protected override void Start()
        {
            base.Start();
            StartCoroutine(SmartLogic());
        }

        IEnumerator SmartLogic()
        {
            while (true)
            {
                Unit enemy = ClosestEnemy;
                if(enemy!=null)
                    TargetNode = ClosestEnemy.CurrentNode;
                yield return new WaitForSeconds(1);
            }
        }
    }
}