using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab; // Karakter prefab'ınız

    void Start()
    {
        // Oyun başladığında oyuncu spawn etmek
        if (PhotonNetwork.IsConnectedAndReady)
        {
            SpawnPlayer();
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        
        // Yeni oyuncu katıldığında oyuncu spawn etmek
        if (PhotonNetwork.IsMasterClient)  // Sadece master client karakteri spawn edebilir
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        // Oyuncuyu sahneye yerleştirme (spawn)
        if (PhotonNetwork.IsConnectedAndReady)
        {
            // İlgili prefab'ı sahneye instantiate et
            PhotonNetwork.Instantiate("Prefabs/PlayerPrefab", new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
