using UnityEngine;
using System.Collections;

public class ViewUtils {

	public static void removeChildrenFromTransform(Transform parent){
		if (parent != null) {
			foreach (Transform child in parent) {
				GameObject.Destroy (child.gameObject);
			}
		}
	}


	public static void removeChildByIndex(Transform parent, int index){
		if (parent != null) {
			Transform childToRemove = parent.GetChild (index);
			GameObject.Destroy (childToRemove);
		}
	}
}
