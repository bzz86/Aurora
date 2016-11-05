using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardSample : MonoBehaviour {

	public Text title;
	public Text description;

	public string ProtoId;
	public string Title;

	public void updateValues() {
		title.text = Title;
	}


	/*"id": "sv_ms1",
	"type": "vehicle",
	"country": "ussr",
	"subtype": "light",
	"power": 1,
	"toughness": 4,
	"cost": 2,
	"increase": 1,
	"level": 1,
	"credits": 0,
	"parent": null,
	"keywords": [ ]*/

}
