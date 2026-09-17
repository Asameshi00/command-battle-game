namespace InGame.Battle.Model
{
    /// <summary>
    /// バトル全体のステートマシンの状態。
    /// BattleManagerがこの順に遷移させてターンを回す。
    /// </summary>
    public enum BattleState
    {
        /// <summary>未初期化。</summary>
        None,

        /// <summary>ユニット配置・開始演出など、バトル開始処理。</summary>
        Start,

        /// <summary>1ターンの開始処理（状態異常のターン更新など）。</summary>
        TurnStart,

        /// <summary>各ユニットの行動（コマンド／AI）を決定するフェーズ。</summary>
        CommandInput,

        /// <summary>決定された行動を順に実行するフェーズ。</summary>
        ActionExecute,

        /// <summary>1ターンの終了処理。</summary>
        TurnEnd,

        /// <summary>勝利。</summary>
        Win,

        /// <summary>敗北。</summary>
        Lose,

        /// <summary>バトル終了（Result遷移などの後始末）。</summary>
        Finished,
    }
}
