using System;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Model
{
    /// <summary>
    /// キャラクターデータ
    /// </summary>
    [CreateAssetMenu(fileName = "CharacterData", menuName = "CommandBattle/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private string characterName;
        [SerializeField] private BaseStatus baseStatus;
        [SerializeField] private List<LearnableSkill> learnableSkills = new List<LearnableSkill>();
        [SerializeField] private List<ElementAffinity> elementAffinities = new List<ElementAffinity>();

        [Header("Visual")]
        [SerializeField] private Sprite portrait;
        [SerializeField] private Sprite icon;

        public string CharacterName => characterName;
        public BaseStatus BaseStatus => baseStatus;
        public IReadOnlyList<LearnableSkill> LearnableSkills => learnableSkills;
        public IReadOnlyList<ElementAffinity> ElementAffinities => elementAffinities;
        public Sprite Portrait => portrait;
        public Sprite Icon => icon;
    }

    /// <summary>
    /// 習得レベルとスキルの組。
    /// </summary>
    [Serializable]
    public class LearnableSkill
    {
        [SerializeField, Min(1)] private int level = 1;
        [SerializeField] private SkillData skill;

        public int Level => level;
        public SkillData Skill => skill;
    }
}
