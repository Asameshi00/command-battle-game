namespace InGame.Battle.Model
{
    /// <summary>
    /// バトルのmodel
    /// </summary>

    /// <summary>
    /// バトルに参加する陣営
    /// </summary>
    public enum UnitSide
    {
        Player,
        Enemy,
    }

    /// <summary>
    /// バトルに参加する1ユニット。
    /// ステータス(Model)と陣営・識別を束ねる。Viewとはid経由で紐付ける想定。
    /// </summary>
    public class BattleUnit
    {
        /// <summary>
        /// ユニットを一意に識別するID（View紐付け・ターゲット指定に使用）
        /// </summary>
        public int UnitId { get; }

        public UnitSide Side { get; }

        public UnitStatus Status { get; }

        public bool IsPlayer => Side == UnitSide.Player;
        public bool IsAlive => !Status.IsDead;

        public BattleUnit(int unitId, UnitSide side, UnitStatus status)
        {
            UnitId = unitId;
            Side = side;
            Status = status;
        }
    }
}
