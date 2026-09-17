using System;
using UnityEngine;

namespace InGame.Battle.Model
{
    /// <summary>
    /// 1ユニットのバトル中ステータス（HP/MP/各パラメータ）を保持するModel。
    /// MonoBehaviourに依存しない純粋C#（テスト・計算しやすさ優先）。
    /// </summary>
    [Serializable]
    public class UnitStatus
    {
        [SerializeField] private string unitName;

        [SerializeField] private int maxHp;
        [SerializeField] private int currentHp;
        [SerializeField] private int maxMp;
        [SerializeField] private int currentMp;

        // 基礎パラメータ（ダメージ計算・行動順で使用）
        [SerializeField] private int attack;
        [SerializeField] private int defense;
        [SerializeField] private int speed;

        public string UnitName => unitName;
        public int MaxHp => maxHp;
        public int CurrentHp => currentHp;
        public int MaxMp => maxMp;
        public int CurrentMp => currentMp;
        public int Attack => attack;
        public int Defense => defense;
        public int Speed => speed;

        /// <summary>戦闘不能かどうか</summary>
        public bool IsDead => currentHp <= 0;

        /// <summary>
        /// マスターデータ等から初期化する想定のコンストラクタ。
        /// TODO: CharacterData(ScriptableObject) からの生成に差し替える。
        /// </summary>
        public UnitStatus(string unitName, int maxHp, int maxMp, int attack, int defense, int speed)
        {
            this.unitName = unitName;
            this.maxHp = maxHp;
            this.currentHp = maxHp;
            this.maxMp = maxMp;
            this.currentMp = maxMp;
            this.attack = attack;
            this.defense = defense;
            this.speed = speed;
        }

        /// <summary>ダメージを受ける（0未満にはしない）。</summary>
        public void TakeDamage(int amount)
        {
            if (amount < 0) amount = 0;
            currentHp = Mathf.Max(0, currentHp - amount);
        }

        /// <summary>回復する（MaxHpを超えない）。</summary>
        public void Heal(int amount)
        {
            if (amount < 0) amount = 0;
            currentHp = Mathf.Min(maxHp, currentHp + amount);
        }

        /// <summary>MPを消費する。足りなければfalse。</summary>
        public bool TryConsumeMp(int amount)
        {
            if (currentMp < amount) return false;
            currentMp -= amount;
            return true;
        }
    }
}
