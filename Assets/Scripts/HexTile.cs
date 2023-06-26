using System;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;


public class HexTile : MonoBehaviour
{
    public enum CurrecnyType {
        None,
        Wood,
        Stone,
    }
    public event EventHandler OnPurchaseAmountChange;
    public event EventHandler<OnHexTileEnableEventArgs> OnHexTileEnable;

    public class OnHexTileEnableEventArgs : EventArgs {
        public HexTile hexTile;
    };
    [SerializeField] private TilePurchaseParticleSystem tileParticleSystem;
    [SerializeField] private Transform hexTileForPurchase;
    [SerializeField] private int woodCost = 500;
    [SerializeField] private int stoneCost = 550;
    [SerializeField] private CurrecnyType wood = CurrecnyType.None;
    [SerializeField] private CurrecnyType stone = CurrecnyType.None;
    private int woodSpentAmount = 0;
    private int stoneSpentAmount = 0;
    
    private void Start() {
       if (wood != CurrecnyType.Wood) {
            woodCost = 0;
       }
       if (stone != CurrecnyType.Stone) {
            stoneCost = 0;
        }
       TilePurchaseManager.Instance.OnTilePurchased += TilePurchaseManager_OnTilePurchased;
    }

    private void TilePurchaseManager_OnTilePurchased(object sender, TilePurchaseManager.OnTilePurchasedEventArgs e) {
        Hide(e.hexTile);
        Show(e.hexTile);
    }

    public int GetTileCost(CurrecnyType currecnyType) {
        if (currecnyType == wood) {
            return woodCost;
        }
        if (currecnyType == stone) {
            return stoneCost;
        }
        return 0;
    }

    public int GetSpentAmount(CurrecnyType currencyType) {
        if (currencyType == CurrecnyType.Wood) {
            return woodSpentAmount;
        }
        if (currencyType == CurrecnyType.Stone) {
            return stoneSpentAmount;
        }
        return 0;
    }

    public Transform GetHexTileForPurchase() {
        return hexTileForPurchase;
    }

    public TilePurchaseParticleSystem GetHexTileParticleSystem() {
        return tileParticleSystem;
    }

    public void SetSpentAmount(int spentAmount, CurrecnyType currecyType) {
        if (currecyType == CurrecnyType.Wood) {
            woodSpentAmount = spentAmount;
        }
        if (currecyType == CurrecnyType.Stone) {
            stoneSpentAmount = spentAmount;
        }
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

