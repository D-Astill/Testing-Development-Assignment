using UnityEngine;

public static class ItemGen
{
    public static Item CreateItem(int itemID)
    {
        Item temp = new Item();
        #region Variables 
        string name = "";
        int value = 0;
        string description = "";
        string icon = "";
        string mesh = "";
        ItemType type = ItemType.Food;
        #endregion
        #region Switch Item Data
        switch(itemID)
        {
            #region Food 0-99
            case 0:
                name = "Apple";
                value = 5;
                description = "Munchies and Crunchies";
                icon = "Apple";
                mesh = "Apple";
                type = ItemType.Food;
                break;
            case 1:
                name = "Cheese Wheel";
                value = 7;
                description = "Golden Goodness";
                icon = "CheeseWheel";
                mesh = "CheeseWheel";
                type = ItemType.Food;
                break;
            #endregion
            #region Weapon 100-199
            #endregion
            #region Apparel 200-299
            #endregion
            #region Crafting 300-399
            #endregion
            #region Quest 400-499
            #endregion
            #region Money 500-599
            #endregion
            #region Ingredients 600-699
            #endregion
            #region Potions 700-799
            #endregion
            #region Scrolls 800-899
            #endregion
            default:
                itemID = 0;
                name = "Apple";
                value = 5;
                description = "Munchies and Crunchies";
                icon = "Apple";
                mesh = "Apple";
                type = ItemType.Food;
                break;
        }
        #endregion
        #region Temp Connect
        temp.ID = itemID;
        temp.Name = name;
        temp.Value = value;
        temp.Description = description;
        temp.Icon = Resources.Load("Icons/" + icon) as Texture2D;
        temp.Mesh = mesh;
        temp.Type = type;
        #endregion
        return temp;
    }
}
