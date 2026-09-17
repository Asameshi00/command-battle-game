using System.Collections.Generic;
using System.Linq;
using InGame.Battle.Model;
using UnityEngine;

namespace InGame.Battle.Manager
{
    /// <summary>
    /// バトル参加ユニット(BattleUnit)の生成・保持・生死管理を担うManager。
    /// バトル1回のライフサイクル（BattleManagerが所有）を想定し、シングルトンにはしない。
    /// </summary>
    public class UnitManager : MonoBehaviour
    {
        private readonly List<BattleUnit> _units = new List<BattleUnit>();
        private int _nextUnitId;

        /// <summary>全ユニット（読み取り専用）。</summary>
        public IReadOnlyList<BattleUnit> Units => _units;

        /// <summary>生存しているプレイヤー側ユニット。</summary>
        public IEnumerable<BattleUnit> AlivePlayers =>
            _units.Where(u => u.IsPlayer && u.IsAlive);

        /// <summary>生存している敵側ユニット。</summary>
        public IEnumerable<BattleUnit> AliveEnemies =>
            _units.Where(u => !u.IsPlayer && u.IsAlive);

        /// <summary>
        /// ステータスからユニットを生成して登録する。生成したユニットを返す。
        /// TODO: CharacterData / EnemyData(ScriptableObject) から生成する経路を追加する。
        /// </summary>
        public BattleUnit Spawn(UnitSide side, UnitStatus status)
        {
            var unit = new BattleUnit(_nextUnitId++, side, status);
            _units.Add(unit);
            // TODO: 対応するUnitView(HP/MPバー・立ち絵)の生成・紐付けを行う。
            return unit;
        }

        /// <summary>IDからユニットを取得する。見つからなければnull。</summary>
        public BattleUnit FindById(int unitId)
        {
            return _units.FirstOrDefault(u => u.UnitId == unitId);
        }

        /// <summary>プレイヤー側が全滅しているか。</summary>
        public bool IsPlayerWipedOut() => !AlivePlayers.Any();

        /// <summary>敵側が全滅しているか。</summary>
        public bool IsEnemyWipedOut() => !AliveEnemies.Any();

        /// <summary>登録済みユニットをすべて破棄する（バトル終了時のクリーンアップ）。</summary>
        public void Clear()
        {
            _units.Clear();
            _nextUnitId = 0;
            // TODO: 生成済みUnitViewの破棄もここで行う。
        }
    }
}
