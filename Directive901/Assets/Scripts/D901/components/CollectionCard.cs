using UnityEngine;
using System.Collections;

public class CollectionCard : Card {

	public override void OnClickDo(){
		Debug.Log("click on collection card " + this.ProtoId);
	}
}
