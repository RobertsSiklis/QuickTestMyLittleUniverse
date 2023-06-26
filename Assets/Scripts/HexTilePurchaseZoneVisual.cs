using UnityEngine;

public class HexTileVisualPurchaseZoneVisual : MonoBehaviour
{
    [SerializeField] private HexTile hexTile;
    [SerializeField] private Transform squareLine;

    private void Start() {
        TilePurchaseManager.Instance.OnTilePurchased += TilePurchaseManager_OnTilePurchased;
    }

    private void TilePurchaseManager_OnTilePurchased(object sender, TilePurchaseManager.OnTilePurchasedEventArgs e) {
        if (hexTile == e.hexTile) {
            Hide();
        }
    }

    private void Hide() {
        squareLine.gameObject.SetActive(false);
    }
}
