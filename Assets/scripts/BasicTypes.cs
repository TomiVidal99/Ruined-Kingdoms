using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Here we describe the basic types of data that we handles across all the code
/// </summary>
public class BasicTypes : MonoBehaviour
{

  public static readonly List<string> CARD_QUALITIES = new List<string>()
  {
    "COMMONER", "NOBLE", "MONARCH", "DIVINE"
  };

  public static readonly List<string> CARD_ACTIONS = new List<string>()
  {
    "ATTACK", "BUILD", "MAGIC"
  };

  public static readonly List<string> CARD_RESOURCES = new List<string>()
  {
    "SPADES", "CRYSTALS", "BRICKS"
  };

  public static readonly List<string> TARGET_ENTITIES = new List<string>()
  {
    "TOWER", "FENCE", "RESOURCES"
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
