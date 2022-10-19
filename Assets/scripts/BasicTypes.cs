using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Here we describe the basic types of data that we handles across all the code
/// </summary>
public class BasicTypes : MonoBehaviour
{

    public enum CARD_QUALITIES
    {
        COMMONER, NOBLE, MONARCH, DIVINE
    };


    public enum CARD_ACTIONS
    {
        ATTACK, BUILD, MAGIC
    };

    public enum CARD_RESOURCES
    {
        SPADES, CRYSTALS, BRICKS
    };

    public enum TARGET_ENTITIES
    {
        TOWER, FENCE, RESOURCES
    };

    public struct CardCost
    {
        int resources; // index refering to the CardResources List
        int magnitude; // how much does the card costs
    }

    public struct CardEffect
    {
        int action; // this is an index to the CardActions List
        int[] magnitude; // magnitude of the effect
        int[] target; // this is the target that will recieve the effect
    }

}
