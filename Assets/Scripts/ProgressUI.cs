using TMPro;
using UnityEngine;

public class ProgressUI : MonoBehaviour
{
    [SerializeField] private HexTile hexTile;
    [SerializeField] private TextMeshProUGUI spentAmountText;
    [SerializeField] private TextMeshProUGUI startCostText;

    private void Start() {
        hexTile.OnPurchaseAmountChange += HexTile_OnPurchaseAmountChange;
        TilePurchaseManager.Instance.OnTilePurchased += TilePurchaseManager_OnTilePurchased;
        if (hexTile.GetTileCost() < 1000) {
            startCostText.text = "/" + hexTile.GetTileCost().ToString();
        } else {
            startCostText.text = "/" + (hexTile.GetTileCost() / 1000f).ToString("0.#") + "K";
        }
        
    }

    private void TilePurchaseManager_OnTilePurchased(object sender, TilePurchaseManager.OnTilePurchasedEventArgs e) {
        if (e.hexTile == hexTile) {
            Hide();
        }
    }

    private void HexTile_OnPurchaseAmountChange(object sender, System.EventArgs e) {
        if (hexTile.GetSpentAmount() < 1000) {
            spentAmountText.text = hexTile.GetSpentAmount().ToString();
        }
        else {
            spentAmountText.text = (hexTile.GetSpentAmount() / 1000f).ToString("0.#") + "K";
        }
        
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
