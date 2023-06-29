using UnityEngine;

public class TilePurchaseParticleSystem : MonoBehaviour {
    private enum State {
        Stepped,
        Unstepped,
    }

    [SerializeField] private HexTile hexTile;
    private ParticleSystem tileParticleSystem;
    private State state;
    private void Awake() {
        tileParticleSystem = GetComponent<ParticleSystem>();
    }
    private void Start() {
        state = State.Unstepped;
        tileParticleSystem.Stop();
        Player.Instance.OnPurchasibleTileZoneUnstepped += Player_OnPurchasibleTileZoneUnstepped;
        Player.Instance.OnPurchasableTileZoneStepped += Player_OnPurchasableTileZoneStepped;
        hexTile.OnPurchaseAmountChange += HexTile_OnPurchaseAmountChange;
    }

    private void HexTile_OnPurchaseAmountChange(object sender, System.EventArgs e) {
        if (hexTile.GetSpentAmount(HexTile.CurrecnyType.Wood) == hexTile.GetTileCost(HexTile.CurrecnyType.Wood)){
            gameObject.SetActive(false);
        }
    }

    private void Player_OnPurchasibleTileZoneUnstepped(object sender, System.EventArgs e) {
        if (state == State.Stepped) {
            tileParticleSystem.Stop();
            state = State.Unstepped;
        }
    }

    private void Player_OnPurchasableTileZoneStepped(object sender, Player.OnPurchasableTileZoneSteppedEventArgs e) {
        if (state == State.Unstepped) {
            e.hexTile.GetHexTileParticleSystem().tileParticleSystem.Play();
            state = State.Stepped;
        }
    }
}
