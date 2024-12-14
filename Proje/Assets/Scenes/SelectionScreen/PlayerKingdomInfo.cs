using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class PlayerKingdomInfo : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject continueButton; // Continue butonunu buradan referans alıyoruz
    [SerializeField] int currentSpriteNumber;  // Book scriptinden alacağımız currentSpriteNumber

    // Odadaki oyuncuların bilgilerini çekip debug olarak yazdırmak için bu fonksiyonu kullanacağız
    void Start()
    {
        // Oda başladığında oyuncuların krallık bilgilerini çekelim
        DebugPlayerKingdoms();
        // Sayfada hangi krallığın seçildiğini kontrol et
        CheckSelectedKingdom();
    }

    // Oda başladığında tüm oyuncuların krallıklarını debug olarak yazdır
    void DebugPlayerKingdoms()
    {
        List<string> playerKingdoms = new List<string>();  // Bu liste odadaki oyuncuların krallıklarını tutacak

        // Oyuncuların mevcut seçimlerini kontrol edelim
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            // Burada oyuncu bilgilerini alıyoruz ve listede var mı diye kontrol ediyoruz
            if (player.CustomProperties.ContainsKey("selectedKingdom"))
            {
                string playerKingdom = player.CustomProperties["selectedKingdom"].ToString();
                playerKingdoms.Add(playerKingdom);
                Debug.Log("Player: " + player.NickName + " - Selected Kingdom: " + playerKingdom);
            }
        }

        // O odadaki tüm oyuncuların krallıklarını yazdırdık
        Debug.Log("All Player Kingdoms: " + string.Join(", ", playerKingdoms));
    }

    // Krallık adı sayfa numarasına göre alınacak
    string GetKingdomName(int spriteNum)
    {
        switch (spriteNum)
        {
            case 2: return "Akhadzria";
            case 3: return "Alfgard";
            case 4: return "Arianopol";
            case 5: return "Dhamuron";
            case 6: return "Lexion";
            case 7: return "Zephyrion";
            default: return "Alfgard"; // Varsayılan olarak Alfgard
        }
    }

    // Krallık seçilip seçilmediğini kontrol et
    void CheckSelectedKingdom()
    {
        string kingdomName = GetKingdomName(currentSpriteNumber); // currentSpriteNumber'a göre krallığı al
        bool kingdomSelected = false;

        // O odadaki oyuncuları kontrol et
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.ContainsKey("selectedKingdom"))
            {
                string playerKingdom = player.CustomProperties["selectedKingdom"].ToString();
                // Eğer oyuncu bu krallığı seçmişse, Continue butonunu gizle
                if (playerKingdom == kingdomName)
                {
                    kingdomSelected = true;
                    break;
                }
            }
        }

        // Eğer krallık seçilmişse, Continue butonunu gizle
        if (kingdomSelected)
        {
            continueButton.SetActive(false);
        }
        else
        {
            continueButton.SetActive(true);
        }
    }
}
