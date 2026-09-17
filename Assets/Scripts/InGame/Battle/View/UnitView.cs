using InGame.Battle.Model;
using UnityEngine;

namespace InGame.Battle.View
{
    /// <summary>
    /// 1ユニットの見た目（立ち絵/スプライト・HP/MPバー等）を担当するView。
    /// Model(BattleUnit)をUnitIdで紐付け、Presenter/BattleUIManagerから更新される。
    /// ロジックは持たず、表示とアニメ(DOTween)だけを担う。
    /// </summary>
    public class UnitView : MonoBehaviour
    {
        [Header("Refs (Inspectorで設定)")]
        [SerializeField] private Transform hpBarFill;   // TODO: Slider/Imageに差し替え
        [SerializeField] private Transform mpBarFill;   // TODO: Slider/Imageに差し替え

        /// <summary>紐付くユニットID（BattleUnit.UnitId）。</summary>
        public int UnitId { get; private set; }

        /// <summary>ユニットの初期表示をセットアップする。</summary>
        public void Setup(BattleUnit unit)
        {
            UnitId = unit.UnitId;
            // TODO: 立ち絵/スプライト、名前ラベルの反映。
            Refresh(unit.Status);
        }

        /// <summary>ステータス変化を表示に反映する。</summary>
        public void Refresh(UnitStatus status)
        {
            // TODO: HP/MPバーの割合更新、数値ラベル反映。DOTweenで補間する。
        }

        /// <summary>選択カーソル（ターゲット指定時のハイライト）表示切り替え。</summary>
        public void SetTargetHighlight(bool active)
        {
            // TODO: カーソル/枠の表示切り替え。
        }

        /// <summary>被ダメージ演出（ヒットストップ・点滅・数値ポップ等）。</summary>
        public void PlayDamageEffect(int amount)
        {
            // TODO: DOTweenでシェイク＋ダメージ数値ポップ。
        }

        /// <summary>戦闘不能演出。</summary>
        public void PlayDeadEffect()
        {
            // TODO: フェードアウト/退場アニメ。
        }
    }
}
