using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorTransition : MonoBehaviour
{
    public Image[] seviyeImages; // Tüm seviyeler için Image nesnelerini tutar
    public float duration = 120f; // Geçiþ süresi (örnek: 2 dakika)

    public ProgressBarController progressBarController;
    public Button createLabButton;


    private Material[] uniqueMaterials; // Her Image için ayrý materyaller
    public ResearchController researchController;
    void Start()
    {
        // Her Image için benzersiz materyal oluþtur ve baþlangýçta renksiz hale getir
        uniqueMaterials = new Material[seviyeImages.Length];
        for (int i = 0; i < seviyeImages.Length; i++)
        {
            if (seviyeImages[i] != null)
            {
                uniqueMaterials[i] = new Material(seviyeImages[i].material); // Orijinal materyalin bir kopyasýný al
                seviyeImages[i].material = uniqueMaterials[i]; // Image'e bu kopyayý ata
                uniqueMaterials[i].SetFloat("_FillAmount", 0f); // Baþlangýçta tamamen renksiz yap
            }
        }
    }

    public void StartColorTransitionSeviye1()
    {
        if (seviyeImages.Length >= 1 && seviyeImages[0] != null)
            StartCoroutine(ColorTransition(seviyeImages[0], 0));
        else
            Debug.LogError("Seviye 1 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye2()
    {
        if (seviyeImages.Length >= 2 && seviyeImages[1] != null)
            StartCoroutine(ColorTransition(seviyeImages[1], 1));
        else
            Debug.LogError("Seviye 2 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye3()
    {
        if (seviyeImages.Length >= 3 && seviyeImages[2] != null)
            StartCoroutine(ColorTransition(seviyeImages[2], 2));
        else
            Debug.LogError("Seviye 3 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye4()
    {
        if (seviyeImages.Length >= 4 && seviyeImages[3] != null)
            StartCoroutine(ColorTransition(seviyeImages[3], 3));
        else
            Debug.LogError("Seviye 4 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye5()
    {
        if (seviyeImages.Length >= 5 && seviyeImages[4] != null)
            StartCoroutine(ColorTransition(seviyeImages[4], 4));
        else
            Debug.LogError("Seviye 5 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye6()
    {
        if (seviyeImages.Length >= 6 && seviyeImages[5] != null)
            StartCoroutine(ColorTransition(seviyeImages[5], 5));
        else
            Debug.LogError("Seviye 6 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye7()
    {
        if (seviyeImages.Length >= 7 && seviyeImages[6] != null)
            StartCoroutine(ColorTransition(seviyeImages[6], 6));
        else
            Debug.LogError("Seviye 7 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye8()
    {
        if (seviyeImages.Length >= 8 && seviyeImages[7] != null)
            StartCoroutine(ColorTransition(seviyeImages[7], 7));
        else
            Debug.LogError("Seviye 8 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye9()
    {
        if (seviyeImages.Length >= 9 && seviyeImages[8] != null)
            StartCoroutine(ColorTransition(seviyeImages[8], 8));
        else
            Debug.LogError("Seviye 9 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye10()
    {
        if (seviyeImages.Length >= 10 && seviyeImages[9] != null)
            StartCoroutine(ColorTransition(seviyeImages[9], 9));
        else
            Debug.LogError("Seviye 10 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye11()
    {
        if (seviyeImages.Length >= 11 && seviyeImages[10] != null)
            StartCoroutine(ColorTransition(seviyeImages[10], 10));
        else
            Debug.LogError("Seviye 11 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye12()
    {
        if (seviyeImages.Length >= 12 && seviyeImages[11] != null)
            StartCoroutine(ColorTransition(seviyeImages[11], 11));
        else
            Debug.LogError("Seviye 12 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye13()
    {
        if (seviyeImages.Length >= 13 && seviyeImages[12] != null)
            StartCoroutine(ColorTransition(seviyeImages[12], 12));
        else
            Debug.LogError("Seviye 13 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye14()
    {
        if (seviyeImages.Length >= 14 && seviyeImages[13] != null)
            StartCoroutine(ColorTransition(seviyeImages[13], 13));
        else
            Debug.LogError("Seviye 14 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye15()
    {
        if (seviyeImages.Length >= 15 && seviyeImages[14] != null)
            StartCoroutine(ColorTransition(seviyeImages[14], 14));
        else
            Debug.LogError("Seviye 15 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye16()
    {
        if (seviyeImages.Length >= 16 && seviyeImages[15] != null)
            StartCoroutine(ColorTransition(seviyeImages[15], 15));
        else
            Debug.LogError("Seviye 16 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye17()
    {
        if (seviyeImages.Length >= 17 && seviyeImages[16] != null)
            StartCoroutine(ColorTransition(seviyeImages[16], 16));
        else
            Debug.LogError("Seviye 17 için geçerli bir Image atanmadý.");
    }

    public void StartColorTransitionSeviye18()
    {
        if (seviyeImages.Length >= 18 && seviyeImages[17] != null)
            StartCoroutine(ColorTransition(seviyeImages[17], 17));
        else
            Debug.LogError("Seviye 18 için geçerli bir Image atanmadý.");
    }




    private IEnumerator ColorTransition(Image targetImage , int researchLevel)
    {
        if(progressBarController.isLabBuildActive || ResearchButtonEvents.isAnyResearchActive)
        {
            Debug.Log("Bina Yüksektmesi veya bir araþtýrmanýn halihazýrda aktif olmasý durumunda araþtýrma yapamazsýnýz.");
        }
        else
        {
            Material material = targetImage.material;
            if (material == null)
            {
                Debug.LogError($"Material is missing on the target image: {targetImage.name}");
                yield break;
            }

            float elapsedTime = 0f;
            ResearchButtonEvents.isAnyResearchActive = true;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration; // 0 ile 1 arasýnda ilerleme
                material.SetFloat("_FillAmount", progress); // Renklendirme ilerlemesi
                yield return null;
            }
            material.SetFloat("_FillAmount", 1f); // Tamamen renkli hale getir
            ResearchButtonEvents.isResearched[researchLevel] = true;
            ResearchButtonEvents.isAnyResearchActive = false;
            if(createLabButton != null)
            {
                createLabButton.enabled = true;
            }
           

            switch (researchLevel)
            {
                case 0: researchController.OpenTwoAndThreeLevels(); break;

                case 1: researchController.OpenFourLevel(); break;

                case 2: researchController.OpenFiveLevel(); break;

                case 3: researchController.controlBuildLevelTwoResearches(); break;

                case 4: researchController.controlBuildLevelTwoResearches(); break;

                case 5: researchController.control9And10Levels(); break;

                case 6: researchController.control9And10Levels(); break;

                case 7: researchController.control9And10Levels(); break;

                case 8: researchController.control11And12And13Levels(); break;

                case 9: researchController.control11And12And13Levels(); break;

                case 10: researchController.controlBuildLevelThreeResearches(); break;

                case 11: researchController.controlBuildLevelThreeResearches(); break;

                case 12: researchController.controlBuildLevelThreeResearches(); break;

                case 13: researchController.control16And17Levels(); break;

                case 14: researchController.control16And17Levels(); break;

                case 15: researchController.level18Control(); break;

                case 16: researchController.level18Control(); break;

                case 17: researchController.level18Control(); break;

                default: break;
            }

            
        }
    }
        //Bina Yükseltmesi Aktifse Yapma
        //Deðilse Yap
        //Baþka Bir Yükseltme Aktifse Yapma
        //Deðilse Yap

        
    }

