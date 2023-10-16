﻿using Blackjack.biz.Cards;
using static Blackjack.biz.Constants;

namespace Blackjack.biz.Players
{
    public class Player
    {
        public Player()
        {
            Name = "Player";
            Hand = new List<Card>();
            Result = Result.InProgress;
            Bet = 0;
            ChipAmount = 0;
        }

        public Player(string name)
        {
            Name = name;
            Hand = new List<Card>();
            Result = Result.InProgress;
            Bet = 0;
            ChipAmount = 0;
        }

        public Player(string name, int chipAmount)
        {
            Name = name;
            Hand = new List<Card>();
            Result = Result.InProgress;
            Bet = 0;
            ChipAmount = chipAmount;
        }

        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public Result Result { get; set; }
        public int Bet { get; set; }
        public int ChipAmount { get; set; }

        public string GetDisplayHandValue()
        {
            string handToDisplay = "";

            foreach(Card card in Hand)
            {
                handToDisplay += card.DisplayCard() + " ";
            }

            return handToDisplay;
        }

        public void ResetPlayer()
        {
            Hand.Clear();
            Result = Result.InProgress;
        }
    }
}
