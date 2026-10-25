using System;
using UnityEngine;

namespace InGame.Model
{
    /// <summary>
    /// 属性ごとのダメージ倍率（弱点/耐性）。未設定の属性は等倍として扱う。
    /// </summary>
    [Serializable]
    public class ElementAffinity
    {
        [SerializeField] private ElementType element;

        [SerializeField, Tooltip("被ダメージ倍率。2.0=弱点, 0.5=耐性, 0=無効。"), Min(0f)] private float damageRate = 1f;

        public ElementType Element => element;
        public float DamageRate => damageRate;
    }
}
