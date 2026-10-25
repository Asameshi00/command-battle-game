using System;
using UnityEngine;

namespace InGame.Model
{
    /// <summary>
    /// マスターデータ上の基礎ステータス。CharacterData / EnemyData で共通利用する。
    /// バトル中の値は UnitStatus 側で保持する。
    /// </summary>
    [Serializable]
    public class BaseStatus
    {
        [SerializeField, Min(1)] private int maxHp = 1;
        [SerializeField, Min(0)] private int maxMp;
        [SerializeField, Min(0)] private int attack;
        [SerializeField, Min(0)] private int defense;
        [SerializeField, Min(0)] private int speed;

        public int MaxHp => maxHp;
        public int MaxMp => maxMp;
        public int Attack => attack;
        public int Defense => defense;
        public int Speed => speed;
    }
}
