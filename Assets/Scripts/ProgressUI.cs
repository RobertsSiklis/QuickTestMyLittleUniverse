using TMPro;
using UnityEngine;

public class ProgressUI : MonoBehaviour
{
    [SerializeField] private HexTile hexTile;
    [SerializeField] private TextMeshProUGUI woodSpentAmountText;
    [SerializeField] private TextMeshProUGUI stoneSpentAmountText;
    [SerializeField] private TextMeshProUGUI startWoodCostText;
    [SerializeField] private TextMeshProUGUI startStoneCostText;
    [SerializeField] private Transform woodUI;
    [SerializeField] private Transform stoneUI;
    private void Start() {
        hexTile.OnPurchaseAmountChange += HexTile_OnPurchaseAmountChange;
        hexTile.OnPurchaseAmountChange += HexTile_OnPurchaseAmountMax;
        TilePurchaseManager.Instance.OnTilePurchased += TilePurchaseManager_OnTilePurchased;
        if (hexTile.GetTileCost(HexTile.CurrecnyType.Wood) > 0){
            if (hexTile.GetTileCost(HexTile.CurrecnyType.Wood) < 1000) {
                startWoodCostText.text = "/" + hexTile.GetTileCost(HexTile.CurrecnyType.Wood).ToString();
            } else {
                startWoodCostText.text = "/" + (hexTile.GetTileCost(HexTile.CurrecnyType.Wood) / 1000f).ToString("0.#") + "K";
            }
        } else {
            woodUI.gameObject.SetActive(false);
        }


        if (hexTile.GetTileCost(HexTile.CurrecnyType.Stone) > 0){
            if (hexTile.GetTileCost(HexTile.CurrecnyType.Stone) < 1000) {
                startStoneCostText.text = "/" + hexTile.GetTileCost(HexTile.CurrecnyType.Stone).ToString();
            } else {
                startStoneCostText.text = "/" + (hexTile.GetTileCost(HexTile.CurrecnyType.Stone) / 1000f).ToString("0.#") + "K";
            }
        } else {
            stoneUI.gameObject.gameObject.SetActive(false);
        }
    }

    private void TilePurchaseManager_OnTilePurchased(object sender, TilePurchaseManager.OnTilePurchasedEventArgs e) {
        if (e.hexTile == hexTile) {
            Hide();
        }
    }

    private void HexTile_OnPurchaseAmountMax(object sender, System.EventArgs e) {
        if (hexTile.GetSpentAmount(HexTile.CurrecnyType.Wood) >= hexTile.GetTileCost(HexTile.CurrecnyType.Wood)) {
            woodUI.gameObject.SetActive(false);
        }

        if (hexTile.GetSpentAmount(HexTile.CurrecnyType.Stone) >= hexTile.GetTileCost(HexTile.CurrecnyType.Stone)) {
            stoneUI.gameObject.SetActive(false);
        }
    }

    private void HexTile_OnPurchaseAmountChange(object sender, System.EventArgs e) {
        if (hexTile.GetSpentAmount(HexTile.CurrecnyType.Wood) < 1000) {
            woodSpentAmountText.text = hexTile.GetSpentAmount(HexTile.CurrecnyType.Wood).ToString();
        } else {
            woodSpentAmountText.text = (hexTile.GetSpentAmount(HexTile.CurrecnyType.Wood) / 1000f).ToString("0.#") + "K";
        }

        if (hexTile.GetSpentAmount(HexTile.CurrecnyType.Stone) < 1000) {
            stoneSpentAmountText.text = hexTile.GetSpentAmount(HexTile.CurrecnyType.Stone).ToString();
        } else {
            stoneSpentAmountText.text = (hexTile.GetSpentAmount(HexTile.CurrecnyType.Stone) / 1000f).ToString("0.#") + "K";
        }
        
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
