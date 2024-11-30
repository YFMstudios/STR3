using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GeriButonu : MonoBehaviourPunCallbacks
{
    public void GeriButonunaBasildi()
    {
        if (PhotonNetwork.InRoom)
        {
            // Oyuncu odadaysa, odadan çık
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            // Odada değilse doğrudan lobiye dön
            SceneManager.LoadScene(8);
        }
    }

    public override void OnLeftRoom()
    {
        // Odadan çıkıldıktan sonra lobi sahnesini yükle
        SceneManager.LoadScene(8);
    }
} 