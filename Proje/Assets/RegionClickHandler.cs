using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class RegionClickHandler : MonoBehaviour, IPointerClickHandler
{
    // �ehirler i�in kullan�lacak sprite'lar
    public Sprite LexionLinePNG;
    public Sprite AlfgardLinePNG;
    public Sprite ZephrionLinePNG;
    public Sprite ArianopolLinePNG;
    public Sprite DhamuronLinePNG;
    public Sprite AkhadzriaPNG;

    public Image FlagImage;
    public Image WarIcon;
    public Image ObservationImage;//G�zetleme Iconu
    public Sprite warSprite;
    public Sprite observationSprite;
    public TMP_Text owner;//sahibi
    public TMP_Text kingdom;//krall�k
    public TMP_Text civilization;//medeniyet
    public TMP_Text numberOfSoldier;//Asker Say�s�
    int selectedKingdom = GetVariableFromHere.currentSpriteNum;

    // Orijinal sprite'lar� saklamak i�in bir de�i�ken
    private Sprite originalSprite;

    // Son t�klanan b�lgenin referans�
    private static Image lastClickedRegion;

    void Start()
    {
        createDefaultPanel();
        Renderer renderer = GetComponent<Renderer>();
    }

    // T�klama olay�n� dinleyen metod
    public void OnPointerClick(PointerEventData eventData)
    {
        // Mevcut Image bile�enini al�yoruz
        Image imageComponent = GetComponent<Image>();

        // E�er bir �nceki t�klanan b�lge varsa, eski haline d�nd�r
        if (lastClickedRegion != null && lastClickedRegion != imageComponent)
        {
            lastClickedRegion.sprite = lastClickedRegion.GetComponent<RegionClickHandler>().originalSprite;
        }

        // Orijinal sprite'� sakl�yoruz (sadece ilk t�klamada)
        if (originalSprite == null)
        {
            originalSprite = imageComponent.sprite;
        }

        // T�klanan objenin ismine g�re g�rseli de�i�tiriyoruz
        if (imageComponent != null)
        {
            switch (gameObject.name)
            {
                case "Lexion":
                    imageComponent.sprite = LexionLinePNG;

                    FlagImage.sprite = Kingdom.Kingdoms[4].Flag;
                    if (isYourKingdoms("Lexion") == true)
                    {
                        WarIcon.enabled = false;
                        ObservationImage.enabled = false;
                    }
                    else
                    {
                        WarIcon.enabled = true;
                        ObservationImage.enabled = true;
                        WarIcon.sprite = warSprite;
                        ObservationImage.sprite = observationSprite;
                    }
                    owner.text = "Sahibi: " + findOwner("Lexion");
                    kingdom.text = "Krall�k:Lexion";
                    civilization.text = "Medeniyet:Elf";
                    numberOfSoldier.text = "Asker Say�s�: " + Kingdom.Kingdoms[4].SoldierAmount.ToString();

                    break;
                case "Alfgard":
                    imageComponent.sprite = AlfgardLinePNG;

                    FlagImage.sprite = Kingdom.Kingdoms[1].Flag;
                    if (isYourKingdoms("Alfgard") == true)
                    {
                        WarIcon.enabled = false;
                        ObservationImage.enabled = false;
                    }
                    else
                    {
                        WarIcon.enabled = true;
                        ObservationImage.enabled = true;
                        WarIcon.sprite = warSprite;
                        ObservationImage.sprite = observationSprite;
                    }
                    owner.text = "Sahibi: " + findOwner("Alfgard");
                    kingdom.text = "Krall�k:Alfgard";
                    civilization.text = "Medeniyet:B�y�c�";
                    numberOfSoldier.text = "Asker Say�s�: " + Kingdom.Kingdoms[1].SoldierAmount.ToString();

                    break;
                case "Zephrion":
                    imageComponent.sprite = ZephrionLinePNG;

                    FlagImage.sprite = Kingdom.Kingdoms[5].Flag;
                    WarIcon.enabled = true;
                    ObservationImage.enabled = true;
                    WarIcon.sprite = warSprite;
                    ObservationImage.sprite = observationSprite;
                    owner.text = "Sahibi: Bilgisayar";
                    kingdom.text = "Krall�k:Zephyrion";
                    civilization.text = "Medeniyet:�l�ler";
                    numberOfSoldier.text = "Asker Say�s�: " + Kingdom.Kingdoms[5].SoldierAmount.ToString();

                    break;
                case "Arianopol":
                    imageComponent.sprite = ArianopolLinePNG;

                    FlagImage.sprite = Kingdom.Kingdoms[0].Flag;
                    if (isYourKingdoms("Arianopol") == true)
                    {
                        WarIcon.enabled = false;
                        ObservationImage.enabled = false;
                    }
                    else
                    {
                        WarIcon.enabled = true;
                        ObservationImage.enabled = true;
                        WarIcon.sprite = warSprite;
                        ObservationImage.sprite = observationSprite;
                    }
                    owner.text = "Sahibi: " + findOwner("Arianopol");
                    kingdom.text = "Krall�k:Arianopol";
                    civilization.text = "Medeniyet:�nsan";
                    numberOfSoldier.text = "Asker Say�s�: " + Kingdom.Kingdoms[0].SoldierAmount.ToString();
                    break;
                case "Dhamuron":
                    imageComponent.sprite = DhamuronLinePNG;

                    FlagImage.sprite = Kingdom.Kingdoms[3].Flag;
                    if (isYourKingdoms("Dhamuron") == true)
                    {
                        WarIcon.enabled = false;
                        ObservationImage.enabled = false;
                    }
                    else
                    {
                        WarIcon.enabled = true;
                        ObservationImage.enabled = true;
                        WarIcon.sprite = warSprite;
                        ObservationImage.sprite = observationSprite;
                    }
                    owner.text = "Sahibi: " + findOwner("Dhamuron");
                    kingdom.text = "Krall�k:Dhamuron";
                    civilization.text = "Medeniyet:C�ce";
                    numberOfSoldier.text = "Asker Say�s�: " + Kingdom.Kingdoms[3].SoldierAmount.ToString();

                    break;
                case "Akhadzria":
                    imageComponent.sprite = AkhadzriaPNG;

                    FlagImage.sprite = Kingdom.Kingdoms[2].Flag;
                    if (isYourKingdoms("Akhadzria") == true)
                    {
                        WarIcon.enabled = false;
                        ObservationImage.enabled = false;
                    }
                    else
                    {
                        WarIcon.enabled = true;
                        ObservationImage.enabled = true;
                        WarIcon.sprite = warSprite;
                        ObservationImage.sprite = observationSprite;
                    }
                    owner.text = "Sahibi: " + findOwner("Akhadzria");
                    kingdom.text = "Krall�k: Akhadzria";
                    civilization.text = "Medeniyet: Ork";
                    numberOfSoldier.text = "Asker Say�s�: " + Kingdom.Kingdoms[2].SoldierAmount.ToString();

                    break;
                default:
                    Debug.Log("T�klanan b�lge i�in bir g�rsel tan�mlanmad�.");
                    break;
            }

            // �u an t�klanan b�lgeyi "lastClickedRegion" olarak sakla
            lastClickedRegion = imageComponent;
        }
    }


    public bool isYourKingdoms(string name)
    {
        foreach (Kingdom kingdom in Kingdom.Kingdoms)
        {
            if (kingdom.Owner == 1 && kingdom.Name == name)
            {
                return true;
            }
        }
        return false;
    }

    public string findOwner(string name)
    {


        foreach (Kingdom kingdom in Kingdom.Kingdoms)
        {
            if (kingdom.Owner == 1 && kingdom.Name == name)
            {
                return "Player";
            }
        }
        return "Bilgisayar";

    }


    public void createDefaultPanel()
    {
        if (selectedKingdom == 2)
        {
            FlagImage.sprite = Kingdom.Kingdoms[2].Flag;
            WarIcon.enabled = false;
            owner.text = "Sahibi: Player";
            kingdom.text = "Krall�k: Akhadzria";
            civilization.text = "Medeniyet: Ork";
            numberOfSoldier.text = "Asker Say�s�: " + Kingdom.Kingdoms[2].SoldierAmount.ToString();
        }
        else if (selectedKingdom == 3)
        {
            FlagImage.sprite = Kingdom.Kingdoms[1].Flag;
            WarIcon.enabled = false;
            owner.text = "Sahibi: Player";
            kingdom.text = "Krall�k:Alfgard";
            civilization.text = "Medeniyet:B�y�c�";
            numberOfSoldier.text = "Asker Say�s�: " + Kingdom.Kingdoms[1].SoldierAmount.ToString();
        }
        else if (selectedKingdom == 4)
        {
            FlagImage.sprite = Kingdom.Kingdoms[0].Flag;
            WarIcon.enabled = false;
            owner.text = "Sahibi: Player";
            kingdom.text = "Krall�k:Arianopol";
            civilization.text = "Medeniyet:�nsan";
            numberOfSoldier.text = "Asker Say�s�: " + Kingdom.Kingdoms[0].SoldierAmount.ToString();
        }
        else if (selectedKingdom == 5)
        {
            FlagImage.sprite = Kingdom.Kingdoms[3].Flag;
            WarIcon.enabled = false;
            owner.text = "Sahibi: Player";
            kingdom.text = "Krall�k:Dhamuron";
            civilization.text = "Medeniyet:C�ce";
            numberOfSoldier.text = "Asker Say�s�: " + Kingdom.Kingdoms[3].SoldierAmount.ToString();
        }
        else
        {
            FlagImage.sprite = Kingdom.Kingdoms[4].Flag;
            WarIcon.enabled = false;
            owner.text = "Sahibi: Player";
            kingdom.text = "Krall�k:Lexion";
            civilization.text = "Medeniyet:Elf";
            numberOfSoldier.text = "Asker Say�s�: " + Kingdom.Kingdoms[4].SoldierAmount.ToString();
        }
    }

}
