using UnityEngine;
using System.Collections;

public class D901BaseObject : MonoBehaviour {

	public D901Application app { get { return GameObject.FindObjectOfType<D901Application>(); }}

	private Transform _transform = null;
	public Transform transform {
		get {
			if (_transform == null) {
				_transform = transform;
			}
			return _transform;
		}
		protected set {
			_transform = value;
		}
	}
}
