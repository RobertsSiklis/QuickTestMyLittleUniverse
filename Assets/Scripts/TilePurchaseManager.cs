using System;
using System.Collections.Generic;
using UnityEngine;

public class TilePurchaseManager : MonoBehaviour
{
    public static TilePurchaseManager Instance { get; private set; }

    public event EventHandler<OnTilePurchasedEventArgs> OnTilePurchased;

    public class OnTilePurchasedEventArgs : EventArgs {
        public HexTile hexTile;
    }
    private int playerWoodResourceAmount = 5000;
    private int playerStoneResourceAmount = 7000;
    private int woodResourceHexTileCost;
    private int stoneResourceHexTileCost;
    private int woodSpentAmount;
    private int stoneSpentAmount;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start() {
        Player.Instance.OnPurchasableTileZoneStepped += Player_OnPurchasableTileZoneStepped;
    }

    private void Player_OnPurchasableTileZoneStepped(object sender, Player.OnPurchasableTileZoneSteppedEventArgs e) {
        //Get Tile Cost
        woodResourceHexTileCost = e.hexTile.GetTileCost(HexTile.CurrecnyType.Wood);
        stoneResourceHexTileCost = e.hexTile.GetTileCost(HexTile.CurrecnyType.Stone);
        //Get How much spent on tile purhcase
        woodSpentAmount = e.hexTile.GetSpentAmount(HexTile.CurrecnyType.Wood);
        stoneSpentAmount = e.hexTile.GetSpentAmount(HexTile.CurrecnyType.Stone);

        if (playerWoodResourceAmount > 0 && woodResourceHexTileCost > woodSpentAmount) {
            playerWoodResourceAmount--;
            woodSpentAmount++;
        }
        if (playerStoneResourceAmount > 0 && stoneResourceHexTileCost > stoneSpentAmount) {
            playerStoneResourceAmount--;
            stoneSpentAmount++;
        }
        e.hexTile.SetSpentAmount(woodSpentAmount, HexTile.CurrecnyType.Wood);
        e.hexTile.SetSpentAmount(stoneSpentAmount, HexTile.CurrecnyType.Stone);
        Debug.Log("The player has " + woodSpentAmount + " " + stoneSpentAmount + " money");
        Debug.Log("Player has spent: " + e.hexTile.GetSpentAmount(HexTile.CurrecnyType.Wood) + " " + e.hexTile.GetSpentAmount(HexTile.CurrecnyType.Stone));
        if (woodSpentAmount == woodResourceHexTileCost && stoneSpentAmount == stoneResourceHexTileCost) {
            Debug.Log("Tile has been purchased which costs " + e.hexTile.GetTileCost(HexTile.CurrecnyType.Wood) + " " + e.hexTile.GetTileCost(HexTile.CurrecnyType.Wood));
            OnTilePurchased?.Invoke(this, new OnTilePurchasedEventArgs {
                hexTile = e.hexTile
            });
        }
    }

}
