using System;
using Game;
using Graphs;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Udit_Shroff
{
    public class Team_Udit_Shroff : Team
    {
        [SerializeField]
        private Color   m_myFancyColor;

        public Battlefield.Node TeamTarget;

        #region Properties
        public Unit ClosestEnemy
        {
            get
            {
                Vector3 avgTeamPosition = AverageTeamPosition;
                float fBestDistance = float.MaxValue;
                Unit closestEnemy = null;
                foreach (Unit enemy in EnemyTeam.Units)
                {
                    float fDistance = Vector3.Distance(enemy.transform.position, transform.position);
                    if (fDistance < fBestDistance)
                    {
                        fBestDistance = fDistance;
                        closestEnemy = enemy; 
                    }
                }

                return closestEnemy;
            }
        }

        private Vector3 AverageTeamPosition
        {
            get
            {
                Vector3 position = Vector3.zero;
                foreach (Unit unit in Units)
                {
                    position += unit.transform.position;
                }
                return position/Units.Count();
            }
        }
        public override Color Color => m_myFancyColor;

        #endregion
        
        public new GraphUtils.Path GetShortestPath(Battlefield.Node start, Battlefield.Node goal)
        {
            if (start == null ||
                goal == null ||
                start == goal ||
                Battlefield.Instance == null)
            {
                return null;
            }

            // initialize pathfinding
            foreach (Battlefield.Node node in Battlefield.Instance.Nodes)
            {
                node?.ResetPathfinding();
            }

            // add start node
            start.m_fDistance = 0.0f;
            start.m_fRemainingDistance = Battlefield.Instance.Heuristic(goal, start);
            List<Battlefield.Node> open = new List<Battlefield.Node>();
            HashSet<Battlefield.Node> closed = new HashSet<Battlefield.Node>();
            open.Add(start);

            // search
            while (open.Count > 0)
            {
                // get next node (the one with the least remaining distance)
                Battlefield.Node current = open[0];
                for (int i = 1; i < open.Count; ++i)
                {
                    if (open[i].m_fRemainingDistance < current.m_fRemainingDistance)
                    {
                        current = open[i];
                    }
                }
                open.Remove(current);
                closed.Add(current);

                // found goal?
                if (current == goal)
                {
                    // construct path
                    GraphUtils.Path path = new GraphUtils.Path();
                    while (current != null)
                    {
                        path.Add(current.m_parentLink);
                        current = current != null && current.m_parentLink != null ? current.m_parentLink.Source : null;
                    }

                    path.RemoveAll(l => l == null);     // HACK: check if path contains null links
                    path.Reverse();
                    return path;
                }
                else
                {
                    foreach (Battlefield.Link link in current.Links)
                    {
                        if (link.Target is Battlefield.Node target)
                        {
                            if (!closed.Contains(target) &&
                                !Units.Contains(target.Unit))
                            {
                                float newDistance = current.m_fDistance + Vector3.Distance(current.WorldPosition, target.WorldPosition) + target.AdditionalCost;
                                
                                float newRemainingDistance = newDistance + Battlefield.Instance.Heuristic(goal,target);

                                if (open.Contains(target))
                                {
                                    if (newRemainingDistance < target.m_fRemainingDistance)
                                    {
                                        // re-parent neighbor node
                                        target.m_fRemainingDistance = newRemainingDistance;
                                        target.m_fDistance = newDistance;
                                        target.m_parentLink = link;
                                    }
                                }
                                else
                                {
                                    // add target to openlist
                                    target.m_fRemainingDistance = newRemainingDistance;
                                    target.m_fDistance = newDistance;
                                    target.m_parentLink = link;
                                    open.Add(target);
                                }
                            }
                        }
                    }
                }
            }

            // no path found :(
            return null;
        }
        protected override void Start()
        {
            base.Start();
            StartCoroutine(GangTarget());
        }

        IEnumerator GangTarget()
        {
            
            while (true)
            {
                Unit enemy = ClosestEnemy;
                if (enemy != null)
                {
                    TeamTarget = enemy.CurrentNode;
                    // foreach (Unit unit in Units)
                    // {
                    //     unit.TargetNode = ClosestEnemy.CurrentNode;
                    // }
                }
                yield return new WaitForSeconds(0.1F);
            }
        }
    }
}