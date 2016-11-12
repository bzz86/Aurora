using UnityEngine;
using System.Collections;

public class D901Singleton<T> : D901BaseObject where T : D901BaseObject {

	protected static T instance;

	protected void onAwake(T newInstance){
		if (instance != null && newInstance != instance) {
			DestroyImmediate (gameObject);
			return;
		}

		instance = newInstance;
		DontDestroyOnLoad(gameObject);
	}

	public static T getInstance()
	{
		return instance;
	}
}
