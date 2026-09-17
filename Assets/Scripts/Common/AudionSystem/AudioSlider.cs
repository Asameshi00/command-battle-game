using UnityEngine;

namespace Common.AudionSystem
{
    /// <summary>
    /// Audio調整のスライダー設定
    /// </summary>
    public class AudioSlider : MonoBehaviour
    {
        [SerializeField, Tooltip("BGMスライダー")] private UnityEngine.UI.Slider bgmSlider;
        [SerializeField, Tooltip("SEスライダー")] private UnityEngine.UI.Slider seSlider;
        
        /// <summary>
        /// Volume保存用のkeyとデフォルト値
        /// </summary>
        private const string BGMVolumeKey = "BGM_VOLUME_KEY";
        private const string SeVolumeKey  = "SE_VOLUME_KEY";
        private const float  BGMVolumeDefault = 0.3f;
        private const float  SeVolumeDefault  = 0.4f;
        
        private void Start()
        {
            var bgmVolume = PlayerPrefs.GetFloat(BGMVolumeKey, BGMVolumeDefault);
            var seVolume = PlayerPrefs.GetFloat(SeVolumeKey, SeVolumeDefault);
            
            // スライダーの初期値を設定
            bgmSlider.value = bgmVolume;
            seSlider.value = seVolume;
            
            AudioManager.Instance.SetBGMVolume(bgmVolume);
            AudioManager.Instance.SetSeVolume(seVolume);
            
            // スライダー変更時のイベント登録
            bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
            seSlider.onValueChanged.AddListener(OnSEVolumeChanged);
        }
        
        
        /// <summary>
        /// AudioManagerにBGMのボリュームを設定
        /// </summary>
        /// <param name="value"></param>
        public static void OnBGMVolumeChanged(float value)
        {
            AudioManager.Instance.SetBGMVolume(value);
            // bgmSlider.value = value;
        }
        
        
        /// <summary>
        /// AudioManagerにSEのボリュームを設定
        /// </summary>
        /// <param name="value"></param>
        public static void OnSEVolumeChanged(float value)
        {
            AudioManager.Instance.SetSeVolume(value);
            // seSlider.value = value;
        }
        
        private void OnDestroy()
        {
            // AudioManagerとスライドバーがnullの場合は何もしない
            if (AudioManager.Instance == null) return;
            if (bgmSlider == null || seSlider == null) return;
            
            // スライダーのイベントリスナーを解除
            bgmSlider.onValueChanged.RemoveListener(OnBGMVolumeChanged);
            seSlider.onValueChanged.RemoveListener(OnSEVolumeChanged);
            
            // ボリュームを保存
            PlayerPrefs.SetFloat(BGMVolumeKey, bgmSlider.value);
            PlayerPrefs.SetFloat(SeVolumeKey, seSlider.value);
            PlayerPrefs.Save();
        }
    }
}