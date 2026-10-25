namespace InGame.Model
{
    /// <summary>
    /// スキルの種別。DamageCalculator / SkillManager で処理を分岐する。
    /// </summary>
    public enum SkillCategory
    {
        Physical, // 物理攻撃（攻撃力 vs 防御力）
        Magical, // 魔法攻撃
        Heal, // HP回復
        Support, // ダメージを伴わない補助（バフ/デバフ・状態異常付与のみ）
    }
}
