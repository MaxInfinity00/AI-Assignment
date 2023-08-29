using UnityEngine;
using AI;

namespace Udit_Shroff
{
    public class Unit_Brain_Udit_Shroff : Brain
    {
        private Unit_Udit_Shroff m_unit;

        #region  Properties

        public Unit_Udit_Shroff Unit => m_unit;
        
        #endregion
        protected override void Start()
        {
            base.Start();
            m_unit = GetComponent<Unit_Udit_Shroff>();
        }
    }
}