using System;
using Common.Interface;
using Cysharp.Threading.Tasks;

namespace Common.SceneSystem
{
    /// <summary>
    /// Sceneの読み込み管理を行うシングルトンクラス
    /// </summary>
    public class SceneManager : SingletonMonoBehaviourBase<SceneManager>
    {
        /// <summary>
        /// Sceneのロード
        /// </summary>
        /// <param name="sceneName"></param>
        public async UniTask LoadSceneAsync(string sceneName)
        {
            var operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            if (operation != null)
            {
                operation.allowSceneActivation = false;

                while (operation.progress < 0.9f)
                {
                    await UniTask.Yield();
                }

                operation.allowSceneActivation = true;
                await UniTask.WaitUntil(() => operation.isDone);
            }
        }
        
        /// <summary>
        /// AdditiveでSceneを追加読み込み
        /// </summary>
        public async UniTask LoadSceneAdditiveAsync(string sceneName, Action<float> onProgress = null)
        {
            var operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            if (operation != null)
            {
                operation.allowSceneActivation = false;

                while (operation.progress < 0.9f)
                {
                    onProgress?.Invoke(operation.progress);
                    await UniTask.Yield();
                }

                onProgress?.Invoke(1.0f);
                operation.allowSceneActivation = true;
                await UniTask.WaitUntil(() => operation.isDone);
            }
        }
        
        
        /// <summary>
        /// Sceneのアンロード
        /// </summary>
        /// <param name="sceneName"></param>
        public async UniTask UnloadSceneAsync(string sceneName)
        {
            var operation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
            if (operation != null)
            {
                await UniTask.WaitUntil(() => operation.isDone);
            }
        }
    }
}