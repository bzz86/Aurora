using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestView : D901BaseObject {
	//private TestModel testModel = app.model.testModel;
	//private string testText;

	public Text testText;

	public void refreshView(){
		this.testText.text = app.model.testModel.TestMessage;
	}

}
