﻿using System.Collections.Generic;

namespace cleanCore.UI
{
    public class Item
    {
        public int Bag { get; private set; }
        public int Slot { get; private set; }

        public Item(int bag, int slot)
        {
            Bag = bag;
            Slot = slot;
        }
    }

    public class Bag
    {
        public int Id { get; private set; }
        public int Slots { get; private set; }
        public int FreeSlots { get; private set; }

        public Bag(int id)
        {
            Id = id;
            Slots = int.Parse(WoWScript.Execute("GetContainerNumSlots(" + id + ")")[0]);
            FreeSlots = int.Parse(WoWScript.Execute("GetContainerNumFreeSlots(" + id + ")")[0]);
        }

        public void UseItem(int slot)
        {
            WoWScript.ExecuteNoResults("UseContainerItem(" + Id + ", " + slot + ")");
        }
    }

    public static class Inventory
    {

        public static int FreeSlots
        {
            get
            {
                int slots = 0;
                for (int i = 0; i < 5; i++)
                {
                    var ret = WoWScript.Execute("GetContainerNumFreeSlots(" + i + ")");
                    if (ret.Count > 0)
                        slots += int.Parse(ret[0]);
                }
                return slots;
            }
        }

        public static List<Bag> Bags
        {
            get
            {
                var ret = new List<Bag>(5);
                for (int i = 0; i < 5; i++)
                    ret.Add(new Bag(i));
                return ret;
            }
        }
    }
}