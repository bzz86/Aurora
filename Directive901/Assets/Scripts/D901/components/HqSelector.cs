using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HqSelector : D901BaseObject {

	[SerializeField] InputField ifDeckName;
	[SerializeField] Card hqCard;

	public void Init(string protoId,
		Sprite art, 
		string title, 
		string description)
	{
		Debug.Log ("HqSelector init: protoId = " + protoId + ", art=" + art + ", title =" + title + ", description =" + description);
		hqCard.Init (protoId,
			art,
			title,
			description
		);
		
	}

}
