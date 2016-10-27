using UnityEngine;
using System.Collections;

public abstract class D901Controller : D901BaseObject {
	public abstract void OnServerNotification (string commandName, string data);
	public abstract void OnNotification (string p_event_path, Object p_target, params object[] p_data);
}
