using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class RegionClickHandler : MonoBehaviour, IPointerClickHandler
{
    // Þehirler için kullanýlacak sprite'lar
    public Sprite LexionLinePNG;
    public Sprite AlfgardLinePNG;
    public Sprite ZephrionLinePNG;
    public Sprite ArianopolLinePNG;
    public Sprite DhamuronLinePNG;
    public Sprite AkhadzriaPNG;

    public Image FlagImage;
    public Image WarIcon;
    public Image ObservationImage;//Gözetleme Iconu
    public Sprite warSprite;
    public Sprite observationSprite;
    public TMP_Text owner;//sahibi
    public TMP_Text kingdom;//krallýk
    public TMP_Text civilization;//medeniyet
    public TMP_Text numberOfSoldier;//Asker Sayýsý
    int selectedKingdom = GetVariableFromHere.currentSpriteNum;

    // Orijinal sprite'larý saklamak için bir deðiþken
    private Sprite originalSprite;

    // Son týklanan bölgenin referansý
    private static Image lastClickedRegion;

    void Start()
    {
        createDefaultPanel();
        Renderer renderer = GetComponent<Renderer>();
    }

    // Týklama olayýný dinleyen metod
    public void OnPointerClick(PointerEventData eventData)
    {
        // Mevcut Image bileþenini alýyoruz
        Image imageComponent = GetComponent<Image>();

        // Eðer bir önceki týklanan bölge varsa, eski haline döndür
        if (lastClickedRegion != null && lastClickedRegion != imageComponent)
        {
            lastClickedRegion.sprite = lastClickedRegion.GetComponent<RegionClickHandler>().originalSprite;
        }

        // Orijinal sprite'ý saklýyoruz (sadece ilk týklamada)
        if (originalSprite == null)
        {
            originalSprite = imageComponent.sprite;
        }

        // Týklanan objenin ismine göre görseli deðiþtiriyoruz
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
                    kingdom.text = "Krallýk:Lexion";
                    civilization.text = "Medeniyet:Elf";
                    numberOfSoldier.text = "Asker Sayýsý: " + Kingdom.Kingdoms[4].SoldierAmount.ToString();

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
                    kingdom.text = "Krallýk:Alfgard";
                    civilization.text = "Medeniyet:Büyücü";
                    numberOfSoldier.text = "Asker Sayýsý: " + Kingdom.Kingdoms[1].SoldierAmount.ToString();

                    break;
                case "Zephrion":
                    imageComponent.sprite = ZephrionLinePNG;

                    FlagImage.sprite = Kingdom.Kingdoms[5].Flag;
                    WarIcon.enabled = true;
                    ObservationImage.enabled = true;
                    WarIcon.sprite = warSprite;
                    ObservationImage.sprite = observationSprite;
                    owner.text = "Sahibi: Bilgisayar";
                    kingdom.text = "Krallýk:Zephyrion";
                    civilization.text = "Medeniyet:Ölüler";
                    numberOfSoldier.text = "Asker Sayýsý: " + Kingdom.Kingdoms[5].SoldierAmount.ToString();

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
                    kingdom.text = "Krallýk:Arianopol";
                    civilization.text = "Medeniyet:Ýnsan";
                    numberOfSoldier.text = "Asker Sayýsý: " + Kingdom.Kingdoms[0].SoldierAmount.ToString();
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
                    kingdom.text = "Krallýk:Dhamuron";
                    civilization.text = "Medeniyet:Cüce";
                    numberOfSoldier.text = "Asker Sayýsý: " + Kingdom.Kingdoms[3].SoldierAmount.ToString();

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
                    kingdom.text = "Krallýk: Akhadzria";
                    civilization.text = "Medeniyet: Ork";
                    numberOfSoldier.text = "Asker Sayýsý: " + Kingdom.Kingdoms[2].SoldierAmount.ToString();

                    break;
                default:
                    Debug.Log("Týklanan bölge için bir görsel tanýmlanmadý.");
                    break;
            }

            // Þu an týklanan bölgeyi "lastClickedRegion" olarak sakla
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
            kingdom.text = "Krallýk: Akhadzria";
            civilization.text = "Medeniyet: Ork";
            numberOfSoldier.text = "Asker Sayýsý: " + Kingdom.Kingdoms[2].SoldierAmount.ToString();
        }
        else if (selectedKingdom == 3)
        {
            FlagImage.sprite = Kingdom.Kingdoms[1].Flag;
            WarIcon.enabled = false;
            owner.text = "Sahibi: Player";
            kingdom.text = "Krallýk:Alfgard";
            civilization.text = "Medeniyet:Büyücü";
            numberOfSoldier.text = "Asker Sayýsý: " + Kingdom.Kingdoms[1].SoldierAmount.ToString();
        }
        else if (selectedKingdom == 4)
        {
            FlagImage.sprite = Kingdom.Kingdoms[0].Flag;
            WarIcon.enabled = false;
            owner.text = "Sahibi: Player";
            kingdom.text = "Krallýk:Arianopol";
            civilization.text = "Medeniyet:Ýnsan";
            numberOfSoldier.text = "Asker Sayýsý: " + Kingdom.Kingdoms[0].SoldierAmount.ToString();
        }
        else if (selectedKingdom == 5)
        {
            FlagImage.sprite = Kingdom.Kingdoms[3].Flag;
            WarIcon.enabled = false;
            owner.text = "Sahibi: Player";
            kingdom.text = "Krallýk:Dhamuron";
            civilization.text = "Medeniyet:Cüce";
            numberOfSoldier.text = "Asker Sayýsý: " + Kingdom.Kingdoms[3].SoldierAmount.ToString();
        }
        else
        {
            FlagImage.sprite = Kingdom.Kingdoms[4].Flag;
            WarIcon.enabled = false;
            owner.text = "Sahibi: Player";
            kingdom.text = "Krallýk:Lexion";
            civilization.text = "Medeniyet:Elf";
            numberOfSoldier.text = "Asker Sayýsý: " + Kingdom.Kingdoms[4].SoldierAmount.ToString();
        }
    }

}
