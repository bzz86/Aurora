using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;

public class D901Application : MonoBehaviour {

	public D901Model model;
	public D901View view;
	public D901Controller controller;



	// Iterates all Controllers and delegates the notification data
	// This method can easily be found because every class is “BounceElement” and has an “app” 
	// instance.
	/*public void Notify(string p_event_path, Object p_target, params object[] p_data)
	{
		D901Controller[] controller_list = GetAllControllers();
		foreach(D901Controller c in controller_list)
		{
			c.OnNotification(p_event_path, p_target, p_data);
		}
	}*/

	/*public void Notify(string p_event, string p_data) {                        
		Traverse(delegate(Transform it) {
			D901Controller[] list = it.GetComponents<D901Controller>();
			for (int i = 0; i < list.Length; i++) list[i].OnNotification(p_event, p_target, p_data);
			return true;
		});
	}*/
	public void ServerNotification(string command, string data) {                        
		Traverse(delegate(Transform it) {
			D901Controller[] list = it.GetComponents<D901Controller>();
			
			for (int i = 0; i < list.Length; i++) {
				Debug.Log("D901Controller: " + list[i]);
				list[i].OnServerNotification(command, data);
			}
			return true;
		});
	}
	
	public void ClientNotification(string p_event, Object p_target, params object[] p_data) {                        
		Traverse(delegate(Transform it) {
			D901Controller[] list = it.GetComponents<D901Controller>();
			for (int i = 0; i < list.Length; i++) list[i].OnNotification(p_event, p_target, p_data);
			return true;
		});
	}

	/// <summary>
	/// Traverses this element's Transform hierarchy with DFS approach.
	/// </summary>
	/// <param name="p_callback"></param>
	public void Traverse(System.Predicate<Transform> p_callback) {
		OnTraverseStep(transform,p_callback);
	}

	/// <summary>
	/// Traverse one element then its children.
	/// </summary>
	/// <param name="p_target"></param>
	/// <param name="p_callback"></param>
	private void OnTraverseStep(Transform p_target,System.Predicate<Transform> p_callback) {
		if(p_target) if(!p_callback(p_target)) return;
		for(int i=0;i<p_target.childCount;i++) { OnTraverseStep(p_target.GetChild(i),p_callback); }
	}

	// Fetches all scene Controllers.
	//public D901Controller[] GetAllControllers() { /* ... */ }
}
