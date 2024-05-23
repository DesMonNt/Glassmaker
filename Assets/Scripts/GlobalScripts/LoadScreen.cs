// using System.Collections;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UIElements;
//
// namespace GlobalScripts
// {
//     public class LoadScreen :MonoBehaviour
//     {
//         [SerializeField] public GameObject loadingScreen;
//
//         private string _sceneName;
//
//         private LoadingBar _scale;
//
//         public LoadScreen(GameObject aloadingScreen, string sceneName)
//         {
//             loadingScreen = aloadingScreen;
//             _sceneName = sceneName;
//         }
//
//         public void Loading()
//         {
//             loadingScreen.SetActive(true);
//
//             StartCoroutine(LoadAsync());
//         }
//         
//         IEnumerator LoadAsync()
//         {
//             var loadAsync = SceneManager.LoadSceneAsync(_sceneName);
//             loadAsync.allowSceneActivation = false;
//
//             while (!loadAsync.isDone)
//             {
//                 _scale.percent = loadAsync.progress * 10;
//
//                 if (!(loadAsync.progress >= .9f) || loadAsync.allowSceneActivation) 
//                     continue;
//                 yield return new WaitForSeconds(2.2f);
//                 loadAsync.allowSceneActivation = true;
//             }
//
//             yield return null;
//         }
//     }
//
//     
// }