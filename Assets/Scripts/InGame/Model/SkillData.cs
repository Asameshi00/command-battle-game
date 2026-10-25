using System;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Model
{
    /// <summary>
    /// スキルデータ
    /// </summary>
    [CreateAssetMenu(fileName = "SkillData", menuName = "CommandBattle/SkillData")]
    public class SkillData : ScriptableObject
    {
        [SerializeField] private string skillName;
        [SerializeField, TextArea] private string description;
        [SerializeField, Min(0)] private int mpCost;

        [SerializeField] private SkillCategory category;
        [SerializeField] private SkillTargetType targetType;
        [SerializeField] private ElementType element;

        [SerializeField, Tooltip("威力係数。DamageCalculator で攻撃力等に掛ける。"), Min(0f)] private float power = 1f;

        [SerializeField, Tooltip("命中率（0～1）。"), Range(0f, 1f)] private float hitRate = 1f;

        [SerializeField] private List<StatusEffectApplication> statusEffects = new List<StatusEffectApplication>();

        public string SkillName => skillName;
        public string Description => description;
        public int MpCost => mpCost;
        public SkillCategory Category => category;
        public SkillTargetType TargetType => targetType;
        public ElementType Element => element;
        public float Power => power;
        public float HitRate => hitRate;
        public IReadOnlyList<StatusEffectApplication> StatusEffects => statusEffects;
    }

    /// <summary>
    /// スキル命中時に付与する状態異常と付与確率。
    /// </summary>
    [Serializable]
    public class StatusEffectApplication
    {
        [SerializeField] private StatusEffectData effect;
        [SerializeField, Range(0f, 1f)] private float chance = 1f;

        public StatusEffectData Effect => effect;
        public float Chance => chance;
    }
}
