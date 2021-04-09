using UnityEngine;

namespace MapacheSplash
{
	public class MapacheSplashAudioPlayer : MonoBehaviour
	{
		[SerializeField]
		public AudioSource[] audioSources;

		public void PlayAll ()
		{
			foreach (AudioSource audioSource in audioSources)
			{
				Play(audioSource);
			}
		}

		public void PlayByIndex (int index)
		{
			Play(audioSources[index]);
		}

		private void Play (AudioSource audioSource)
		{
			audioSource.Play();
		}
	}
}