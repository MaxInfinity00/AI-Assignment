using AI;
using Game;
using Graphs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firstname_Lastname
{
    public class Unit_Firstname_Lastname : Unit
    {
        #region Properties

        public new Team_Firstname_Lastname Team => base.Team as Team_Firstname_Lastname;

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
            TargetNode = Battlefield.Instance.GetRandomNode();
            Debug.Log("smart logic start");
            while (true)
            {
                yield return new WaitForSeconds(1);
                Debug.Log("in the smart loop");
                TargetNode = ClosestEnemy.CurrentNode;
                // if(TargetNode!=null)
                //     Debug.Log("target set");
                // yield return null;
                // yield return new WaitForSeconds(1);
            }
        }
    }
}