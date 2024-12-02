using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public GameObject progressBar;
    public GameObject healProgressBar;
    public GameObject buildWareHouseBar;
    public GameObject buildStonepitBar;
    public GameObject buildSawmillBar;
    public GameObject buildFarmBar;
    public GameObject buildBlacksmithBar;
    public GameObject buildLabBar;
    public GameObject buildBarracksBar;
    public GameObject buildHospitalBar;
    public GameObject upgradeCastleBar;


    public SliderController slider;
    public HastaneSliderController hastaneSlider;
    public float savasciCreationTime = 1.5f;
    public float okcuCreationTime = 2.5f;
    public float mizrakciCreationTime = 4.5f;
    private float totalUnitAmount;
    public float savasciHealTime = 1.5f;
    public float okcuHealTime = 2.5f;
    public float mizrakciHealTime = 4.5f;

    public bool isBarracksBuildActive = false;
    public bool isUnitCreationActive = false;
    public bool isHealActive = false;
    public bool isHospitalBuildActive = false;
    public Button createUnitButton;
    public Button healButton;

    public bool isLabBuildActive = false;
   
    public WareHousePanelController wareHousePanelController;
    public StonepitPanelController stonepitPanelController;
    public SawmillPanelController sawmillPanelController;
    public FarmPanelController farmPanelController;
    public BlacksmithPanelController blacksmithPanelController;
    public LabPanelController labPanelController;
    public BarracksPanelController barracksPanelController;
    public HospitalPanelController hospitalPanelController;
    public CastlePanelController castlePanelController;
    private TextMeshProUGUI buttonText;
    private TextMeshProUGUI healButtonText;


    public float time;
    public TextMeshProUGUI kalanZaman;

    private float totalAltin = 0, totalYemek = 0, totalDemir = 0, totalTas = 0, totalKereste = 0;
    
    void Start()
    {
        // Ba�lang��ta zaman s�f�rlanabilir.
        time = 0;
        buttonText = createUnitButton.GetComponentInChildren<TextMeshProUGUI>();
        healButtonText = healButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void CreateUnits()
    {
        //Progressbar� kontrol et 0'dan farkl�ysa ---> Bina Y�kseltmesi s�ras�nda asker �retemezsiniz.
        //De�ilse asker �retebilirsin.
        if (!isBarracksBuildActive)
        {
            if (Barracks.wasBarracksCreated == true)
            {
                // �retim s�relerini toplamak i�in de�i�kenler
                float totalTime = 0;
                totalUnitAmount = 0;
                // Sava��� slider'�n�n de�eri varsa
                if (slider.savasciSlider.value > 0)
                {
                    float savasciTime = slider.savasciSlider.value * savasciCreationTime;
                    totalTime += savasciTime;
                    totalUnitAmount += slider.savasciSlider.value;
                }

                // Ok�u slider'�n�n de�eri varsa
                if (slider.okcuSlider.value > 0)
                {
                    float okcuTime = slider.okcuSlider.value * okcuCreationTime;
                    totalTime += okcuTime;
                    totalUnitAmount += slider.okcuSlider.value;
                }

                // M�zrak�� slider'�n�n de�eri varsa
                if (slider.mizrakciSlider.value > 0)
                {
                    float mizrakciTime = slider.mizrakciSlider.value * mizrakciCreationTime;
                    totalTime += mizrakciTime;
                    totalUnitAmount += slider.mizrakciSlider.value;
                }

                // T�m birimlerin toplam �retim s�resi s�f�rdan b�y�kse progress bar'� g�ncelle
                if (totalTime > 0)
                {
                    // E�er progress bar doluyorsa ve aktifse
                    if (isUnitCreationActive)
                    {
                        // Mevcut animasyonu durdur
                        buttonText.text = "E�it";
                        giveCostBack(slider.savasciSlider.value, slider.okcuSlider.value, slider.mizrakciSlider.value);
                        LeanTween.cancel(progressBar);
                        isUnitCreationActive = false;
                        // Progress bar'� s�f�rla
                        ResetProgressBar(progressBar);
                        totalUnitAmount = 0;
                    }
                    else
                    {
                        // Progress bar'� ba�lat
                        isUnitCreationActive = true; // Progress bar aktif
                        buttonText.text = "�ptal Et";
                        reduceCost(slider.savasciSlider.value, slider.okcuSlider.value, slider.mizrakciSlider.value);
                        LeanTween.scaleX(progressBar, 1, totalTime)
                            .setOnComplete(() =>
                            {
                                // Progress bar doldu�unda yap�lacak i�lemler
                                buttonText.text = "�ret";
                                OnProgressComplete();
                                ResetProgressBar(progressBar); // Progress bar'� s�f�rlamak i�in �a��r
                               isUnitCreationActive = false;
                            });
                    }
                }
            }
            else
            {
                Debug.Log("�ncelikle bir k��la �retmelisiniz.");
            }
        }
        else
        {
            Debug.Log("Bina Y�kseltmesi S�ras�nda Asker E�itemezsin");
        }


        
        
    }

    void reduceCost(float savasciCount, float okcuCount, float mizrakciCount ) // Maliyetleri kaynaklardan d��en fonksiyon.
    {

        totalAltin = ((int)savasciCount * 5) + ((int)okcuCount * 7) + ((int)mizrakciCount * 7);
        totalYemek = ((int)savasciCount * 5) + ((int)okcuCount * 6) + ((int)mizrakciCount * 6);
        totalDemir = ((int)savasciCount * 5) + ((int)okcuCount * 3) + ((int)mizrakciCount * 3);
        totalTas = ((int)savasciCount * 5) + ((int)okcuCount * 2) + ((int)mizrakciCount * 2);
        totalKereste = ((int)savasciCount * 5) + ((int)okcuCount * 10) + ((int)mizrakciCount * 10);

        Kingdom.myKingdom.GoldAmount -= (int)totalAltin;
        Kingdom.myKingdom.FoodAmount -= (int)totalYemek;
        Kingdom.myKingdom.IronAmount -= (int)totalDemir;
        Kingdom.myKingdom.StoneAmount -= (int)totalTas;
        Kingdom.myKingdom.WoodAmount -= (int)totalKereste;
    }

    void giveCostBack(float savasciCount, float okcuCount, float mizrakciCount)
    {

        totalAltin = ((int)savasciCount * 5) + ((int)okcuCount * 7) + ((int)mizrakciCount * 7);
        totalYemek = ((int)savasciCount * 5) + ((int)okcuCount * 6) + ((int)mizrakciCount * 6);
        totalDemir = ((int)savasciCount * 5) + ((int)okcuCount * 3) + ((int)mizrakciCount * 3);
        totalTas = ((int)savasciCount * 5) + ((int)okcuCount * 2) + ((int)mizrakciCount * 2);
        totalKereste = ((int)savasciCount * 5) + ((int)okcuCount * 10) + ((int)mizrakciCount * 10);

        Kingdom.myKingdom.GoldAmount += (int)totalAltin;
        Kingdom.myKingdom.FoodAmount += (int)totalYemek;
        Kingdom.myKingdom.IronAmount += (int)totalDemir;
        Kingdom.myKingdom.StoneAmount += (int)totalTas;
        Kingdom.myKingdom.WoodAmount += (int)totalKereste;
    }
    void ResetProgressBar(GameObject gameObject)
    {
        gameObject.transform.localScale = new Vector3(0, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        // �sterseniz progress bar'� yeniden kullanmak i�in ba�ka i�lemler de yapabilirsiniz
    }
    void OnProgressComplete()
    {
        // Burada progress bar doldu�unda yap�lacak i�lemleri tan�mla
        Debug.Log("Progress Bar doldu, i�lem ger�ekle�tiriliyor!");
        Kingdom.myKingdom.SoldierAmount += totalUnitAmount;
        totalUnitAmount = 0;
        Debug.Log("Krall���n�z�n asker say�s�:" + Kingdom.myKingdom.SoldierAmount);
    }


    public void HealUnits()
    {
        if(!isHospitalBuildActive)
        {
            if (Hospital.wasHospitalCreated == true)
            {
                float totalHealTime = 0; // Toplam iyile�tirme s�resi

                // HastaneSlider de�erlerini kontrol et
                if (hastaneSlider.savasciSlider.value > 0)
                {
                    totalHealTime += hastaneSlider.savasciSlider.value * savasciHealTime;
                }

                if (hastaneSlider.okcuSlider.value > 0)
                {
                    totalHealTime += hastaneSlider.okcuSlider.value * okcuHealTime;
                }

                if (hastaneSlider.mizrakciSlider.value > 0)
                {
                    totalHealTime += hastaneSlider.mizrakciSlider.value * mizrakciHealTime;
                }

                // Toplam iyile�tirme s�resi s�f�rdan b�y�kse progress bar'� g�ncelle
                if (totalHealTime > 0)
                {


                    Debug.Log("Toplam iyile�tirme s�resi: " + totalHealTime);

                    // E�er progress bar doluyorsa ve aktifse
                    if (isHealActive)
                    {
                        // Mevcut animasyonu durdur
                        healButtonText.text = "�yile�tir";
                        giveCostBack(hastaneSlider.savasciSlider.value, hastaneSlider.okcuSlider.value, hastaneSlider.mizrakciSlider.value);
                        LeanTween.cancel(healProgressBar);
                        // Progress bar'� s�f�rla
                        isHealActive = false;
                        ResetProgressBar(healProgressBar);
                        totalHealTime = 0;
                    }
                    else
                    {
                        // Progress bar'� ba�lat
                        isHealActive = true; // Progress bar aktif
                        healButtonText.text = "�ptal Et";
                        reduceCost(hastaneSlider.savasciSlider.value, hastaneSlider.okcuSlider.value, hastaneSlider.mizrakciSlider.value);
                        LeanTween.scaleX(healProgressBar, 1, totalHealTime)
                            .setOnComplete(() =>
                            {
                                // Progress bar doldu�unda yap�lacak i�lemler
                                healButtonText.text = "�yile�tir";
                                OnProgressComplete();
                                ResetProgressBar(healProgressBar); // Progress bar'� s�f�rlamak i�in �a��r
                                isHealActive = false;

                            });
                    }
                }
            }
            else
            {
                Debug.Log("�ncelikle hastane in�a etmelisiniz.");
            }
        }
        else
        {
            Debug.Log("�n�a s�ras�nda birlik e�itemezsin");
        }
        
        
    }


    public IEnumerator WarehouseIsFinished(Warehouse warehouse, System.Action<bool> onCompletion)
    {
        wareHousePanelController.cancelWarehouseButton.gameObject.SetActive(true);
        wareHousePanelController.isBuildCanceled = false; // �ptal durumu s�f�rla
       
        // LeanTween animasyonu ba�lat

        LeanTween.scaleX(buildWareHouseBar, 1, warehouse.buildTime).setOnComplete(() => ResetProgressBar(buildWareHouseBar));

        float elapsedTime = 0f; // Ge�en zaman� takip et

        while (elapsedTime < warehouse.buildTime)
        {
            if (wareHousePanelController.isBuildCanceled) // E�er iptal edilirse
            {
                LeanTween.cancel(buildWareHouseBar); // Animasyonu iptal et
                ResetProgressBar(buildWareHouseBar); // ProgressBar'� s�f�rla
                onCompletion(false); // Ba�ar�s�zl�k durumunu bildir
                yield break; // Coroutine sonland�r
            }

            elapsedTime += Time.deltaTime; // Ge�en s�reyi art�r
            yield return null; // Bir sonraki kareye kadar bekle
        }

        // �ptal edilmeden tamamland�ysa
        wareHousePanelController.cancelWarehouseButton.gameObject.SetActive(false);
        onCompletion(true); // Tamamland���nda ba�ar�l� olarak bildir
    }

    public IEnumerator StonePitIsFinished(StonePit stonepit, System.Action<bool> onCompletion)
    {
        stonepitPanelController.cancelStonepitButton.gameObject.SetActive(true);
        stonepitPanelController.isBuildCanceled = false; // �ptal durumu s�f�rla

        // LeanTween animasyonu ba�lat

        LeanTween.scaleX(buildStonepitBar, 1, stonepit.buildTime).setOnComplete(() => ResetProgressBar(buildStonepitBar));

        float elapsedTime = 0f; // Ge�en zaman� takip et

        while (elapsedTime < stonepit.buildTime)
        {
            if (stonepitPanelController.isBuildCanceled) // E�er iptal edilirse
            {
                LeanTween.cancel(buildStonepitBar); // Animasyonu iptal et
                ResetProgressBar(buildStonepitBar); // ProgressBar'� s�f�rla
                onCompletion(false); // Ba�ar�s�zl�k durumunu bildir
                yield break; // Coroutine sonland�r
            }

            elapsedTime += Time.deltaTime; // Ge�en s�reyi art�r
            yield return null; // Bir sonraki kareye kadar bekle
        }

        // �ptal edilmeden tamamland�ysa
        stonepitPanelController.cancelStonepitButton.gameObject.SetActive(false);
        onCompletion(true); // Tamamland���nda ba�ar�l� olarak bildir

    }

    public IEnumerator SawmillIsFinished(Sawmill sawmill, System.Action<bool> onCompletion)
    {
        sawmillPanelController.cancelSawmillButton.gameObject.SetActive(true);
        sawmillPanelController.isBuildCanceled = false; // �ptal durumu s�f�rla

        // LeanTween animasyonu ba�lat

        LeanTween.scaleX(buildSawmillBar, 1, sawmill.buildTime).setOnComplete(() => ResetProgressBar(buildSawmillBar));

        float elapsedTime = 0f; // Ge�en zaman� takip et

        while (elapsedTime < sawmill.buildTime)
        {
            if (sawmillPanelController.isBuildCanceled) // E�er iptal edilirse
            {
                LeanTween.cancel(buildSawmillBar); // Animasyonu iptal et
                ResetProgressBar(buildSawmillBar); // ProgressBar'� s�f�rla
                onCompletion(false); // Ba�ar�s�zl�k durumunu bildir
                yield break; // Coroutine sonland�r
            }

            elapsedTime += Time.deltaTime; // Ge�en s�reyi art�r
            yield return null; // Bir sonraki kareye kadar bekle
        }

        // �ptal edilmeden tamamland�ysa
        sawmillPanelController.cancelSawmillButton.gameObject.SetActive(false);
        onCompletion(true); // Tamamland���nda ba�ar�l� olarak bildir
    }

    public IEnumerator FarmIsFinished(Farm farm, System.Action<bool> onCompletion)
    {
        farmPanelController.cancelFarmButton.gameObject.SetActive(true);
        farmPanelController.isBuildCanceled = false; // �ptal durumu s�f�rla

        // LeanTween animasyonu ba�lat
        LeanTween.scaleX(buildFarmBar, 1, farm.buildTime).setOnComplete(() => ResetProgressBar(buildFarmBar));

        float elapsedTime = 0f; // Ge�en zaman� takip et

        while (elapsedTime < farm.buildTime)
        {
            if (farmPanelController.isBuildCanceled) // E�er iptal edilirse
            {
                LeanTween.cancel(buildFarmBar); // Animasyonu iptal et
                ResetProgressBar(buildFarmBar); // ProgressBar'� s�f�rla
                onCompletion(false); // Ba�ar�s�zl�k durumunu bildir
                yield break; // Coroutine sonland�r
            }

            elapsedTime += Time.deltaTime; // Ge�en s�reyi art�r
            yield return null; // Bir sonraki kareye kadar bekle
        }

        // �ptal edilmeden tamamland�ysa
        farmPanelController.cancelFarmButton.gameObject.SetActive(false);
        onCompletion(true); // Tamamland���nda ba�ar�l� olarak bildir
    }


    public IEnumerator BlacksmithIsFinished(Blacksmith blacksmith, System.Action<bool> onCompletion)
    {
        blacksmithPanelController.cancelBlacksmithButton.gameObject.SetActive(true);
        blacksmithPanelController.isBuildCanceled = false; // �ptal durumu s�f�rla

        // LeanTween animasyonu ba�lat
        LeanTween.scaleX(buildBlacksmithBar, 1, blacksmith.buildTime).setOnComplete(() => ResetProgressBar(buildBlacksmithBar));

        float elapsedTime = 0f; // Ge�en zaman� takip et

        while (elapsedTime < blacksmith.buildTime)
        {
            if (blacksmithPanelController.isBuildCanceled) // E�er iptal edilirse
            {
                LeanTween.cancel(buildBlacksmithBar); // Animasyonu iptal et
                ResetProgressBar(buildBlacksmithBar); // ProgressBar'� s�f�rla
                onCompletion(false); // Ba�ar�s�zl�k durumunu bildir
                yield break; // Coroutine sonland�r
            }

            elapsedTime += Time.deltaTime; // Ge�en s�reyi art�r
            yield return null; // Bir sonraki kareye kadar bekle
        }

        // �ptal edilmeden tamamland�ysa
        blacksmithPanelController.cancelBlacksmithButton.gameObject.SetActive(false);
        onCompletion(true); // Tamamland���nda ba�ar�l� olarak bildir
    }

    public IEnumerator LabIsFinished(Lab lab, System.Action<bool> onCompletion)
    {
        if(ResearchButtonEvents.isAnyResearchActive)
        {
            Debug.Log("Ara�t�rma S�ras�nda Bina Y�kseltmesi Yapamazs�n�z");
        }
        else
        {
            labPanelController.cancelLabButton.gameObject.SetActive(true);
            labPanelController.isBuildCanceled = false; // �ptal durumu s�f�rla

            // LeanTween animasyonu ba�lat
            LeanTween.scaleX(buildLabBar, 1, lab.buildTime).setOnComplete(() => ResetProgressBar(buildLabBar));
            isLabBuildActive = true;
            float elapsedTime = 0f; // Ge�en zaman� takip et

            while (elapsedTime < lab.buildTime)
            {
                if (labPanelController.isBuildCanceled) // E�er iptal edilirse
                {
                    LeanTween.cancel(buildLabBar); // Animasyonu iptal et
                    isLabBuildActive = false;
                    ResetProgressBar(buildLabBar); // ProgressBar'� s�f�rla
                    onCompletion(false); // Ba�ar�s�zl�k durumunu bildir
                    yield break; // Coroutine sonland�r
                }

                elapsedTime += Time.deltaTime; // Ge�en s�reyi art�r
                yield return null; // Bir sonraki kareye kadar bekle
            }

            // �ptal edilmeden tamamland�ysa
            isLabBuildActive = false;

            labPanelController.cancelLabButton.gameObject.SetActive(false);
            onCompletion(true); // Tamamland���nda ba�ar�l� olarak bildir
        }
    }

    public IEnumerator BarracksIsFinished(Barracks barracks, System.Action<bool> onCompletion)
    {

        if(isUnitCreationActive)
        {
            Debug.Log("Asker �retimi Yaparken Bina Y�kseltemezsiniz");
        }

        else
        {
            barracksPanelController.cancelBarracksButton.gameObject.SetActive(true);
            barracksPanelController.isBuildCanceled = false; // �ptal durumu s�f�rla

            // LeanTween animasyonu ba�lat
            LeanTween.scaleX(buildBarracksBar, 1, barracks.buildTime).setOnComplete(() => ResetProgressBar(buildBarracksBar));
            isBarracksBuildActive = true;

            float elapsedTime = 0f; // Ge�en zaman� takip et

            while (elapsedTime < barracks.buildTime)
            {
                if (barracksPanelController.isBuildCanceled) // E�er iptal edilirse
                {
                    LeanTween.cancel(buildBarracksBar); // Animasyonu iptal et
                    ResetProgressBar(buildBarracksBar); // ProgressBar'� s�f�rla
                    isBarracksBuildActive = false;
                    onCompletion(false); // Ba�ar�s�zl�k durumunu bildir
                    yield break; // Coroutine sonland�r
                }

                elapsedTime += Time.deltaTime; // Ge�en s�reyi art�r
                yield return null; // Bir sonraki kareye kadar bekle
            }

            // �ptal edilmeden tamamland�ysa
            isBarracksBuildActive = false;
            barracksPanelController.cancelBarracksButton.gameObject.SetActive(false);
            onCompletion(true); // Tamamland���nda ba�ar�l� olarak bildir
        } 
    }

    public IEnumerator HospitalIsFinished(Hospital hospital, System.Action<bool> onCompletion)
    {
        if(!isHealActive) 
        {
            hospitalPanelController.cancelHospitalButton.gameObject.SetActive(true);
            hospitalPanelController.isBuildCanceled = false; // �ptal durumu s�f�rla

            // LeanTween animasyonu ba�lat
            LeanTween.scaleX(buildHospitalBar, 1, hospital.buildTime).setOnComplete(() => ResetProgressBar(buildHospitalBar));
            isHospitalBuildActive = true;
            float elapsedTime = 0f; // Ge�en zaman� takip et

            while (elapsedTime < hospital.buildTime)
            {
                if (hospitalPanelController.isBuildCanceled) // E�er iptal edilirse
                {
                    LeanTween.cancel(buildHospitalBar); // Animasyonu iptal et
                    ResetProgressBar(buildHospitalBar); // ProgressBar'� s�f�rla
                    isHospitalBuildActive=false;
                    onCompletion(false); // Ba�ar�s�zl�k durumunu bildir
                    yield break; // Coroutine sonland�r
                }

                elapsedTime += Time.deltaTime; // Ge�en s�reyi art�r
                yield return null; // Bir sonraki kareye kadar bekle
            }

            // �ptal edilmeden tamamland�ysa
            isHospitalBuildActive = false;
            hospitalPanelController.cancelHospitalButton.gameObject.SetActive(false);
            onCompletion(true); // Tamamland���nda ba�ar�l� olarak bildir
        }

        else
        {
            Debug.Log("�yile�tirme s�ras�nda bina y�kseltemezsiniz, iyile�tirmeyi iptal edip yeniden deneyin.");
        }
       
    }

    public IEnumerator CastleIsFinished(Castle castle, System.Action<bool> onCompletion)
    {
        castlePanelController.cancelUpgradeCastleButton.gameObject.SetActive(true);               
        castlePanelController.isBuildCanceled = false; // �ptal durumu s�f�rla

        // LeanTween animasyonu ba�lat

        LeanTween.scaleX(upgradeCastleBar, 1, castle.buildTime).setOnComplete(() => ResetProgressBar(upgradeCastleBar));

        float elapsedTime = 0f; // Ge�en zaman� takip et

        while (elapsedTime < castle.buildTime)
        {
            if (castlePanelController.isBuildCanceled) // E�er iptal edilirse
            {
                LeanTween.cancel(upgradeCastleBar); // Animasyonu iptal et
                ResetProgressBar(upgradeCastleBar); // ProgressBar'� s�f�rla
                onCompletion(false); // Ba�ar�s�zl�k durumunu bildir
                yield break; // Coroutine sonland�r
            }

            elapsedTime += Time.deltaTime; // Ge�en s�reyi art�r
            yield return null; // Bir sonraki kareye kadar bekle
        }

        // �ptal edilmeden tamamland�ysa
        castlePanelController.cancelUpgradeCastleButton.gameObject.SetActive(false);
        onCompletion(true); // Tamamland���nda ba�ar�l� olarak bildir
    }
}
