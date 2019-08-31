using UnityEngine;

namespace Defender
{
	public class SingletonDontDestroyOnLoad<T> : MonoBehaviour
	{
		public static T Instance;
		//-----------------------------------------------------------------
		protected virtual void Awake()
		{
			Instance    =   GetComponent<T>();
            DontDestroyOnLoad(gameObject);
		}

		//-----------------------------------------------------------------
	}
}
