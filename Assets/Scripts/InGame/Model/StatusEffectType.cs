namespace InGame.Model
{
    /// <summary>
    /// 状態異常・バフ/デバフの種別。StatusEffectManager が処理を分岐する。
    /// </summary>
    public enum StatusEffectType
    {
        Poison, // 毒（毎ターンHP減少）
        Paralysis, // 麻痺（一定確率で行動不能）
        Sleep, // 睡眠（行動不能）

        AttackUp, // 攻撃力上昇
        AttackDown, // // 攻撃力減少
        DefenseUp, // 防御力上昇
        DefenseDown, // 防御力上昇
        SpeedUp, // 素早さ上昇
        SpeedDown, // 素早さ減少
    }
}
