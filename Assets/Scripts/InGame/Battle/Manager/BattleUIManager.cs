using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using InGame.Battle.Model;
using InGame.Battle.View;
using UnityEngine;

namespace InGame.Battle.Manager
{
    /// <summary>
    /// バトルUIの束ね役。各Viewの生成・更新をまとめ、BattleManagerからの指示を表示へ橋渡しする。
    /// FE型レイアウト：味方エリアと敵エリアの両方に UnitView を並べて表示する。
    /// ここ自身はロジックを持たず、Model→Viewの反映に徹する（Presenter的役割）。
    /// </summary>
    public class BattleUIManager : MonoBehaviour
    {
        [Header("Layout Roots（FE型：味方列と敵列）")]
        [SerializeField] private Transform playerPartyRoot; // 味方UnitViewの親（横並び等）
        [SerializeField] private Transform enemyPartyRoot;  // 敵UnitViewの親

        [Header("Prefabs")]
        [SerializeField] private UnitView unitViewPrefab;

        [Header("Windows")]
        [SerializeField] private GameObject commandWindowRoot; // TODO: CommandWindowViewに差し替え
        [SerializeField] private GameObject messageWindowRoot;  // TODO: メッセージ/ログ表示Viewに差し替え

        /// <summary>UnitId → 生成済みUnitView の対応表。</summary>
        private readonly Dictionary<int, UnitView> _unitViews = new Dictionary<int, UnitView>();

        /// <summary>
        /// 全ユニットのViewを生成・配置する（バトル開始時に1回）。
        /// 陣営ごとに配置先ルートを分けてFE型に並べる。
        /// </summary>
        public void BuildUnitViews(IEnumerable<BattleUnit> units)
        {
            foreach (var unit in units)
            {
                var root = unit.IsPlayer ? playerPartyRoot : enemyPartyRoot;
                var view = Instantiate(unitViewPrefab, root);
                view.Setup(unit);
                _unitViews[unit.UnitId] = view;
            }
        }

        /// <summary>指定ユニットの表示を最新ステータスへ更新する。</summary>
        public void RefreshUnit(BattleUnit unit)
        {
            if (_unitViews.TryGetValue(unit.UnitId, out var view))
            {
                view.Refresh(unit.Status);
            }
        }

        /// <summary>UnitIdからViewを取得する（演出呼び出し用）。見つからなければnull。</summary>
        public UnitView GetUnitView(int unitId)
        {
            return _unitViews.TryGetValue(unitId, out var view) ? view : null;
        }

        /// <summary>ターゲット候補のハイライトをまとめて切り替える。</summary>
        public void SetTargetHighlights(IEnumerable<int> targetUnitIds, bool active)
        {
            foreach (var id in targetUnitIds)
            {
                GetUnitView(id)?.SetTargetHighlight(active);
            }
        }

        /// <summary>コマンドウィンドウの表示切り替え。</summary>
        public void SetCommandWindowVisible(bool visible)
        {
            if (commandWindowRoot != null) commandWindowRoot.SetActive(visible);
            // TODO: CommandWindowView.Show/Hide（アニメ付き）に差し替え。
        }

        /// <summary>メッセージを1件表示して、送り待ち/一定時間待つ。</summary>
        public async UniTask ShowMessageAsync(string message)
        {
            // TODO: messageWindowRoot にテキスト反映、クリック待ち or 一定秒待機。
            await UniTask.CompletedTask;
        }

        /// <summary>バトル終了時のUIクリーンアップ。</summary>
        public void Clear()
        {
            foreach (var view in _unitViews.Values)
            {
                if (view != null) Destroy(view.gameObject);
            }
            _unitViews.Clear();
        }
    }
}
