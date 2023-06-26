using System;
using UnityEngine;

public class TilePurchaseManager : MonoBehaviour
{
    public static TilePurchaseManager Instance { get; private set; }

    public event EventHandler<OnTilePurchasedEventArgs> OnTilePurchased;

    public class OnTilePurchasedEventArgs : EventArgs {
        public HexTile hexTile;
    }
    private int playerMoney = 11000;
    private int hexTileCost;
    private int spentAmount;

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
        hexTileCost = e.hexTile.GetTileCost();
        spentAmount = e.hexTile.GetSpentAmount();
        if (playerMoney > 0 && hexTileCost > spentAmount) {
            playerMoney--;
            spentAmount++;
        }
        e.hexTile.SetSpentAmount(spentAmount);
        //Debug.Log("The player has " + playerMoney + " money");
        //Debug.Log("Player has spent: " + e.hexTile.GetSpentAmount());
        if (spentAmount == hexTileCost) {
            //Debug.Log("Tile has been purchased which costs " + e.hexTile.GetTileCost());
            OnTilePurchased?.Invoke(this, new OnTilePurchasedEventArgs {
                hexTile = e.hexTile
            });
        }
    }

}
