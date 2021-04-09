using UnityEngine;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace MapacheSplash
{
	public class MapacheSplashSceneChanger : MonoBehaviour
	{
		[SerializeField]
		private int nextScene = 1;

		public void SplashFinishedSceneChange ()
		{
			SceneManager.LoadScene(nextScene);
		}
	}
}