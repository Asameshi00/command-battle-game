using UnityEngine;

namespace InGame.Model
{
    /// <summary>
    /// 効果・持続ターンのデータ
    /// </summary>
    [CreateAssetMenu(fileName = "StatusEffectData", menuName = "CommandBattle/StatusEffectData")]
    public class StatusEffectData : ScriptableObject
    {
        [SerializeField] private string effectName;
        [SerializeField] private StatusEffectType effectType;

        [SerializeField, Tooltip("持続ターン数。"), Min(1)] private int durationTurns = 3;

        [SerializeField, Tooltip("毎ターン終了時のHP変化量（最大HPに対する割合）。負数でダメージ、正数で回復。"), Range(-1f, 1f)] private float hpRatePerTurn;

        [SerializeField, Tooltip("ステータス補正倍率（AttackUp/Down 等で使用）。1.5=1.5倍, 0.7=0.7倍。"), Min(0f)] private float statModifierRate = 1f;

        [SerializeField, Tooltip("行動不能になる確率（0～1）。麻痺/睡眠で使用。"), Range(0f, 1f)] private float actionBlockRate;

        [SerializeField] private Sprite icon;

        public string EffectName => effectName;
        public StatusEffectType EffectType => effectType;
        public int DurationTurns => durationTurns;
        public float HpRatePerTurn => hpRatePerTurn;
        public float StatModifierRate => statModifierRate;
        public float ActionBlockRate => actionBlockRate;
        public Sprite Icon => icon;
    }
}
