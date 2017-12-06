//in this script  you will only need using UnityEngine as we just need the script to connect to unity
using UnityEngine;
//this public class doent inherit from MonoBehaviour
//this script is also referenced by other scripts but never attached to anything
public class Item
{
    //basic variables for items that we need are 
    #region Private Variables
    //Identification Number
    private int _idNum;
    //Object Name
    private string _name;
    //Value of the Object
    private int _value;
    //Description of what the Object is
    private string _description;
    //Icon that displays when that Object is in an Inventory
    private Texture2D _icon;
    //Mesh of that object when it is equipt or in the world
    private string _mesh;
    //Enum ItemType which is the Type of object so we can classify them
    private ItemType _type;
    #endregion
    #region Constructors
    //A constructor is a bit of code that allows you to create objects from a class. You call the constructor by using the keyword new , followed by the name of the class, followed by any necessary parameters.
    //the Item needs Identification Number, Object Name, Icon and Type
    public void ItemConstructor(int itemId, string itemName, Texture2D itemIcon, ItemType itemType)
    {
        //here we connect the parameters with the item variables
        _idNum = itemId;
        _name = itemName;
        _icon = itemIcon;
        _type = itemType;
    }
    #endregion
    #region Public Variables
    //here we are creating the public versions or our private variables that we can reference and connect to when interacting with items
    //public Identification Number
    public int ID
    {
        //get the private Identification Number
        get { return _idNum; }
        //and set it to the value of our public Identification Number
        set { _idNum = value; }
    }
    //public Name 
    public string Name
    {
        get { return _name; }
        //get the private Name
        set { _name = value; }
        //and set it to the value of our public Name
    }
    //public Value 
    public int Value
    {
        get { return _value; }
        //get the private Value
        set { _value = value; }
        //and set it to the value of our public Value
    }
    //public Description 
    public string Description
    {
        get { return _description; }
        //get the private Description
        set { _description = value; }
        //and set it to the value of our public Description
    }
    //public Icon 
    public Texture2D Icon
    {
        get { return _icon; }
        //get the private Icon
        set { _icon = value; }
        //and set it to the value of our public Icon
    }
    //public Mesh 
    public string Mesh
    {
        get { return _mesh; }
        //get the private Mesh
        set { _mesh = value; }
        //and set it to the value of our public Mesh
    }
    //public Type 
    public ItemType Type
    {
        get { return _type; }
        //get the private Type
        set { _type = value; }
        //and set it to the value of our public Type
    }
    #endregion
}
#region Enums
//The Global Enum ItemType that we have created categories in
public enum ItemType
{
    Food,
    Weapon,
    Apparel,
    Crafting,
    Quest,
    Money,
    Ingredients,
    Potions,
    Scrolls
}
#endregion
