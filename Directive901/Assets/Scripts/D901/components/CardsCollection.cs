using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardsCollection : D901BaseObject {

	[SerializeField] CollectionCard cardPrefab;
	[SerializeField] Transform collectionContainer;


	public void initCollection(List<CardItem> filteredCollection){
		//this.filteredCollection = applyFilters(this.collection);
		ViewUtils.removeChildrenFromTransform (collectionContainer);
		foreach(CardItem item in filteredCollection){
			CollectionCard cardInstance = Instantiate (cardPrefab);
			cardInstance.Init(item.ProtoID, null, item.ProtoID, item.ProtoID, item.Count);
			cardInstance.transform.SetParent(collectionContainer, false);
		}
	}
}
