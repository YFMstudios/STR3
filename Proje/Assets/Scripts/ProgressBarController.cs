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
    private TextMeshProUGUI buttonText;
    private TextMeshProUGUI healButtonText;


    public float time;
    public TextMeshProUGUI kalanZaman;

    private float totalAltin = 0, totalYemek = 0, totalDemir = 0, totalTas = 0, totalKereste = 0;
    
    void Start()
    {
        // Baþlangýçta zaman sýfýrlanabilir.
        time = 0;
        buttonText = createUnitButton.GetComponentInChildren<TextMeshProUGUI>();
        healButtonText = healButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void CreateUnits()
    {
        //Progressbarý kontrol et 0'dan farklýysa ---> Bina Yükseltmesi sýrasýnda asker üretemezsiniz.
        //Deðilse asker üretebilirsin.
        if (!isBarracksBuildActive)
        {
            if (Barracks.wasBarracksCreated == true)
            {
                // Üretim sürelerini toplamak için deðiþkenler
                float totalTime = 0;
                totalUnitAmount = 0;
                // Savaþçý slider'ýnýn deðeri varsa
                if (slider.savasciSlider.value > 0)
                {
                    float savasciTime = slider.savasciSlider.value * savasciCreationTime;
                    totalTime += savasciTime;
                    totalUnitAmount += slider.savasciSlider.value;
                }

                // Okçu slider'ýnýn deðeri varsa
                if (slider.okcuSlider.value > 0)
                {
                    float okcuTime = slider.okcuSlider.value * okcuCreationTime;
                    totalTime += okcuTime;
                    totalUnitAmount += slider.okcuSlider.value;
                }

                // Mýzrakçý slider'ýnýn deðeri varsa
                if (slider.mizrakciSlider.value > 0)
                {
                    float mizrakciTime = slider.mizrakciSlider.value * mizrakciCreationTime;
                    totalTime += mizrakciTime;
                    totalUnitAmount += slider.mizrakciSlider.value;
                }

                // Tüm birimlerin toplam üretim süresi sýfýrdan büyükse progress bar'ý güncelle
                if (totalTime > 0)
                {
                    // Eðer progress bar doluyorsa ve aktifse
                    if (isUnitCreationActive)
                    {
                        // Mevcut animasyonu durdur
                        buttonText.text = "Eðit";
                        giveCostBack(slider.savasciSlider.value, slider.okcuSlider.value, slider.mizrakciSlider.value);
                        LeanTween.cancel(progressBar);
                        isUnitCreationActive = false;
                        // Progress bar'ý sýfýrla
                        ResetProgressBar(progressBar);
                        totalUnitAmount = 0;
                    }
                    else
                    {
                        // Progress bar'ý baþlat
                        isUnitCreationActive = true; // Progress bar aktif
                        buttonText.text = "Ýptal Et";
                        reduceCost(slider.savasciSlider.value, slider.okcuSlider.value, slider.mizrakciSlider.value);
                        LeanTween.scaleX(progressBar, 1, totalTime)
                            .setOnComplete(() =>
                            {
                                // Progress bar dolduðunda yapýlacak iþlemler
                                buttonText.text = "Üret";
                                OnProgressComplete();
                                ResetProgressBar(progressBar); // Progress bar'ý sýfýrlamak için çaðýr
                               isUnitCreationActive = false;
                            });
                    }
                }
            }
            else
            {
                Debug.Log("Öncelikle bir kýþla üretmelisiniz.");
            }
        }
        else
        {
            Debug.Log("Bina Yükseltmesi Sýrasýnda Asker Eðitemezsin");
        }


        
        
    }

    void reduceCost(float savasciCount, float okcuCount, float mizrakciCount ) // Maliyetleri kaynaklardan düþen fonksiyon.
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
        // Ýsterseniz progress bar'ý yeniden kullanmak için baþka iþlemler de yapabilirsiniz
    }
    void OnProgressComplete()
    {
        // Burada progress bar dolduðunda yapýlacak iþlemleri tanýmla
        Debug.Log("Progress Bar doldu, iþlem gerçekleþtiriliyor!");
        Kingdom.myKingdom.SoldierAmount += totalUnitAmount;
        totalUnitAmount = 0;
        Debug.Log("Krallýðýnýzýn asker sayýsý:" + Kingdom.myKingdom.SoldierAmount);
    }


    public void HealUnits()
    {
        if(!isHospitalBuildActive)
        {
            if (Hospital.wasHospitalCreated == true)
            {
                float totalHealTime = 0; // Toplam iyileþtirme süresi

                // HastaneSlider deðerlerini kontrol et
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

                // Toplam iyileþtirme süresi sýfýrdan büyükse progress bar'ý güncelle
                if (totalHealTime > 0)
                {


                    Debug.Log("Toplam iyileþtirme süresi: " + totalHealTime);

                    // Eðer progress bar doluyorsa ve aktifse
                    if (isHealActive)
                    {
                        // Mevcut animasyonu durdur
                        healButtonText.text = "Ýyileþtir";
                        giveCostBack(hastaneSlider.savasciSlider.value, hastaneSlider.okcuSlider.value, hastaneSlider.mizrakciSlider.value);
                        LeanTween.cancel(healProgressBar);
                        // Progress bar'ý sýfýrla
                        isHealActive = false;
                        ResetProgressBar(healProgressBar);
                        totalHealTime = 0;
                    }
                    else
                    {
                        // Progress bar'ý baþlat
                        isHealActive = true; // Progress bar aktif
                        healButtonText.text = "Ýptal Et";
                        reduceCost(hastaneSlider.savasciSlider.value, hastaneSlider.okcuSlider.value, hastaneSlider.mizrakciSlider.value);
                        LeanTween.scaleX(healProgressBar, 1, totalHealTime)
                            .setOnComplete(() =>
                            {
                                // Progress bar dolduðunda yapýlacak iþlemler
                                healButtonText.text = "Ýyileþtir";
                                OnProgressComplete();
                                ResetProgressBar(healProgressBar); // Progress bar'ý sýfýrlamak için çaðýr
                                isHealActive = false;

                            });
                    }
                }
            }
            else
            {
                Debug.Log("Öncelikle hastane inþa etmelisiniz.");
            }
        }
        else
        {
            Debug.Log("Ýnþa sýrasýnda birlik eðitemezsin");
        }
        
        
    }


    public IEnumerator WarehouseIsFinished(Warehouse warehouse, System.Action<bool> onCompletion)
    {
        wareHousePanelController.cancelWarehouseButton.gameObject.SetActive(true);
        wareHousePanelController.isBuildCanceled = false; // Ýptal durumu sýfýrla
       
        // LeanTween animasyonu baþlat

        LeanTween.scaleX(buildWareHouseBar, 1, warehouse.buildTime).setOnComplete(() => ResetProgressBar(buildWareHouseBar));

        float elapsedTime = 0f; // Geçen zamaný takip et

        while (elapsedTime < warehouse.buildTime)
        {
            if (wareHousePanelController.isBuildCanceled) // Eðer iptal edilirse
            {
                LeanTween.cancel(buildWareHouseBar); // Animasyonu iptal et
                ResetProgressBar(buildWareHouseBar); // ProgressBar'ý sýfýrla
                onCompletion(false); // Baþarýsýzlýk durumunu bildir
                yield break; // Coroutine sonlandýr
            }

            elapsedTime += Time.deltaTime; // Geçen süreyi artýr
            yield return null; // Bir sonraki kareye kadar bekle
        }

        // Ýptal edilmeden tamamlandýysa
        wareHousePanelController.cancelWarehouseButton.gameObject.SetActive(false);
        onCompletion(true); // Tamamlandýðýnda baþarýlý olarak bildir
    }

    public IEnumerator StonePitIsFinished(StonePit stonepit, System.Action<bool> onCompletion)
    {
        stonepitPanelController.cancelStonepitButton.gameObject.SetActive(true);
        stonepitPanelController.isBuildCanceled = false; // Ýptal durumu sýfýrla

        // LeanTween animasyonu baþlat

        LeanTween.scaleX(buildStonepitBar, 1, stonepit.buildTime).setOnComplete(() => ResetProgressBar(buildStonepitBar));

        float elapsedTime = 0f; // Geçen zamaný takip et

        while (elapsedTime < stonepit.buildTime)
        {
            if (stonepitPanelController.isBuildCanceled) // Eðer iptal edilirse
            {
                LeanTween.cancel(buildStonepitBar); // Animasyonu iptal et
                ResetProgressBar(buildStonepitBar); // ProgressBar'ý sýfýrla
                onCompletion(false); // Baþarýsýzlýk durumunu bildir
                yield break; // Coroutine sonlandýr
            }

            elapsedTime += Time.deltaTime; // Geçen süreyi artýr
            yield return null; // Bir sonraki kareye kadar bekle
        }

        // Ýptal edilmeden tamamlandýysa
        stonepitPanelController.cancelStonepitButton.gameObject.SetActive(false);
        onCompletion(true); // Tamamlandýðýnda baþarýlý olarak bildir

    }

    public IEnumerator SawmillIsFinished(Sawmill sawmill, System.Action<bool> onCompletion)
    {
        sawmillPanelController.cancelSawmillButton.gameObject.SetActive(true);
        sawmillPanelController.isBuildCanceled = false; // Ýptal durumu sýfýrla

        // LeanTween animasyonu baþlat

        LeanTween.scaleX(buildSawmillBar, 1, sawmill.buildTime).setOnComplete(() => ResetProgressBar(buildSawmillBar));

        float elapsedTime = 0f; // Geçen zamaný takip et

        while (elapsedTime < sawmill.buildTime)
        {
            if (sawmillPanelController.isBuildCanceled) // Eðer iptal edilirse
            {
                LeanTween.cancel(buildSawmillBar); // Animasyonu iptal et
                ResetProgressBar(buildSawmillBar); // ProgressBar'ý sýfýrla
                onCompletion(false); // Baþarýsýzlýk durumunu bildir
                yield break; // Coroutine sonlandýr
            }

            elapsedTime += Time.deltaTime; // Geçen süreyi artýr
            yield return null; // Bir sonraki kareye kadar bekle
        }

        // Ýptal edilmeden tamamlandýysa
        sawmillPanelController.cancelSawmillButton.gameObject.SetActive(false);
        onCompletion(true); // Tamamlandýðýnda baþarýlý olarak bildir
    }

    public IEnumerator FarmIsFinished(Farm farm, System.Action<bool> onCompletion)
    {
        farmPanelController.cancelFarmButton.gameObject.SetActive(true);
        farmPanelController.isBuildCanceled = false; // Ýptal durumu sýfýrla

        // LeanTween animasyonu baþlat
        LeanTween.scaleX(buildFarmBar, 1, farm.buildTime).setOnComplete(() => ResetProgressBar(buildFarmBar));

        float elapsedTime = 0f; // Geçen zamaný takip et

        while (elapsedTime < farm.buildTime)
        {
            if (farmPanelController.isBuildCanceled) // Eðer iptal edilirse
            {
                LeanTween.cancel(buildFarmBar); // Animasyonu iptal et
                ResetProgressBar(buildFarmBar); // ProgressBar'ý sýfýrla
                onCompletion(false); // Baþarýsýzlýk durumunu bildir
                yield break; // Coroutine sonlandýr
            }

            elapsedTime += Time.deltaTime; // Geçen süreyi artýr
            yield return null; // Bir sonraki kareye kadar bekle
        }

        // Ýptal edilmeden tamamlandýysa
        farmPanelController.cancelFarmButton.gameObject.SetActive(false);
        onCompletion(true); // Tamamlandýðýnda baþarýlý olarak bildir
    }


    public IEnumerator BlacksmithIsFinished(Blacksmith blacksmith, System.Action<bool> onCompletion)
    {
        blacksmithPanelController.cancelBlacksmithButton.gameObject.SetActive(true);
        blacksmithPanelController.isBuildCanceled = false; // Ýptal durumu sýfýrla

        // LeanTween animasyonu baþlat
        LeanTween.scaleX(buildBlacksmithBar, 1, blacksmith.buildTime).setOnComplete(() => ResetProgressBar(buildBlacksmithBar));

        float elapsedTime = 0f; // Geçen zamaný takip et

        while (elapsedTime < blacksmith.buildTime)
        {
            if (blacksmithPanelController.isBuildCanceled) // Eðer iptal edilirse
            {
                LeanTween.cancel(buildBlacksmithBar); // Animasyonu iptal et
                ResetProgressBar(buildBlacksmithBar); // ProgressBar'ý sýfýrla
                onCompletion(false); // Baþarýsýzlýk durumunu bildir
                yield break; // Coroutine sonlandýr
            }

            elapsedTime += Time.deltaTime; // Geçen süreyi artýr
            yield return null; // Bir sonraki kareye kadar bekle
        }

        // Ýptal edilmeden tamamlandýysa
        blacksmithPanelController.cancelBlacksmithButton.gameObject.SetActive(false);
        onCompletion(true); // Tamamlandýðýnda baþarýlý olarak bildir
    }

    public IEnumerator LabIsFinished(Lab lab, System.Action<bool> onCompletion)
    {
        if(ResearchButtonEvents.isAnyResearchActive)
        {
            Debug.Log("Araþtýrma Sýrasýnda Bina Yükseltmesi Yapamazsýnýz");
        }
        else
        {
            labPanelController.cancelLabButton.gameObject.SetActive(true);
            labPanelController.isBuildCanceled = false; // Ýptal durumu sýfýrla

            // LeanTween animasyonu baþlat
            LeanTween.scaleX(buildLabBar, 1, lab.buildTime).setOnComplete(() => ResetProgressBar(buildLabBar));
            isLabBuildActive = true;
            float elapsedTime = 0f; // Geçen zamaný takip et

            while (elapsedTime < lab.buildTime)
            {
                if (labPanelController.isBuildCanceled) // Eðer iptal edilirse
                {
                    LeanTween.cancel(buildLabBar); // Animasyonu iptal et
                    isLabBuildActive = false;
                    ResetProgressBar(buildLabBar); // ProgressBar'ý sýfýrla
                    onCompletion(false); // Baþarýsýzlýk durumunu bildir
                    yield break; // Coroutine sonlandýr
                }

                elapsedTime += Time.deltaTime; // Geçen süreyi artýr
                yield return null; // Bir sonraki kareye kadar bekle
            }

            // Ýptal edilmeden tamamlandýysa
            isLabBuildActive = false;

            labPanelController.cancelLabButton.gameObject.SetActive(false);
            onCompletion(true); // Tamamlandýðýnda baþarýlý olarak bildir
        }
    }

    public IEnumerator BarracksIsFinished(Barracks barracks, System.Action<bool> onCompletion)
    {

        if(isUnitCreationActive)
        {
            Debug.Log("Asker Üretimi Yaparken Bina Yükseltemezsiniz");
        }

        else
        {
            barracksPanelController.cancelBarracksButton.gameObject.SetActive(true);
            barracksPanelController.isBuildCanceled = false; // Ýptal durumu sýfýrla

            // LeanTween animasyonu baþlat
            LeanTween.scaleX(buildBarracksBar, 1, barracks.buildTime).setOnComplete(() => ResetProgressBar(buildBarracksBar));
            isBarracksBuildActive = true;

            float elapsedTime = 0f; // Geçen zamaný takip et

            while (elapsedTime < barracks.buildTime)
            {
                if (barracksPanelController.isBuildCanceled) // Eðer iptal edilirse
                {
                    LeanTween.cancel(buildBarracksBar); // Animasyonu iptal et
                    ResetProgressBar(buildBarracksBar); // ProgressBar'ý sýfýrla
                    isBarracksBuildActive = false;
                    onCompletion(false); // Baþarýsýzlýk durumunu bildir
                    yield break; // Coroutine sonlandýr
                }

                elapsedTime += Time.deltaTime; // Geçen süreyi artýr
                yield return null; // Bir sonraki kareye kadar bekle
            }

            // Ýptal edilmeden tamamlandýysa
            isBarracksBuildActive = false;
            barracksPanelController.cancelBarracksButton.gameObject.SetActive(false);
            onCompletion(true); // Tamamlandýðýnda baþarýlý olarak bildir
        } 
    }

    public IEnumerator HospitalIsFinished(Hospital hospital, System.Action<bool> onCompletion)
    {
        if(!isHealActive) 
        {
            hospitalPanelController.cancelHospitalButton.gameObject.SetActive(true);
            hospitalPanelController.isBuildCanceled = false; // Ýptal durumu sýfýrla

            // LeanTween animasyonu baþlat
            LeanTween.scaleX(buildHospitalBar, 1, hospital.buildTime).setOnComplete(() => ResetProgressBar(buildHospitalBar));
            isHospitalBuildActive = true;
            float elapsedTime = 0f; // Geçen zamaný takip et

            while (elapsedTime < hospital.buildTime)
            {
                if (hospitalPanelController.isBuildCanceled) // Eðer iptal edilirse
                {
                    LeanTween.cancel(buildHospitalBar); // Animasyonu iptal et
                    ResetProgressBar(buildHospitalBar); // ProgressBar'ý sýfýrla
                    isHospitalBuildActive=false;
                    onCompletion(false); // Baþarýsýzlýk durumunu bildir
                    yield break; // Coroutine sonlandýr
                }

                elapsedTime += Time.deltaTime; // Geçen süreyi artýr
                yield return null; // Bir sonraki kareye kadar bekle
            }

            // Ýptal edilmeden tamamlandýysa
            isHospitalBuildActive = false;
            hospitalPanelController.cancelHospitalButton.gameObject.SetActive(false);
            onCompletion(true); // Tamamlandýðýnda baþarýlý olarak bildir
        }

        else
        {
            Debug.Log("Ýyileþtirme sýrasýnda bina yükseltemezsiniz, iyileþtirmeyi iptal edip yeniden deneyin.");
        }
       
    }
}
