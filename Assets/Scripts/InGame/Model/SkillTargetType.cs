namespace InGame.Model
{
    /// <summary>
    /// スキルの対象範囲。TargetManager が選択候補の絞り込みに使用する。
    /// 「味方/敵」は使用者から見た陣営。
    /// </summary>
    public enum SkillTargetType
    {
        SingleEnemy, // 敵単体
        AllEnemies, // 敵全体
        SingleAlly, // 味方単体
        AllAllies, // 味方全体
        Self, // 自分自身
    }
}
