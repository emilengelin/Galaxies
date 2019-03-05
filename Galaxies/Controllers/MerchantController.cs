﻿using Galaxies.Economy;
using Galaxies.Items;

namespace Galaxies.Controllers
{

    static class MerchantController
    {

        public static Trader Merchant { get; private set; }

        public static void CreateMerchant()
        {
            Merchant = new Trader(null, new Balance());
            Merchant.Inventory = new Inventory(Merchant);
        }

        public static void TradeItem(Item tradedItem, Trader buyer, Trader seller)
        {
            if (buyer.Balance.Extract(tradedItem.Data.GalacticGoldValue))
            {
                tradedItem.ChangeInventory(buyer.Inventory);

                seller.Balance.Deposit(tradedItem.Data.GalacticGoldValue);
            }
        }

    }

}