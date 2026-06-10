// extension awaiter/methods can be used by this namespace
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Samples.Unitask.Demo
{
    class UnitaskDemo : MonoBehaviour
    {
        // You can return type as struct UniTask<T>(or UniTask), it is unity specialized lightweight alternative of Task<T>
        // zero allocation and fast excution for zero overhead async/await integrate with Unity
        async UniTask<string> DemoAsync()
        {
            // You can await Unity's AsyncObject
            var asset = await Resources.LoadAsync<TextAsset>("foo");
            var txt = (await UnityWebRequest.Get("https://...").SendWebRequest()).downloadHandler.text;
            await SceneManager.LoadSceneAsync("scene2");

            // .WithCancellation enables Cancel, GetCancellationTokenOnDestroy synchornizes with lifetime of GameObject
            // after Unity 2022.2, you can use `destroyCancellationToken` in MonoBehaviour
            var asset2 = await Resources.LoadAsync<TextAsset>("bar").WithCancellation(this.GetCancellationTokenOnDestroy());

            // Handle cancellation token
            var cts = new CancellationTokenSource();
            var asset3 = await Resources.LoadAsync<TextAsset>("bar").WithCancellation(cts.Token);


            // .ToUniTask accepts progress callback(and all options), Progress.Create is a lightweight alternative of IProgress<T>
            var asset4 = await Resources.LoadAsync<TextAsset>("baz").ToUniTask(Progress.Create<float>(x => Debug.Log(x)));

            // await frame-based operation like a coroutine
            await UniTask.DelayFrame(100);

            // replacement of yield return new WaitForSeconds/WaitForSecondsRealtime
            await UniTask.Delay(TimeSpan.FromSeconds(10), ignoreTimeScale: false);

            // yield any playerloop timing(PreUpdate, Update, LateUpdate, etc...)
            await UniTask.Yield(PlayerLoopTiming.PreLateUpdate);

            // replacement of yield return null
            await UniTask.Yield();
            await UniTask.NextFrame();

            // replacement of WaitForEndOfFrame
#if UNITY_2023_1_OR_NEWER
            await UniTask.WaitForEndOfFrame();
#else
    // requires MonoBehaviour(CoroutineRunner))
    await UniTask.WaitForEndOfFrame(this); // this is MonoBehaviour
#endif

            // replacement of yield return new WaitForFixedUpdate(same as UniTask.Yield(PlayerLoopTiming.FixedUpdate))
            await UniTask.WaitForFixedUpdate();

            // replacement of yield return WaitUntil
            await UniTask.WaitUntil(() => isActiveAndEnabled == false);

            // special helper of WaitUntil
            await UniTask.WaitUntilValueChanged(this, x => x.isActiveAndEnabled);

            // You can await IEnumerator coroutines
            await FooCoroutineEnumerator();

            // You can await a standard task
            await Task.Run(() => 100);

            // Multithreading, run on ThreadPool under this code
            await UniTask.SwitchToThreadPool();

            /* work on ThreadPool */

            // return to MainThread(same as `ObserveOnMainThread` in UniRx)
            await UniTask.SwitchToMainThread();

            // get async webrequest
            async UniTask<string> GetTextAsync(UnityWebRequest req)
            {
                var op = await req.SendWebRequest();
                return op.downloadHandler.text;
            }

            var task1 = GetTextAsync(UnityWebRequest.Get("http://google.com"));
            var task2 = GetTextAsync(UnityWebRequest.Get("http://bing.com"));
            var task3 = GetTextAsync(UnityWebRequest.Get("http://yahoo.com"));

            // concurrent async-wait and get results easily by tuple syntax
            var (google, bing, yahoo) = await UniTask.WhenAll(task1, task2, task3);

            // shorthand of WhenAll, tuple can await directly
            var (google2, bing2, yahoo2) = await (task1, task2, task3);

            // return async-value.(or you can use `UniTask`(no result), `UniTaskVoid`(fire and forget)).
            return (asset as TextAsset)?.text ?? throw new InvalidOperationException("Asset not found");
        }

        IEnumerator FooCoroutineEnumerator()
        {
            yield return new WaitForSeconds(1);
            Debug.Log("Hello");
        }
    }
}