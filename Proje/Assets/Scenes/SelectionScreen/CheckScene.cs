using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class CheckScene : MonoBehaviour
{
    // Krallık bilgisini burada tutuyoruz
    public static string selectedKingdom = "";

    // Bu fonksiyon Continue butonuna basıldığında çalışacak
    public void OnContinueButtonClicked()
    {
        // GetVariableFromHere scriptinden currentSpriteNum değerini al
        int currentSpriteNumber = GetVariableFromHere.currentSpriteNum;

        // Krallık ismini al
        selectedKingdom = GetKingdomName(currentSpriteNumber);

       

        // Room sahnesine yönlendir
        SceneManager.LoadScene("room");
    }

    // CurrentSpriteNumber'a göre krallığı al
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
}
