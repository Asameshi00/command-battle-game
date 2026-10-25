using System;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Model
{
    /// <summary>
    /// 敵データ
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyData", menuName = "CommandBattle/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private string enemyName;
        [SerializeField] private BaseStatus baseStatus;

        [SerializeField, Tooltip("使用する技（EnemyAIManager が行動パターンとして参照）。")] private List<SkillData> skills = new List<SkillData>();

        [SerializeField] private List<ElementAffinity> elementAffinities = new List<ElementAffinity>();

        [Header("Reward")]
        [SerializeField, Min(0)] private int exp;
        [SerializeField, Min(0)] private int gold;
        [SerializeField] private List<DropItem> dropItems = new List<DropItem>();

        [Header("Visual")]
        [SerializeField] private Sprite sprite;

        public string EnemyName => enemyName;
        public BaseStatus BaseStatus => baseStatus;
        public IReadOnlyList<SkillData> Skills => skills;
        public IReadOnlyList<ElementAffinity> ElementAffinities => elementAffinities;
        public int Exp => exp;
        public int Gold => gold;
        public IReadOnlyList<DropItem> DropItems => dropItems;
        public Sprite Sprite => sprite;
    }

    /// <summary>
    /// ドロップ品とドロップ率。
    /// </summary>
    [Serializable]
    public class DropItem
    {
        // TODO: ItemData実装後に ItemData 参照へ差し替える。
        [SerializeField] private string itemId;
        [SerializeField, Range(0f, 1f)] private float dropRate;

        public string ItemId => itemId;
        public float DropRate => dropRate;
    }
}
