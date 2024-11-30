using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class BackButtonHandler : MonoBehaviour
{
    public void GoToMenuScene()
    {
        // Önce lobiden çık
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
            Debug.Log("Lobiden çıkıldı.");
        }

        // Menü sahnesine dön
        SceneManager.LoadScene(0);
    }

    void OnEnable()
    {
        // Bu ekrana geri gelindiğinde tekrar lobiye katıl
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
            Debug.Log("Lobiye tekrar katıldı.");
        }
    }
}
