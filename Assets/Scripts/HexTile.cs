using System;
using UnityEngine;

public class HexTile : MonoBehaviour
{

    public event EventHandler OnPurchaseAmountChange;
    public event EventHandler<OnHexTileEnableEventArgs> OnHexTileEnable;

    public class OnHexTileEnableEventArgs : EventArgs {
        public HexTile hexTile;
    };
    [SerializeField] private TilePurchaseParticleSystem tileParticleSystem;
    [SerializeField] private Transform hexTileForPurchase;
    [SerializeField] private int tileCost = 300;
    private int spentAmount = 0;
    
    private void Start() {
       TilePurchaseManager.Instance.OnTilePurchased += TilePurchaseManager_OnTilePurchased;
    }

    private void TilePurchaseManager_OnTilePurchased(object sender, TilePurchaseManager.OnTilePurchasedEventArgs e) {
        Hide(e.hexTile);
        Show(e.hexTile);
    }

    public int GetTileCost() {
        return tileCost;
    }

    public int GetSpentAmount() {
        return spentAmount;
    }

    public Transform GetHexTileForPurchase() {
        return hexTileForPurchase;
    }

    public TilePurchaseParticleSystem GetHexTileParticleSystem() {
        return tileParticleSystem;
    }

    public void SetSpentAmount(int spentAmount) {
        this.spentAmount = spentAmount;
        OnPurchaseAmountChange?.Invoke(this, EventArgs.Empty);
    }

    private void Hide(HexTile hexTile) {
        hexTile.gameObject.SetActive(false);
    }

    private void Show(HexTile hexTile) {
        hexTile.hexTileForPurchase.gameObject.SetActive(true);
        OnHexTileEnable?.Invoke(this, new OnHexTileEnableEventArgs {
            hexTile = hexTile
        });
    }


}

