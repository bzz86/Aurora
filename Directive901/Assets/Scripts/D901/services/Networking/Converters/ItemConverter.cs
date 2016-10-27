using System;
using Sun.DTO.Entities;
using System.Collections.Generic;
using System.Collections;

namespace Aurora.Networking.Converters
{
	public class ItemConverter
	{
		public ItemConverter ()
		{
		}

	
		public static List<DeckDTO.DeckCard> getDeckCardFromItem(CardItem[] items){
			List<DeckDTO.DeckCard> cardList = new List<DeckDTO.DeckCard>();
			foreach (CardItem item in items){
				cardList.Add (getDeckCardFromItem(item));
			}
			return cardList;
		}


		public static CardItem getItemFromDeckCard(DeckDTO.DeckCard card){
			return new CardItem (card.ProtoID, card.Count);
		}

		public static DeckDTO.DeckCard getDeckCardFromItem(CardItem item){
			DeckDTO.DeckCard card = new DeckDTO.DeckCard ();
			card.ProtoID = item.ProtoID;
			card.Count = item.Count;
			return card;
		}


	}
}

