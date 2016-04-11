﻿using System;

namespace InventoryManager
{
    public class Item
    {
        public string Label { get; set; }
        public DateTime Expiration { get; set; }
        public string Description { get; set; }
        public int Sku { get; set; }
    }
}