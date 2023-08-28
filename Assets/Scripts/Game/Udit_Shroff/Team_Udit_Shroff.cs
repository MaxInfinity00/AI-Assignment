using Game;
using Graphs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Udit_Shroff
{
    public class Team_Udit_Shroff : Team
    {
        [SerializeField]
        private Color   m_myFancyColor;

        #region Properties

        public override Color Color => m_myFancyColor;

        #endregion
    }
}