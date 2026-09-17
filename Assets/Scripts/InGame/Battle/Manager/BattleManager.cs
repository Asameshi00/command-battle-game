using System;
using Cysharp.Threading.Tasks;
using InGame.Battle.Model;
using UnityEngine;

namespace InGame.Battle.Manager
{
    /// <summary>
    /// コマンドバトルの司令塔。ステートマシンでターン制の進行を回す。
    /// 細かい計算は各Model/他Managerに委譲し、ここは「進行の調停」に徹する。
    /// </summary>
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private UnitManager unitManager;

        private BattleContext _context;

        /// <summary>現在のステート。</summary>
        public BattleState State => _context?.State ?? BattleState.None;

        /// <summary>
        /// バトルを開始する。勝敗が決まるまでステートを回し続ける。
        /// </summary>
        public async UniTask RunBattleAsync()
        {
            _context = new BattleContext();
            ChangeState(BattleState.Start);

            // 勝敗が確定するまでループ
            while (_context.State != BattleState.Finished)
            {
                switch (_context.State)
                {
                    case BattleState.Start:
                        await OnStartAsync();
                        ChangeState(BattleState.TurnStart);
                        break;

                    case BattleState.TurnStart:
                        await OnTurnStartAsync();
                        ChangeState(BattleState.CommandInput);
                        break;

                    case BattleState.CommandInput:
                        await OnCommandInputAsync();
                        ChangeState(BattleState.ActionExecute);
                        break;

                    case BattleState.ActionExecute:
                        await OnActionExecuteAsync();
                        ChangeState(BattleState.TurnEnd);
                        break;

                    case BattleState.TurnEnd:
                        await OnTurnEndAsync();
                        ChangeState(ResolveNextStateAfterTurn());
                        break;

                    case BattleState.Win:
                        await OnWinAsync();
                        ChangeState(BattleState.Finished);
                        break;

                    case BattleState.Lose:
                        await OnLoseAsync();
                        ChangeState(BattleState.Finished);
                        break;

                    default:
                        // None / 想定外はループを抜ける（保険）。
                        ChangeState(BattleState.Finished);
                        break;
                }
            }
        }

        /// <summary>
        /// ステート遷移（将来ログ・イベント発火をここに集約できる）
        /// </summary>
        private void ChangeState(BattleState next)
        {
            _context.State = next;
            // TODO: 状態変化イベントを通知（UIやSEのトリガー用）。
        }

        /// <summary>
        /// ターン終了後、勝敗判定して次ステートを決める
        /// </summary>
        private BattleState ResolveNextStateAfterTurn()
        {
            if (unitManager != null && unitManager.IsEnemyWipedOut()) return BattleState.Win;
            if (unitManager != null && unitManager.IsPlayerWipedOut()) return BattleState.Lose;
            return BattleState.TurnStart;
        }

        // --- 各フェーズ処理（ひな型：中身はTODO） ---

        private async UniTask OnStartAsync()
        {
            // TODO: unitManager でパーティ/敵をSpawn、開始演出、BGM再生。
            await UniTask.CompletedTask;
        }

        private async UniTask OnTurnStartAsync()
        {
            _context.TurnCount++;
            // TODO: 状態異常のターン更新、TurnManagerで行動順を確定。
            await UniTask.CompletedTask;
        }

        private async UniTask OnCommandInputAsync()
        {
            // TODO: プレイヤーはCommandManagerで入力待ち、敵はEnemyAIで行動決定。
            await UniTask.CompletedTask;
        }

        private async UniTask OnActionExecuteAsync()
        {
            // TODO: 行動順に沿って行動を実行（DamageCalculator・エフェクト再生）。
            //       1行動ごとに勝敗を確認し、決着したら以降をスキップしてもよい。
            await UniTask.CompletedTask;
        }

        private async UniTask OnTurnEndAsync()
        {
            // TODO: ターン終了時の毒ダメージ等、持続効果の処理。
            await UniTask.CompletedTask;
        }

        private async UniTask OnWinAsync()
        {
            // TODO: 勝利演出、RewardManagerで報酬確定 → Resultへ受け渡し。
            await UniTask.CompletedTask;
        }

        private async UniTask OnLoseAsync()
        {
            // TODO: 敗北演出 → ゲームオーバー/リトライ処理。
            await UniTask.CompletedTask;
        }
    }
}
