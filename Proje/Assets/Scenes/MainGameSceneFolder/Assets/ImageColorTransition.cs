using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorTransition : MonoBehaviour
{
    public Image[] seviyeImages; // T�m seviyeler i�in Image nesnelerini tutar
    public float duration = 120f; // Ge�i� s�resi (�rnek: 2 dakika)

    public ProgressBarController progressBarController;
    public Button createLabButton;


    private Material[] uniqueMaterials; // Her Image i�in ayr� materyaller
    public ResearchController researchController;
    void Start()
    {
        // Her Image i�in benzersiz materyal olu�tur ve ba�lang��ta renksiz hale getir
        uniqueMaterials = new Material[seviyeImages.Length];
        for (int i = 0; i < seviyeImages.Length; i++)
        {
            if (seviyeImages[i] != null)
            {
                uniqueMaterials[i] = new Material(seviyeImages[i].material); // Orijinal materyalin bir kopyas�n� al
                seviyeImages[i].material = uniqueMaterials[i]; // Image'e bu kopyay� ata
                uniqueMaterials[i].SetFloat("_FillAmount", 0f); // Ba�lang��ta tamamen renksiz yap
            }
        }
    }

    public void StartColorTransitionSeviye1()
    {
        if (seviyeImages.Length >= 1 && seviyeImages[0] != null)
            StartCoroutine(ColorTransition(seviyeImages[0], 0));
        else
            Debug.LogError("Seviye 1 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye2()
    {
        if (seviyeImages.Length >= 2 && seviyeImages[1] != null)
            StartCoroutine(ColorTransition(seviyeImages[1], 1));
        else
            Debug.LogError("Seviye 2 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye3()
    {
        if (seviyeImages.Length >= 3 && seviyeImages[2] != null)
            StartCoroutine(ColorTransition(seviyeImages[2], 2));
        else
            Debug.LogError("Seviye 3 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye4()
    {
        if (seviyeImages.Length >= 4 && seviyeImages[3] != null)
            StartCoroutine(ColorTransition(seviyeImages[3], 3));
        else
            Debug.LogError("Seviye 4 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye5()
    {
        if (seviyeImages.Length >= 5 && seviyeImages[4] != null)
            StartCoroutine(ColorTransition(seviyeImages[4], 4));
        else
            Debug.LogError("Seviye 5 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye6()
    {
        if (seviyeImages.Length >= 6 && seviyeImages[5] != null)
            StartCoroutine(ColorTransition(seviyeImages[5], 5));
        else
            Debug.LogError("Seviye 6 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye7()
    {
        if (seviyeImages.Length >= 7 && seviyeImages[6] != null)
            StartCoroutine(ColorTransition(seviyeImages[6], 6));
        else
            Debug.LogError("Seviye 7 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye8()
    {
        if (seviyeImages.Length >= 8 && seviyeImages[7] != null)
            StartCoroutine(ColorTransition(seviyeImages[7], 7));
        else
            Debug.LogError("Seviye 8 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye9()
    {
        if (seviyeImages.Length >= 9 && seviyeImages[8] != null)
            StartCoroutine(ColorTransition(seviyeImages[8], 8));
        else
            Debug.LogError("Seviye 9 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye10()
    {
        if (seviyeImages.Length >= 10 && seviyeImages[9] != null)
            StartCoroutine(ColorTransition(seviyeImages[9], 9));
        else
            Debug.LogError("Seviye 10 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye11()
    {
        if (seviyeImages.Length >= 11 && seviyeImages[10] != null)
            StartCoroutine(ColorTransition(seviyeImages[10], 10));
        else
            Debug.LogError("Seviye 11 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye12()
    {
        if (seviyeImages.Length >= 12 && seviyeImages[11] != null)
            StartCoroutine(ColorTransition(seviyeImages[11], 11));
        else
            Debug.LogError("Seviye 12 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye13()
    {
        if (seviyeImages.Length >= 13 && seviyeImages[12] != null)
            StartCoroutine(ColorTransition(seviyeImages[12], 12));
        else
            Debug.LogError("Seviye 13 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye14()
    {
        if (seviyeImages.Length >= 14 && seviyeImages[13] != null)
            StartCoroutine(ColorTransition(seviyeImages[13], 13));
        else
            Debug.LogError("Seviye 14 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye15()
    {
        if (seviyeImages.Length >= 15 && seviyeImages[14] != null)
            StartCoroutine(ColorTransition(seviyeImages[14], 14));
        else
            Debug.LogError("Seviye 15 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye16()
    {
        if (seviyeImages.Length >= 16 && seviyeImages[15] != null)
            StartCoroutine(ColorTransition(seviyeImages[15], 15));
        else
            Debug.LogError("Seviye 16 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye17()
    {
        if (seviyeImages.Length >= 17 && seviyeImages[16] != null)
            StartCoroutine(ColorTransition(seviyeImages[16], 16));
        else
            Debug.LogError("Seviye 17 i�in ge�erli bir Image atanmad�.");
    }

    public void StartColorTransitionSeviye18()
    {
        if (seviyeImages.Length >= 18 && seviyeImages[17] != null)
            StartCoroutine(ColorTransition(seviyeImages[17], 17));
        else
            Debug.LogError("Seviye 18 i�in ge�erli bir Image atanmad�.");
    }




    private IEnumerator ColorTransition(Image targetImage , int researchLevel)
    {
        if(progressBarController.isLabBuildActive || ResearchButtonEvents.isAnyResearchActive)
        {
            Debug.Log("Bina Y�ksektmesi veya bir ara�t�rman�n halihaz�rda aktif olmas� durumunda ara�t�rma yapamazs�n�z.");
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
                float progress = elapsedTime / duration; // 0 ile 1 aras�nda ilerleme
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
        //Bina Y�kseltmesi Aktifse Yapma
        //De�ilse Yap
        //Ba�ka Bir Y�kseltme Aktifse Yapma
        //De�ilse Yap

        
    }

