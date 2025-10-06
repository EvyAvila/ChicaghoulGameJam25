using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum item { Wealth, Poor, None }

public class ItemIdentifier : MonoBehaviour, IItem
{
    [SerializeField] private item ItemWorth;
    public item itemWorth { get { return ItemWorth; } set { ItemWorth = value; } }

    
    [SerializeField] private string ItemName;
    public string itemName { get { return ItemName; } set { ItemName = value; } }

   

   
}

public interface IItem
{
    public item itemWorth { get; set; }
    public string itemName { get; set; }

}
