using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public void UseItem()
    {
        if(statToChange == StatToChange.health)
        {
            GameObject.Find("HealthManeger").GetComponent<healthAmount>()
        }
    }






    public enum StatToChange
    {
        None,
        health,
        Scurvy
    };
    

}
