using System;
using UnityEngine;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.Variables;


namespace GameCreator.Runtime.VisualScripting
{
    [Title("Calculate Damage Formula")]
    [Description("Performs a custom calculation based on PhysicalDamage, MagicDamage, Percentage of Both Damages, Critical Chance, SkillMultiplier, and the Defense Value")]
    [Category("Math/Damage Formula")]

    [Keywords("Calculation", "Formula")]
    [Image(typeof(IconDivideCircle), ColorTheme.Type.Blue)]

    [Serializable]
    public class InstructionCaculateDamage : Instruction
    {

        [SerializeField]
        private PropertyGetDecimal m_MinMaxDamage = new PropertyGetDecimal();

        private PropertyGetDecimal m_CriticalChance = new PropertyGetDecimal();
        [SerializeField]
        private PropertyGetDecimal m_SkillMultiplier = new PropertyGetDecimal();
        [SerializeField]
        private PropertyGetDecimal m_Defense = new PropertyGetDecimal();
        [SerializeField]
        private PropertySetNumber m_Result = new PropertySetNumber();

        // PROPERTIES: ----------------------------------------------------------------------------

        public override string Title => $"Formula to calulate the total damage";

        // RUN METHOD: ----------------------------------------------------------------------------

         protected override Task Run(Args args)
        {
            double damage = m_MinMaxDamage.Get(args);
            double criticalChance = m_CriticalChance.Get(args);
            double skillMulitplier = m_SkillMultiplier.Get(args);
            double defenseValue = m_Defense.Get(args);
            
            
            
            double totalDamage = skillMulitplier * damage;

            double resultValue = totalDamage * (100.0 / (100.0 + defenseValue));

            this.m_Result.Set(resultValue, args);

            return DefaultResult;
        }
    }
}