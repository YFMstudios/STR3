using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon; // Photon'un Hashtable kullanımı için
using System.Collections.Generic;

public class PlayerInfoManager : MonoBehaviourPunCallbacks
{
    // UI Bileşenleri
    public TMP_InputField playerNameInputField; // Kullanıcı ismi için InputField
    public TMP_Dropdown kingdomDropdown;       // Krallık seçimi için Dropdown
    public GameObject inputPanel;             // Panelin referansı

    // Sabit krallıklar
    private readonly List<string> allKingdoms = new List<string> {
        "AKHADZRİA", "ALFGARD", "ARİANAPOL", "DHAMURON", "LEXİON", "ZEPHYRİON"
    };

    private void Start()
    {
        // Kullanılabilir krallıkları başlat ve Dropdown'u güncelle
        UpdateAvailableKingdoms();
        PopulateKingdomDropdown();
    }

    private void PopulateKingdomDropdown()
    {
        // Var olan seçenekleri temizle
        kingdomDropdown.ClearOptions();

        // Kullanılabilir krallıkları Dropdown'a ekle
        kingdomDropdown.AddOptions(allKingdoms);

        // Varsayılan bir seçim yap
        if (allKingdoms.Count > 0)
        {
            kingdomDropdown.value = 0;
            kingdomDropdown.RefreshShownValue();
        }
    }

    private void UpdateAvailableKingdoms()
    {
        // Mevcut oyuncuların seçtiği krallıkları al
        HashSet<string> selectedKingdoms = new HashSet<string>();

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.ContainsKey("Kingdom"))
            {
                string selectedKingdom = player.CustomProperties["Kingdom"].ToString();
                selectedKingdoms.Add(selectedKingdom);
            }
        }

        // Tüm krallık listesinden seçilenleri çıkar
        allKingdoms.RemoveAll(kingdom => selectedKingdoms.Contains(kingdom));
    }

    public void OnEnterButtonPressed()
    {
        // Kullanıcı ismini ve krallığını al
        string playerName = playerNameInputField.text;
        string selectedKingdom = kingdomDropdown.options[kingdomDropdown.value].text;

        // Boş isim kontrolü
        if (string.IsNullOrWhiteSpace(playerName))
        {
            Debug.LogWarning("Kullanıcı adı boş bırakılamaz!");
            return;
        }

        // Photon üzerinden bilgileri gönder
        PhotonNetwork.NickName = playerName; // Photon'da kullanıcı adı olarak ayarla

        // Kullanıcıya özel krallık bilgisini ekle
        var customProperties = new Hashtable();
        customProperties["Kingdom"] = selectedKingdom;
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);

        // Paneli kapat
        inputPanel.SetActive(false);

        // Debug için bilgileri yazdır
        Debug.Log($"Kullanıcı adı: {playerName}, Krallık: {selectedKingdom}");
    }

    // Photon Callback: Bir oyuncu odaya katıldığında
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (changedProps.ContainsKey("Kingdom"))
        {
            // Kullanılabilir krallıkları güncelle ve Dropdown'u yenile
            UpdateAvailableKingdoms();
            PopulateKingdomDropdown();
        }
    }

    // Photon Callback: Bir oyuncu odayı terk ettiğinde
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (otherPlayer.CustomProperties.ContainsKey("Kingdom"))
        {
            // Kullanılabilir krallıkları güncelle ve Dropdown'u yenile
            UpdateAvailableKingdoms();
            PopulateKingdomDropdown();
        }
    }
}
