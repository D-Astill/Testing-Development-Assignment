using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region
    [Header("")]
    public bool showInv;

    public List<Item> inventory = new List<Item>();
    public int slotsX, slotsY;

    private Rect inventorySize;
    public bool dragging;
    public Item draggedItem;
    public int draggedFrom;
    public GameObject droppedItem;
    public int toolTipItem;
    public bool showToolTip;
    private Rect toolTipRect;
    //Related To inventory size x/y
    private float invW = (1920 * 0.75f) / 16;
    private float invH = (1080 * 0.75f) / 9;

    #endregion
    private Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.width);
        return r;
    }
    void Start()
    {
        inventorySize = new Rect(Screen.width - invW * 4 - 20, Screen.height - invH * 4.75f - 20, invW * 4, invH * 4.75f);
        for (int i = 0; i < slotsX * slotsY; i++)
        {
            inventory.Add(new Item());
        }
        AddItem(0);
        AddItem(0);
        AddItem(0);
        AddItem(0);
        AddItem(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            showInv = !showInv;
        }
    }
    void InventoryDrag(int windowID)
    {
        GUI.Box(new Rect(0, invH * 0.25f, invW * 4, invH * 0.5f), "Banner");
        GUI.Box(new Rect(0, invH * 4.25f, invW * 4, invH * 0.5f), "Gold/Exp");

        showToolTip = false;
        #region Nested For Loop
        Event e = Event.current;
        int i = 0;
        for (int y = 0; y < slotsY; y++)
        {
            for (int x = 0; x < slotsX; x++)
            {
                Rect SlotLocation = new Rect(invW * (0.57f / 2) + (invW * 0.685f) * x, invH * 0.875f + (invH * 0.685f) * y, invW * 0.5f, invH * 0.5f);
                GUI.Box(SlotLocation, ""/*,SlotStyles*/);
#if UNITY_EDITOR
                if (SlotLocation.Contains(e.mousePosition))
                {
                    if (e.button == 0 && e.type == EventType.mouseDown)
                    {
                        print("Left Clicked" + i);
                    }
                    if (e.button == 1 && e.type == EventType.mouseDown)
                    {
                        print("Right Clicked" + i);
                    }
                }
#endif
                #region Pickup Item 
                if (e.button == 0 && e.type == EventType.MouseDown && SlotLocation.Contains(e.mousePosition) && !dragging && inventory[i].Name != null && !Input.GetKeyDown(KeyCode.LeftShift))
                {
                    draggedItem = inventory[i];
                    inventory[i] = new Item();
                    dragging = true;
                    draggedFrom = i;
                    Debug.Log("" + draggedItem.Name);
                }
                #endregion
                if (e.button == 0 && e.type == EventType.mouseUp && SlotLocation.Contains(e.mousePosition) && dragging && inventory[i].Name != null)
                {
                    #region Swap Item
                    //bool returnItem = false;
                    Debug.Log("Swapped " + draggedItem.Name + " with " + inventory[i]);

                    inventory[draggedFrom] = inventory[i];
                    inventory[i] = draggedItem;
                    draggedItem = new Item();
                    dragging = false;
                    #endregion
                }
                else if (e.button == 0 && e.type == EventType.mouseUp && SlotLocation.Contains(e.mousePosition) && dragging && inventory[i].Name != null)
                {
                    #region Place Item
                    Debug.Log("Placed " + draggedItem.Name + " into " + i);
                    inventory[i] = draggedItem;
                    draggedItem = new Item();
                    dragging = false;
                    #endregion
                }
                //return item
                else if (e.button == 0 && e.type == EventType.mouseUp && i == ((slotsX * slotsY) - 1) && dragging)
                {
                    #region Return Item
                    Debug.Log("Return " + draggedItem.Name + " into " + draggedFrom);
                    inventory[draggedFrom] = draggedItem;
                    draggedItem = new Item();
                    dragging = false;
                    #endregion
                }
                #region Draw Item in the inventory
                if (inventory[i].Name != null)
                {
                    GUI.DrawTexture(SlotLocation, inventory[i].Icon);
                    #region Set Tooltip mouse over
                    if (SlotLocation.Contains(e.mousePosition) && !dragging && showInv)
                    {
                        toolTipItem = i;
                        showToolTip = true;

                    }
                    #endregion
                }
                #endregion
                i++;
            }
        }
        #endregion
        #region Drag Windows
        GUI.DragWindow(new Rect(0, 0, invW * 4, invH * 0.875f));//top dragging
        GUI.DragWindow(new Rect(0, invH * 0.875f, invW * 0.375f, invH * 3.25f));//left dragging
        GUI.DragWindow(new Rect(invW * 3.625f, invH * 0.875f, invW * 0.375f, invH * 3.25f));//right dragging
        GUI.DragWindow(new Rect(0, invH * 4.125f, invW * 4, invH * 0.625f));//bottom dragging
        #endregion
    }
    #region Add item Method
    public void AddItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Name == null)
            {
                inventory[i] = ItemGen.CreateItem(id);
                Debug.Log("Added Item: " + inventory[i].Name);
                return;
            }
        }
    }
    #endregion

    #region Drop Item
    public void DropItem(int id)
    {
        droppedItem = Resources.Load("Prefabs/" + ItemGen.CreateItem(id).Mesh) as GameObject;
        Instantiate(droppedItem, transform.position + transform.forward * 1, Quaternion.identity);
        return;
    }
    #endregion
    #region Tool Tip
    private string ToolTipText(int item)
    {
        string toolTipText =
            "Name: " + inventory[item].Name +
            "\nDescription: " + inventory[item].Description +
            "\nType: " + inventory[item].Type +
            "\nID: " + inventory[item].ID;

        return toolTipText;
    }
    #endregion
    void DrawToolTip(int windowID)
    {
        GUI.Box(new Rect(0, 0, invW * 2, invW * 3), ToolTipText(toolTipItem));
    }
    void OnGUI()
    {
        Event e = Event.current;
        #region Draw inventory if show inv is true
        if (showInv)
        {
            inventorySize = ClampToScreen(GUI.Window(1, (inventorySize), InventoryDrag, "My Inventory"));
        }
        #endregion
        #region Draw Tooltip
        if (showInv && showToolTip)
        {
            toolTipRect = new Rect(e.mousePosition.x + 0.01f, e.mousePosition.y, invW * 2, invH * 3);
            GUI.Window(15, toolTipRect, DrawToolTip, "");
        }
        #endregion
        #region DropItem if not inve and mouse up
        if (e.button == 0 && e.type == EventType.mouseUp && dragging)
        {
            DropItem(draggedItem.ID);
            Debug.Log("dropped " + draggedItem.Name);
            draggedItem = new Item();
            dragging = false;
        }
        #endregion
        #region incase inventory closes drop dragged item
        if (e.button == 0 && e.type == EventType.mouseUp && !showInv && dragging)
        {
            DropItem(draggedItem.ID);
            Debug.Log("dropped " + draggedItem.Name);
            draggedItem = new Item();
            dragging = false;
        }
        #endregion
        #region Draw Item On Mouse
        if (dragging)
        {
            if (draggedItem != null)
            {
                Rect mouseLocation = new Rect(e.mousePosition.x + 0.125f, e.mousePosition.y + 0.125f, invW * 0.5f, invH * 0.5f);

                GUI.Window(2, mouseLocation, DrawItem, "");
            }
        }
        #endregion
    }
    void DrawItem(int windowID)
    {
        GUI.DrawTexture(new Rect(0, 0, invW * 0.5f, invH * 0.5f), draggedItem.Icon);
    }
}
