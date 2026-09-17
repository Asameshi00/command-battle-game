using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Common.SceneSystem
{
    public class FadeManager : MonoBehaviour
    {
        [SerializeField, Tooltip("フェードの時間")] private float fadeDuration;
        [SerializeField, Tooltip("フェードパネル")] private GameObject fadePanel;

        private Image _fadeImage;

        private void Awake()
        {
            var canvas = GameObject.FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                var fadePanelTransform = canvas.transform.Find("FadePanel");
                if (fadePanelTransform != null)
                {
                    fadePanel = fadePanelTransform.gameObject;
                    _fadeImage = fadePanel.GetComponent<Image>();
                    DontDestroyOnLoad(fadePanel);
                }
                else
                {
                    Debug.LogWarning("FadePanelがCanvasの子に見つかりませんでした。");
                }
            }
            else
            {
                Debug.LogWarning("Canvasがシーン内に見つかりませんでした。");
            }

            DontDestroyOnLoad(fadePanel);
        }

        public async UniTask FadeInAsync()
        {
            if (_fadeImage == null) return;

            fadePanel.SetActive(true);
            Color c = _fadeImage.color;
            c.a = 1.0f;
            _fadeImage.color = c;

            while (_fadeImage.color.a > 0.0f)
            {
                c = _fadeImage.color;
                c.a -= Time.deltaTime / fadeDuration;
                _fadeImage.color = c;
                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            fadePanel.SetActive(false);
        }

        public async UniTask FadeOutAsync()
        {
            if (_fadeImage == null) return;

            fadePanel.SetActive(true);
            Color c = _fadeImage.color;
            c.a = 0.0f;
            _fadeImage.color = c;

            while (_fadeImage.color.a < 1.0f)
            {
                c = _fadeImage.color;
                c.a += Time.deltaTime / fadeDuration;
                _fadeImage.color = c;
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }

        public async UniTask FadeAndSwitchSceneAsync(string sceneName)
        {
            await FadeOutAsync();
            await SceneManager.Instance.LoadSceneAsync(sceneName);
            await FadeInAsync();
        }
    }
}