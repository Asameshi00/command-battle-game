namespace InGame.Battle.Model
{
    /// <summary>
    /// 1回のバトルの進行状態を保持する箱。
    /// バトル外(GameManager等)からの入力と、進行中の一時情報をまとめる。
    /// </summary>
    public class BattleContext
    {
        /// <summary>現在のステート。</summary>
        public BattleState State { get; set; } = BattleState.None;

        /// <summary>経過ターン数（1始まり）。</summary>
        public int TurnCount { get; set; }

        // TODO: パーティ編成・敵編成データ、逃走可否、背景/BGM ID などを追加する。
        // TODO: バトル結果(勝敗)を RewardManager / Result シーンへ受け渡すための情報を持たせる。
    }
}
