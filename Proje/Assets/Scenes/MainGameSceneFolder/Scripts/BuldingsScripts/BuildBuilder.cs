using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class BuildBuilder : MonoBehaviour
{
    public ResearchController researchController;
    public Button buildButton;
    private Text buttonText;
    public ProgressBarController progressBarController;

    public WareHousePanelController wareHousePanelController;
    public StonepitPanelController stonepitPanelController;
    public SawmillPanelController sawmillPanelController;
    public FarmPanelController farmPanelController;
    public BlacksmithPanelController blacksmithPanelController;
    public LabPanelController labPanelController;
    public BarracksPanelController barracksPanelController;
    public HospitalPanelController hospitalPanelController;
    public CastlePanelController castlePanelController;

    public static bool checkResources(Building building) // Art�k Building t�r� kabul ediliyor
    {
        // G�ncel maliyetleri kontrol edin
        building.UpdateCosts();

        if ((building.buildGoldCost > Kingdom.myKingdom.GoldAmount) ||
            (building.buildStoneCost > Kingdom.myKingdom.StoneAmount) ||
            (building.buildTimberCost > Kingdom.myKingdom.WoodAmount) ||
            (building.buildIronCost > Kingdom.myKingdom.IronAmount) ||
            (building.buildFoodCost > Kingdom.myKingdom.FoodAmount))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void BuildStonePit()
    {
        // Zaten var olan ta� oca�� nesnesini kullanmak i�in kontrol edin
        StonePit stonePit = GetComponent<StonePit>();

        if (!StonePit.wasStonePitCreated)
        {
            stonePit = gameObject.AddComponent<StonePit>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(stonePit))
            {
                // Kaynaklar� azalt�n
                Kingdom.myKingdom.GoldAmount -= stonePit.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= stonePit.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= stonePit.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= stonePit.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= stonePit.buildFoodCost;

                buildButton.enabled = false;

                StartCoroutine(progressBarController.StonePitIsFinished(stonePit, (isFinished) =>
                {
                    if (isFinished)
                    {
                        StonePit.wasStonePitCreated = true;
                        StonePit.canIStartProduction = true;
                        StonePit.buildLevel = 1;
                        StonePit.refreshStoneProductionRate();
                        stonePit.UpdateCosts(); // Maliyetleri g�ncelle

                        Debug.Log("Bina Seviyesi : " + StonePit.buildLevel);
                        buttonText.text = "Y�kselt";
                        buildButton.enabled = true;
                        stonepitPanelController.refreshStonePit();
                    }
                    else
                    {
                        // Kaynaklar� iade et
                        Kingdom.myKingdom.GoldAmount += stonePit.buildGoldCost;
                        Kingdom.myKingdom.StoneAmount += stonePit.buildStoneCost;
                        Kingdom.myKingdom.WoodAmount += stonePit.buildTimberCost;
                        Kingdom.myKingdom.IronAmount += stonePit.buildIronCost;
                        Kingdom.myKingdom.FoodAmount += stonePit.buildFoodCost;
                        buildButton.enabled = true;
                    }
                }));
            }
            else
            {
                Debug.Log("Yeterli kaynak bulunmamaktad�r");
            }
        }
        else
        {

            // Zaten bir ta� oca�� varsa, yeni bir nesne yaratmay�n
            if (StonePit.buildLevel == 1)
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

                if (checkResources(stonePit))
                {
                    // Kaynaklar� azalt�n
                    Kingdom.myKingdom.GoldAmount -= stonePit.buildGoldCost;
                    Kingdom.myKingdom.StoneAmount -= stonePit.buildStoneCost;
                    Kingdom.myKingdom.WoodAmount -= stonePit.buildTimberCost;
                    Kingdom.myKingdom.IronAmount -= stonePit.buildIronCost;
                    Kingdom.myKingdom.FoodAmount -= stonePit.buildFoodCost;

                    buildButton.enabled = false;

                    StartCoroutine(progressBarController.StonePitIsFinished(stonePit, (isFinished) =>
                    {
                        if (isFinished)
                        {
                            // Gerekli i�lemleri yap

                            StonePit.buildLevel++;
                            StonePit.refreshStoneProductionRate(); // �retim miktar�n� g�ncelliyoruz.
                            stonePit.UpdateCosts(); // Maliyetleri g�ncelle
                            buttonText.text = "Y�kselt";
                            buildButton.enabled = true;
                            stonepitPanelController.refreshStonePit();
                        }
                        else
                        {
                            // Kaynaklar� iade et
                            Kingdom.myKingdom.GoldAmount += stonePit.buildGoldCost;
                            Kingdom.myKingdom.StoneAmount += stonePit.buildStoneCost;
                            Kingdom.myKingdom.WoodAmount += stonePit.buildTimberCost;
                            Kingdom.myKingdom.IronAmount += stonePit.buildIronCost;
                            Kingdom.myKingdom.FoodAmount += stonePit.buildFoodCost;
                            buildButton.enabled = true;
                        }
                    }));
                }
                else
                {
                    Debug.Log("Yeterli kaynak bulunmamaktad�r");
                }
            }

            else if (StonePit.buildLevel == 2)
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

                if (checkResources(stonePit))
                {

                    Kingdom.myKingdom.GoldAmount -= stonePit.buildGoldCost;
                    Kingdom.myKingdom.StoneAmount -= stonePit.buildStoneCost;
                    Kingdom.myKingdom.WoodAmount -= stonePit.buildTimberCost;
                    Kingdom.myKingdom.IronAmount -= stonePit.buildIronCost;
                    Kingdom.myKingdom.FoodAmount -= stonePit.buildFoodCost;

                    buildButton.enabled = false;

                    StartCoroutine(progressBarController.StonePitIsFinished(stonePit, (isFinished) =>
                    {
                        if (isFinished)
                        {
                            // Gerekli i�lemleri yap

                            StonePit.buildLevel++;
                            StonePit.refreshStoneProductionRate(); // �retim miktar�n� g�ncelliyoruz.                  
                            stonepitPanelController.refreshStonePit();
                            Destroy(buildButton.gameObject);
                        }
                        else
                        {
                            // Kaynaklar� iade et
                            Kingdom.myKingdom.GoldAmount += stonePit.buildGoldCost;
                            Kingdom.myKingdom.StoneAmount += stonePit.buildStoneCost;
                            Kingdom.myKingdom.WoodAmount += stonePit.buildTimberCost;
                            Kingdom.myKingdom.IronAmount += stonePit.buildIronCost;
                            Kingdom.myKingdom.FoodAmount += stonePit.buildFoodCost;
                            buildButton.enabled = true;
                        }
                    }));
                }

            }
            else
            {
                Debug.Log("Bir sorun var gibi duruyor 'BuildBuilder' scriptindeki buildStonePit fonksiyonunu kontrol ediniz.");
            }
        }

    }

    public void BuildBlacksmith()
    {
        // Zaten var olan demirci nesnesini kullanmak i�in kontrol edin
        Blacksmith blacksmith = GetComponent<Blacksmith>();

        if (!Blacksmith.wasBlacksmithCreated)
        {
            blacksmith = gameObject.AddComponent<Blacksmith>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(blacksmith))
            {
                // Kaynaklar� azalt�n
                Kingdom.myKingdom.GoldAmount -= blacksmith.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= blacksmith.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= blacksmith.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= blacksmith.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= blacksmith.buildFoodCost;

                buildButton.enabled = false;

                StartCoroutine(progressBarController.BlacksmithIsFinished(blacksmith, (isFinished) =>
                {
                    if (isFinished)
                    {
                        Blacksmith.wasBlacksmithCreated = true;
                        Blacksmith.canIStartProduction = true;
                        Blacksmith.buildLevel = 1;
                        Blacksmith.refreshIronProductionRate();
                        blacksmith.UpdateCosts(); // Maliyetleri g�ncelle

                        Debug.Log("Bina Seviyesi : " + Blacksmith.buildLevel);
                        buttonText.text = "Y�kselt";
                        buildButton.enabled = true;
                        blacksmithPanelController.refreshBlacksmith();
                    }
                    else
                    {
                        // Kaynaklar� iade et
                        Kingdom.myKingdom.GoldAmount += blacksmith.buildGoldCost;
                        Kingdom.myKingdom.StoneAmount += blacksmith.buildStoneCost;
                        Kingdom.myKingdom.WoodAmount += blacksmith.buildTimberCost;
                        Kingdom.myKingdom.IronAmount += blacksmith.buildIronCost;
                        Kingdom.myKingdom.FoodAmount += blacksmith.buildFoodCost;
                        buildButton.enabled = true;
                    }
                }));
            }
            else
            {
                Debug.Log("Yeterli kaynak bulunmamaktad�r");
            }
        }
        else
        {
            // Zaten bir demirci varsa, yeni bir nesne yaratmay�n
            if (Blacksmith.buildLevel == 1)
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

                if (checkResources(blacksmith))
                {
                    // Kaynaklar� azalt�n
                    Kingdom.myKingdom.GoldAmount -= blacksmith.buildGoldCost;
                    Kingdom.myKingdom.StoneAmount -= blacksmith.buildStoneCost;
                    Kingdom.myKingdom.WoodAmount -= blacksmith.buildTimberCost;
                    Kingdom.myKingdom.IronAmount -= blacksmith.buildIronCost;
                    Kingdom.myKingdom.FoodAmount -= blacksmith.buildFoodCost;

                    buildButton.enabled = false;

                    StartCoroutine(progressBarController.BlacksmithIsFinished(blacksmith, (isFinished) =>
                    {
                        if (isFinished)
                        {
                            // Gerekli i�lemleri yap

                            Blacksmith.buildLevel++;
                            Blacksmith.refreshIronProductionRate(); // �retim miktar�n� g�ncelliyoruz.
                            blacksmith.UpdateCosts(); // Maliyetleri g�ncelle
                            buttonText.text = "Y�kselt";
                            buildButton.enabled = true;
                            blacksmithPanelController.refreshBlacksmith();
                        }
                        else
                        {
                            // Kaynaklar� iade et
                            Kingdom.myKingdom.GoldAmount += blacksmith.buildGoldCost;
                            Kingdom.myKingdom.StoneAmount += blacksmith.buildStoneCost;
                            Kingdom.myKingdom.WoodAmount += blacksmith.buildTimberCost;
                            Kingdom.myKingdom.IronAmount += blacksmith.buildIronCost;
                            Kingdom.myKingdom.FoodAmount += blacksmith.buildFoodCost;
                            buildButton.enabled = true;
                        }
                    }));
                }
                else
                {
                    Debug.Log("Yeterli kaynak bulunmamaktad�r");
                }
            }

            else if (Blacksmith.buildLevel == 2)
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

                if (checkResources(blacksmith))
                {
                    Kingdom.myKingdom.GoldAmount -= blacksmith.buildGoldCost;
                    Kingdom.myKingdom.StoneAmount -= blacksmith.buildStoneCost;
                    Kingdom.myKingdom.WoodAmount -= blacksmith.buildTimberCost;
                    Kingdom.myKingdom.IronAmount -= blacksmith.buildIronCost;
                    Kingdom.myKingdom.FoodAmount -= blacksmith.buildFoodCost;

                    buildButton.enabled = false;

                    StartCoroutine(progressBarController.BlacksmithIsFinished(blacksmith, (isFinished) =>
                    {
                        if (isFinished)
                        {
                            // Gerekli i�lemleri yap

                            Blacksmith.buildLevel++;
                            Blacksmith.refreshIronProductionRate(); // �retim miktar�n� g�ncelliyoruz.                   
                            blacksmithPanelController.refreshBlacksmith();
                            Destroy(buildButton.gameObject);
                        }
                        else
                        {
                            // Kaynaklar� iade et
                            Kingdom.myKingdom.GoldAmount += blacksmith.buildGoldCost;
                            Kingdom.myKingdom.StoneAmount += blacksmith.buildStoneCost;
                            Kingdom.myKingdom.WoodAmount += blacksmith.buildTimberCost;
                            Kingdom.myKingdom.IronAmount += blacksmith.buildIronCost;
                            Kingdom.myKingdom.FoodAmount += blacksmith.buildFoodCost;
                            buildButton.enabled = true;
                        }
                    }));
                }
            }
            else
            {
                Debug.Log("Bir sorun var gibi duruyor 'BuildBuilder' scriptindeki BuildBlacksmith fonksiyonunu kontrol ediniz.");
            }
        }
    }


    public void BuildSawmill()
    {
        // Zaten var olan kereste oca�� nesnesini kullanmak i�in kontrol edin
        Sawmill sawmill = GetComponent<Sawmill>();

        if (!Sawmill.wasSawmillCreated)
        {
            sawmill = gameObject.AddComponent<Sawmill>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(sawmill))
            {
                // Kaynaklar� azalt�n
                Kingdom.myKingdom.GoldAmount -= sawmill.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= sawmill.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= sawmill.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= sawmill.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= sawmill.buildFoodCost;

                buildButton.enabled = false;

                StartCoroutine(progressBarController.SawmillIsFinished(sawmill, (isFinished) =>
                {
                    if (isFinished)
                    {
                        Sawmill.wasSawmillCreated = true;
                        Sawmill.canIStartProduction = true;
                        Sawmill.buildLevel = 1;
                        Sawmill.refreshTimberProductionRate();
                        sawmill.UpdateCosts(); // Maliyetleri g�ncelle

                        Debug.Log("Bina Seviyesi : " + Sawmill.buildLevel);
                        buttonText.text = "Y�kselt";
                        buildButton.enabled = true;
                        sawmillPanelController.refreshSawmill();
                    }
                    else
                    {
                        // Kaynaklar� iade et
                        Kingdom.myKingdom.GoldAmount += sawmill.buildGoldCost;
                        Kingdom.myKingdom.StoneAmount += sawmill.buildStoneCost;
                        Kingdom.myKingdom.WoodAmount += sawmill.buildTimberCost;
                        Kingdom.myKingdom.IronAmount += sawmill.buildIronCost;
                        Kingdom.myKingdom.FoodAmount += sawmill.buildFoodCost;
                        buildButton.enabled = true;
                    }
                }));
            }
            else
            {
                Debug.Log("Yeterli kaynak bulunmamaktad�r");
            }
        }
        else
        {
            // Zaten bir kereste oca�� varsa, yeni bir nesne yaratmay�n
            if (Sawmill.buildLevel == 1)
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

                if (checkResources(sawmill))
                {
                    // Kaynaklar� azalt�n
                    Kingdom.myKingdom.GoldAmount -= sawmill.buildGoldCost;
                    Kingdom.myKingdom.StoneAmount -= sawmill.buildStoneCost;
                    Kingdom.myKingdom.WoodAmount -= sawmill.buildTimberCost;
                    Kingdom.myKingdom.IronAmount -= sawmill.buildIronCost;
                    Kingdom.myKingdom.FoodAmount -= sawmill.buildFoodCost;

                    buildButton.enabled = false;

                    StartCoroutine(progressBarController.SawmillIsFinished(sawmill, (isFinished) =>
                    {
                        if (isFinished)
                        {
                            // Gerekli i�lemleri yap

                            Sawmill.buildLevel++;
                            Sawmill.refreshTimberProductionRate(); // �retim miktar�n� g�ncelliyoruz.
                            sawmill.UpdateCosts(); // Maliyetleri g�ncelle
                            buttonText.text = "Y�kselt";
                            buildButton.enabled = true;
                            sawmillPanelController.refreshSawmill();
                        }
                        else
                        {
                            // Kaynaklar� iade et
                            Kingdom.myKingdom.GoldAmount += sawmill.buildGoldCost;
                            Kingdom.myKingdom.StoneAmount += sawmill.buildStoneCost;
                            Kingdom.myKingdom.WoodAmount += sawmill.buildTimberCost;
                            Kingdom.myKingdom.IronAmount += sawmill.buildIronCost;
                            Kingdom.myKingdom.FoodAmount += sawmill.buildFoodCost;
                            buildButton.enabled = true;
                        }
                    }));
                }
                else
                {
                    Debug.Log("Yeterli kaynak bulunmamaktad�r");
                }
            }

            else if (Sawmill.buildLevel == 2)
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

                if (checkResources(sawmill))
                {

                    Kingdom.myKingdom.GoldAmount -= sawmill.buildGoldCost;
                    Kingdom.myKingdom.StoneAmount -= sawmill.buildStoneCost;
                    Kingdom.myKingdom.WoodAmount -= sawmill.buildTimberCost;
                    Kingdom.myKingdom.IronAmount -= sawmill.buildIronCost;
                    Kingdom.myKingdom.FoodAmount -= sawmill.buildFoodCost;

                    buildButton.enabled = false;

                    StartCoroutine(progressBarController.SawmillIsFinished(sawmill, (isFinished) =>
                    {
                        if (isFinished)
                        {
                            // Gerekli i�lemleri yap

                            Sawmill.buildLevel++;
                            Sawmill.refreshTimberProductionRate(); // �retim miktar�n� g�ncelliyoruz.                  
                            sawmillPanelController.refreshSawmill();
                            Destroy(buildButton.gameObject);
                        }
                        else
                        {
                            // Kaynaklar� iade et
                            Kingdom.myKingdom.GoldAmount += sawmill.buildGoldCost;
                            Kingdom.myKingdom.StoneAmount += sawmill.buildStoneCost;
                            Kingdom.myKingdom.WoodAmount += sawmill.buildTimberCost;
                            Kingdom.myKingdom.IronAmount += sawmill.buildIronCost;
                            Kingdom.myKingdom.FoodAmount += sawmill.buildFoodCost;
                            buildButton.enabled = true;
                        }
                    }));
                }
            }
            else
            {
                Debug.Log("Bir sorun var gibi duruyor 'BuildBuilder' scriptindeki buildSawmill fonksiyonunu kontrol ediniz.");
            }
        }
    }


    public void BuildFarm()
    {
        // Zaten var olan �iftlik nesnesini kullanmak i�in kontrol edin
        Farm farm = GetComponent<Farm>();

        if (!Farm.wasFarmCreated)
        {
            farm = gameObject.AddComponent<Farm>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(farm))
            {
                // Kaynaklar� azalt�n
                Kingdom.myKingdom.GoldAmount -= farm.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= farm.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= farm.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= farm.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= farm.buildFoodCost;

                buildButton.enabled = false;

                StartCoroutine(progressBarController.FarmIsFinished(farm, (isFinished) =>
                {
                    if (isFinished)
                    {
                        Farm.wasFarmCreated = true;
                        Farm.canIStartProduction = true;
                        Farm.buildLevel = 1;
                        Farm.refreshFoodProductionRate();
                        farm.UpdateCosts(); // Maliyetleri g�ncelle

                        Debug.Log("Bina Seviyesi : " + Farm.buildLevel);
                        buttonText.text = "Y�kselt";
                        buildButton.enabled = true;
                        farmPanelController.refreshFarm();
                    }
                    else
                    {
                        // Kaynaklar� iade et
                        Kingdom.myKingdom.GoldAmount += farm.buildGoldCost;
                        Kingdom.myKingdom.StoneAmount += farm.buildStoneCost;
                        Kingdom.myKingdom.WoodAmount += farm.buildTimberCost;
                        Kingdom.myKingdom.IronAmount += farm.buildIronCost;
                        Kingdom.myKingdom.FoodAmount += farm.buildFoodCost;
                        buildButton.enabled = true;
                    }
                }));
            }
            else
            {
                Debug.Log("Yeterli kaynak bulunmamaktad�r");
            }
        }
        else
        {
            // Zaten bir �iftlik varsa, yeni bir nesne yaratmay�n
            if (Farm.buildLevel == 1)
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

                if (checkResources(farm))
                {
                    // Kaynaklar� azalt�n
                    Kingdom.myKingdom.GoldAmount -= farm.buildGoldCost;
                    Kingdom.myKingdom.StoneAmount -= farm.buildStoneCost;
                    Kingdom.myKingdom.WoodAmount -= farm.buildTimberCost;
                    Kingdom.myKingdom.IronAmount -= farm.buildIronCost;
                    Kingdom.myKingdom.FoodAmount -= farm.buildFoodCost;

                    buildButton.enabled = false;

                    StartCoroutine(progressBarController.FarmIsFinished(farm, (isFinished) =>
                    {
                        if (isFinished)
                        {
                            // Gerekli i�lemleri yap

                            Farm.buildLevel++;
                            Farm.refreshFoodProductionRate(); // �retim miktar�n� g�ncelliyoruz.
                            farm.UpdateCosts(); // Maliyetleri g�ncelle
                            buttonText.text = "Y�kselt";
                            buildButton.enabled = true;
                            farmPanelController.refreshFarm();
                        }
                        else
                        {
                            // Kaynaklar� iade et
                            Kingdom.myKingdom.GoldAmount += farm.buildGoldCost;
                            Kingdom.myKingdom.StoneAmount += farm.buildStoneCost;
                            Kingdom.myKingdom.WoodAmount += farm.buildTimberCost;
                            Kingdom.myKingdom.IronAmount += farm.buildIronCost;
                            Kingdom.myKingdom.FoodAmount += farm.buildFoodCost;
                            buildButton.enabled = true;
                        }
                    }));
                }
                else
                {
                    Debug.Log("Yeterli kaynak bulunmamaktad�r");
                }
            }

            else if (Farm.buildLevel == 2)
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

                if (checkResources(farm))
                {
                    Kingdom.myKingdom.GoldAmount -= farm.buildGoldCost;
                    Kingdom.myKingdom.StoneAmount -= farm.buildStoneCost;
                    Kingdom.myKingdom.WoodAmount -= farm.buildTimberCost;
                    Kingdom.myKingdom.IronAmount -= farm.buildIronCost;
                    Kingdom.myKingdom.FoodAmount -= farm.buildFoodCost;

                    buildButton.enabled = false;

                    StartCoroutine(progressBarController.FarmIsFinished(farm, (isFinished) =>
                    {
                        if (isFinished)
                        {
                            // Gerekli i�lemleri yap

                            Farm.buildLevel++;
                            Farm.refreshFoodProductionRate(); // �retim miktar�n� g�ncelliyoruz.                  
                            farmPanelController.refreshFarm();
                            Destroy(buildButton.gameObject);
                        }
                        else
                        {
                            // Kaynaklar� iade et
                            Kingdom.myKingdom.GoldAmount += farm.buildGoldCost;
                            Kingdom.myKingdom.StoneAmount += farm.buildStoneCost;
                            Kingdom.myKingdom.WoodAmount += farm.buildTimberCost;
                            Kingdom.myKingdom.IronAmount += farm.buildIronCost;
                            Kingdom.myKingdom.FoodAmount += farm.buildFoodCost;
                            buildButton.enabled = true;
                        }
                    }));
                }
            }
            else
            {
                Debug.Log("Bir sorun var gibi duruyor 'BuildBuilder' scriptindeki BuildFarm fonksiyonunu kontrol ediniz.");
            }
        }
    }


    public void BuildBarracks()
    {
        // Zaten var olan k��la nesnesini kullanmak i�in kontrol edin
        Barracks barracks = GetComponent<Barracks>();

        if (!Barracks.wasBarracksCreated)
        {
            barracks = gameObject.AddComponent<Barracks>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(barracks) && Sawmill.buildLevel >= 1 && Farm.buildLevel >= 2 && Blacksmith.buildLevel >= 1)
            {                
                //Kaynaklar� Azalt
                Kingdom.myKingdom.GoldAmount -= barracks.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= barracks.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= barracks.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= barracks.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= barracks.buildFoodCost;

                buildButton.enabled = false;


                StartCoroutine(progressBarController.BarracksIsFinished(barracks, (isFinished) =>
                {
                    if (isFinished)
                    {
                        // Gerekli i�lemleri yap


                        Barracks.wasBarracksCreated = true;
                        Barracks.buildLevel = 1;
                        barracks.UpdateCosts(); // Maliyetleri g�ncelle
                        Debug.Log("Bina Seviyesi : " + Barracks.buildLevel);
                        buttonText.text = "Y�kselt";
                        buildButton.enabled = true;
                        barracksPanelController.refreshBarracks();
                    }
                    else
                    {
                        // Kaynaklar� iade et
                        Kingdom.myKingdom.GoldAmount += barracks.buildGoldCost;
                        Kingdom.myKingdom.StoneAmount += barracks.buildStoneCost;
                        Kingdom.myKingdom.WoodAmount += barracks.buildTimberCost;
                        Kingdom.myKingdom.IronAmount += barracks.buildIronCost;
                        Kingdom.myKingdom.FoodAmount += barracks.buildFoodCost;
                        buildButton.enabled = true;
                    }
                }));

            }
            else
            {
                Debug.Log("Binay� olu�turmak i�in gerekli gereksinimleri sa�lam�yorsunuz.");
            }
        }
        else
        {
            if (Barracks.buildLevel == 1)
            {
                if (checkResources(barracks) && Sawmill.buildLevel >= 2 && Farm.buildLevel >= 3 && Blacksmith.buildLevel >= 2)
                {
                    //E�er asker �retimi varsa buraya girme -----> Asker �retimi yaparken geli�tirilemez.
                    if (progressBarController.isUnitCreationActive)
                    {
                        Debug.Log("Asker �retimi S�ras�nda Bina Y�kseltmesi Yap�lamaz.");
                    }
                    //yoksa gir.
                    else
                    {
                        //Kaynaklar� Azalt
                        Kingdom.myKingdom.GoldAmount -= barracks.buildGoldCost;
                        Kingdom.myKingdom.StoneAmount -= barracks.buildStoneCost;
                        Kingdom.myKingdom.WoodAmount -= barracks.buildTimberCost;
                        Kingdom.myKingdom.IronAmount -= barracks.buildIronCost;
                        Kingdom.myKingdom.FoodAmount -= barracks.buildFoodCost;

                        buildButton.enabled = false;


                        StartCoroutine(progressBarController.BarracksIsFinished(barracks, (isFinished) =>
                        {
                            if (isFinished)
                            {
                                // Gerekli i�lemleri yap


                                Barracks.wasBarracksCreated = true;
                                Barracks.buildLevel++;
                                barracks.UpdateCosts(); // Maliyetleri g�ncelle
                                Debug.Log("Bina Seviyesi : " + Barracks.buildLevel);
                                buildButton.enabled = true;
                                barracksPanelController.refreshBarracks();
                            }
                            else
                            {
                                // Kaynaklar� iade et
                                Kingdom.myKingdom.GoldAmount += barracks.buildGoldCost;
                                Kingdom.myKingdom.StoneAmount += barracks.buildStoneCost;
                                Kingdom.myKingdom.WoodAmount += barracks.buildTimberCost;
                                Kingdom.myKingdom.IronAmount += barracks.buildIronCost;
                                Kingdom.myKingdom.FoodAmount += barracks.buildFoodCost;
                                buildButton.enabled = true;
                            }
                        }));
                    }     
                }
                else
                {
                    Debug.Log("Binay� olu�turmak i�in gerekli gereksinimleri sa�lam�yorsunuz.");
                }
            }

            else if (Barracks.buildLevel == 2)
            {
                if (checkResources(barracks) && Sawmill.buildLevel >= 3 && Farm.buildLevel >= 3 && Blacksmith.buildLevel >= 3)
                {
                    //Asker �retimi varsa buraya girme.             
                    if (progressBarController.isUnitCreationActive)
                    {
                        Debug.Log("Asker �retimi S�ras�nda Bina Y�kseltmesi Yap�lamaz.");
                    }
                    //yoksa gir.
                    else
                    {
                        //Kaynaklar� Azalt
                        Kingdom.myKingdom.GoldAmount -= barracks.buildGoldCost;
                        Kingdom.myKingdom.StoneAmount -= barracks.buildStoneCost;
                        Kingdom.myKingdom.WoodAmount -= barracks.buildTimberCost;
                        Kingdom.myKingdom.IronAmount -= barracks.buildIronCost;
                        Kingdom.myKingdom.FoodAmount -= barracks.buildFoodCost;

                        buildButton.enabled = false;


                        StartCoroutine(progressBarController.BarracksIsFinished(barracks, (isFinished) =>
                        {
                            if (isFinished)
                            {
                                // Gerekli i�lemleri yap


                                Barracks.buildLevel++;
                                barracks.UpdateCosts(); // Maliyetleri g�ncelle
                                Debug.Log("Bina Seviyesi : " + Barracks.buildLevel);
                                Destroy(buildButton.gameObject);
                                barracksPanelController.refreshBarracks();
                            }
                            else
                            {
                                // Kaynaklar� iade et
                                Kingdom.myKingdom.GoldAmount += barracks.buildGoldCost;
                                Kingdom.myKingdom.StoneAmount += barracks.buildStoneCost;
                                Kingdom.myKingdom.WoodAmount += barracks.buildTimberCost;
                                Kingdom.myKingdom.IronAmount += barracks.buildIronCost;
                                Kingdom.myKingdom.FoodAmount += barracks.buildFoodCost;
                                buildButton.enabled = true;
                            }
                        }));
                    } 
                }
                else
                {
                    Debug.Log("Binay� olu�turmak i�in gerekli gereksinimleri sa�lam�yorsunuz.");
                }
            }
            else
            {
                Debug.Log("Bir sorun var gibi duruyor 'BuildBuilder' scriptindeki buildBarracks fonksiyonunu kontrol ediniz.");
            }
        }                       
    }



    public void BuildHospital()
    {
        // Zaten var olan hastane nesnesini kullanmak i�in kontrol edin
        Hospital hospital = GetComponent<Hospital>();

        if (!Hospital.wasHospitalCreated)
        {
            hospital = gameObject.AddComponent<Hospital>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(hospital))
            {
                buildButton.enabled = false;
                // Kaynaklar� azalt�n
                Kingdom.myKingdom.GoldAmount -= hospital.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= hospital.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= hospital.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= hospital.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= hospital.buildFoodCost;

                buildButton.enabled = false;

                StartCoroutine(progressBarController.HospitalIsFinished(hospital, (isFinished) =>
                {
                    if (isFinished)
                    {
                        // Gerekli i�lemleri yap

                        Hospital.wasHospitalCreated = true;
                        Hospital.buildLevel = 1;
                        hospital.UpdateCapasity();
                        Debug.Log("Bina Seviyesi : " + Hospital.buildLevel);
                        Debug.Log("Hastane Kapasitesi : " + Hospital.capasity);
                        hospital.UpdateCosts(); // Maliyetleri g�ncelle              
                        buttonText.text = "Y�kselt";
                        hospitalPanelController.refreshHospital();
                        buildButton.enabled = true;
                    }
                    else
                    {
                        // Kaynaklar� iade et
                        Kingdom.myKingdom.GoldAmount += hospital.buildGoldCost;
                        Kingdom.myKingdom.StoneAmount += hospital.buildStoneCost;
                        Kingdom.myKingdom.WoodAmount += hospital.buildTimberCost;
                        Kingdom.myKingdom.IronAmount += hospital.buildIronCost;
                        Kingdom.myKingdom.FoodAmount += hospital.buildFoodCost;
                        buildButton.enabled = true;
                    }
                }));

                
            }
            else
            {
                Debug.Log("Yeterli kaynak bulunmamaktad�r");
            }
        }
        else
        {
            if(Hospital.buildLevel == 1)
            {
                if (checkResources(hospital))
                {
                    TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();
                    if(progressBarController.isHealActive)
                    {
                        Debug.Log("�yile�tirme esnas�nda bina y�kseltmesi yap�lamaz.");
                    }
                    else
                    {
                        Kingdom.myKingdom.GoldAmount -= hospital.buildGoldCost;
                        Kingdom.myKingdom.StoneAmount -= hospital.buildStoneCost;
                        Kingdom.myKingdom.WoodAmount -= hospital.buildTimberCost;
                        Kingdom.myKingdom.IronAmount -= hospital.buildIronCost;
                        Kingdom.myKingdom.FoodAmount -= hospital.buildFoodCost;

                        buildButton.enabled = false;

                        StartCoroutine(progressBarController.HospitalIsFinished(hospital, (isFinished) =>
                        {
                            if (isFinished)
                            {
                                // Gerekli i�lemleri yap
                                Hospital.buildLevel++;
                                hospital.UpdateCapasity();
                                Debug.Log("Bina Seviyesi : " + Hospital.buildLevel);
                                Debug.Log("Hastane Kapasitesi : " + Hospital.capasity);
                                hospital.UpdateCosts(); // Maliyetleri g�ncelle
                                buttonText.text = "Y�kselt";
                                hospitalPanelController.refreshHospital();
                                buildButton.enabled = true;
                            }
                            else
                            {
                                // Kaynaklar� iade et
                                Kingdom.myKingdom.GoldAmount += hospital.buildGoldCost;
                                Kingdom.myKingdom.StoneAmount += hospital.buildStoneCost;
                                Kingdom.myKingdom.WoodAmount += hospital.buildTimberCost;
                                Kingdom.myKingdom.IronAmount += hospital.buildIronCost;
                                Kingdom.myKingdom.FoodAmount += hospital.buildFoodCost;
                                buildButton.enabled = true;
                            }
                        }));

                    }
                }
                else
                {
                    Debug.Log("Yeterli kaynak bulunmamaktad�r");
                }

            }

            else if(Hospital.buildLevel == 2)
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();
                // Kaynaklar� azalt�n


                if (checkResources(hospital))
                {
                   
                    if (progressBarController.isHealActive)
                    {
                        Debug.Log("�yile�tirme esnas�nda bina y�kseltmesi yap�lamaz.");
                    }
                    else
                    {
                        Kingdom.myKingdom.GoldAmount -= hospital.buildGoldCost;
                        Kingdom.myKingdom.StoneAmount -= hospital.buildStoneCost;
                        Kingdom.myKingdom.WoodAmount -= hospital.buildTimberCost;
                        Kingdom.myKingdom.IronAmount -= hospital.buildIronCost;
                        Kingdom.myKingdom.FoodAmount -= hospital.buildFoodCost;

                        buildButton.enabled = false;

                        StartCoroutine(progressBarController.HospitalIsFinished(hospital, (isFinished) =>
                        {
                            if (isFinished)
                            {
                                // Gerekli i�lemleri yap
                                Hospital.buildLevel++;
                                hospital.UpdateCapasity();
                                Destroy(buildButton.gameObject);
                                hospitalPanelController.refreshHospital();
                            }
                            else
                            {
                                // Kaynaklar� iade et
                                Kingdom.myKingdom.GoldAmount += hospital.buildGoldCost;
                                Kingdom.myKingdom.StoneAmount += hospital.buildStoneCost;
                                Kingdom.myKingdom.WoodAmount += hospital.buildTimberCost;
                                Kingdom.myKingdom.IronAmount += hospital.buildIronCost;
                                Kingdom.myKingdom.FoodAmount += hospital.buildFoodCost;
                                buildButton.enabled = true;
                            }
                        }));
                    }
                }
                else
                {
                    Debug.Log("Yeterli kaynak bulunmamaktad�r");
                }
            }

            else
            {
                Debug.Log("Bir sorun var gibi duruyor 'BuildBuilder' scriptindeki buildHospital fonksiyonunu kontrol ediniz.");
            }
        }

    }

    public void BuildLab()
    {
        Lab lab = gameObject.GetComponent<Lab>();

        if (Lab.wasLabCreated == false)//Daha �nce �retilmediyse
        {
            
            lab = gameObject.AddComponent<Lab>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(lab) && Sawmill.buildLevel >= 2) // Kaynaklar yeterliyse, keresteci seviye 2 ise
            {
                // 
                Kingdom.myKingdom.GoldAmount -= lab.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= lab.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= lab.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= lab.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= lab.buildFoodCost;


                buildButton.enabled = false;

                StartCoroutine(progressBarController.LabIsFinished(lab, (isFinished) =>
                {
                    if (isFinished)
                    {
                        // Gerekli i�lemleri yap
                        Lab.wasLabCreated = true;
                        Lab.buildLevel = 1;
                        //Ara�t�rma H�z�n� Artt�r
                        researchController.OpenResearchUnit(Lab.buildLevel);
                        lab.UpdateCosts();
                        buttonText.text = "Y�kselt";
                        buildButton.enabled = true;
                        labPanelController.refreshLab();
                    }
                    else
                    {
                        // Kaynaklar� iade et
                        Kingdom.myKingdom.GoldAmount += lab.buildGoldCost;
                        Kingdom.myKingdom.StoneAmount += lab.buildStoneCost;
                        Kingdom.myKingdom.WoodAmount += lab.buildTimberCost;
                        Kingdom.myKingdom.IronAmount += lab.buildIronCost;
                        Kingdom.myKingdom.FoodAmount += lab.buildFoodCost;
                        buildButton.enabled = true;
                    }
                }));                           
            }

            else
            {
                Debug.Log("Yeterli Kaynak Bulunmamaktad�r veya Keresteci 2.Seviye De�il.");
            }
        }
        else//Daha �nce �retildi ise
        {
            if(Lab.buildLevel == 1 & ResearchButtonEvents.isResearched[3] && ResearchButtonEvents.isResearched[4])//Lab 1.seviyeyse
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();
                if (checkResources(lab) )
                //&
                //Kaynaklar yeterliyse 3.ve 4. ara�t�rma yap�ld�ysa.
                {


                    Kingdom.myKingdom.GoldAmount -= lab.buildGoldCost;
                    Kingdom.myKingdom.StoneAmount -= lab.buildStoneCost;
                    Kingdom.myKingdom.WoodAmount -= lab.buildTimberCost;
                    Kingdom.myKingdom.IronAmount -= lab.buildIronCost;
                    Kingdom.myKingdom.FoodAmount -= lab.buildFoodCost;

                    buildButton.enabled = false;

                    StartCoroutine(progressBarController.LabIsFinished(lab, (isFinished) =>
                    {
                        if (isFinished)
                        {
                            // Gerekli i�lemleri yap                           
                            Lab.buildLevel++;
                            //Ara�t�rma H�z�n� Artt�r
                            researchController.controlBuildLevelTwoResearches();
                            lab.UpdateCosts();
                            buttonText.text = "Y�kselt";
                            buildButton.enabled = true;
                            labPanelController.refreshLab();
                        }
                        else
                        {
                            // Kaynaklar� iade et
                            Kingdom.myKingdom.GoldAmount += lab.buildGoldCost;
                            Kingdom.myKingdom.StoneAmount += lab.buildStoneCost;
                            Kingdom.myKingdom.WoodAmount += lab.buildTimberCost;
                            Kingdom.myKingdom.IronAmount += lab.buildIronCost;
                            Kingdom.myKingdom.FoodAmount += lab.buildFoodCost;
                            buildButton.enabled = true;
                        }
                    }));                   
                }
                else
                {
                    Debug.Log("L�tfen kaynaklar�n yeterli oldu�undan veya D�rt ve Be� numaral� ara�t�rmal� tamamlad���n�zdan emin olun!");
                }
            }

            else if(Lab.buildLevel ==2 )
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();
                if (checkResources(lab) && ResearchButtonEvents.isResearched[10] && ResearchButtonEvents.isResearched[11] && ResearchButtonEvents.isResearched[12] && Sawmill.buildLevel >= 3)
                //
                //Kaynak yeterliyse 11,12,13. ara�t�rmalar yap�ld�ysa ve keresteci seviye 3'se
                {


                    Kingdom.myKingdom.GoldAmount -= lab.buildGoldCost;
                    Kingdom.myKingdom.StoneAmount -= lab.buildStoneCost;
                    Kingdom.myKingdom.WoodAmount -= lab.buildTimberCost;
                    Kingdom.myKingdom.IronAmount -= lab.buildIronCost;
                    Kingdom.myKingdom.FoodAmount -= lab.buildFoodCost;

                    buildButton.enabled = false;

                    StartCoroutine(progressBarController.LabIsFinished(lab, (isFinished) =>
                    {
                        if (isFinished)
                        {
                            // Gerekli i�lemleri yap                           
                            Lab.buildLevel++;
                            //Ara�t�rma H�z�n� Artt�r
                            researchController.controlBuildLevelThreeResearches();
                            lab.UpdateCosts();
                            buttonText.text = "Y�kselt";
                            buildButton.enabled = true;
                            labPanelController.refreshLab();
                            Destroy(buildButton.gameObject);
                        }
                        else
                        {
                            // Kaynaklar� iade et
                            Kingdom.myKingdom.GoldAmount += lab.buildGoldCost;
                            Kingdom.myKingdom.StoneAmount += lab.buildStoneCost;
                            Kingdom.myKingdom.WoodAmount += lab.buildTimberCost;
                            Kingdom.myKingdom.IronAmount += lab.buildIronCost;
                            Kingdom.myKingdom.FoodAmount += lab.buildFoodCost;
                            buildButton.enabled = true;
                        }
                    }));
                }
                else
                {
                    Debug.Log("L�tfen kaynaklar�n yeterli oldu�undan, 11,12,13 numaral� ara�t�rmal� tamamlad���n�zdan ve Kerestecinizin 3.seviye oldu�undan emin olun!");
                }
            }

            else
            {
                Debug.Log("Bir sorun var gibi duruyor 'BuildBuilder' scriptindeki buildLab fonksiyonunu kontrol ediniz.");
            }
                
        }
    }
                      

public void BuildDefenseWorkshop()
    {
        // Zaten var olan savunma at�lyesi nesnesini kullanmak i�in kontrol edin
        DefenseWorkshop defenseWorkshop = GetComponent<DefenseWorkshop>();

        if (!DefenseWorkshop.wasDefenseWorkshopCreated)
        {
            defenseWorkshop = gameObject.AddComponent<DefenseWorkshop>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(defenseWorkshop))
            {
                // Kaynaklar� azalt�n
                Kingdom.myKingdom.GoldAmount -= defenseWorkshop.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= defenseWorkshop.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= defenseWorkshop.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= defenseWorkshop.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= defenseWorkshop.buildFoodCost;

                DefenseWorkshop.wasDefenseWorkshopCreated = true;
                DefenseWorkshop.buildLevel = 1;
                defenseWorkshop.UpdateCosts(); // Maliyetleri g�ncelle

                Debug.Log("Bina Seviyesi : " + DefenseWorkshop.buildLevel);
                buttonText.text = "Y�kselt";
            }
            else
            {
                Debug.Log("Yeterli kaynak bulunmamaktad�r");
            }
        }
        else
        {
            // Zaten bir savunma at�lyesi varsa, yeni bir nesne yaratmay�n
            if (checkResources(defenseWorkshop))
            {
                // Kaynaklar� azalt�n
                Kingdom.myKingdom.GoldAmount -= defenseWorkshop.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= defenseWorkshop.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= defenseWorkshop.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= defenseWorkshop.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= defenseWorkshop.buildFoodCost;

                DefenseWorkshop.buildLevel++;
                Debug.Log("Bina Seviyesi : " + DefenseWorkshop.buildLevel);
                defenseWorkshop.UpdateCosts(); // Maliyetleri g�ncelle

                // 3. seviyeye ula��ld���nda butonu yok et
                if (DefenseWorkshop.buildLevel == 3)
                {
                    Destroy(buildButton.gameObject);
                }
            }
            else
            {
                Debug.Log("Yeterli kaynak bulunmamaktad�r");
            }
        }

    }

    public void BuildSiegeWorkshop()
    {
        // Zaten var olan ku�atma at�lyesi nesnesini kullanmak i�in kontrol edin
        SiegeWorkshop siegeWorkshop = GetComponent<SiegeWorkshop>();

        if (!SiegeWorkshop.wasSiegeWorkshopCreated)
        {
            siegeWorkshop = gameObject.AddComponent<SiegeWorkshop>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(siegeWorkshop))
            {
                // Kaynaklar� azalt�n
                Kingdom.myKingdom.GoldAmount -= siegeWorkshop.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= siegeWorkshop.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= siegeWorkshop.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= siegeWorkshop.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= siegeWorkshop.buildFoodCost;

                SiegeWorkshop.wasSiegeWorkshopCreated = true;
                SiegeWorkshop.buildLevel = 1;
                siegeWorkshop.UpdateCosts(); // Maliyetleri g�ncelle

                Debug.Log("Bina Seviyesi : " + SiegeWorkshop.buildLevel);
                buttonText.text = "Y�kselt";
            }
            else
            {
                Debug.Log("Yeterli kaynak bulunmamaktad�r");
            }
        }
        else
        {
            // Zaten bir ku�atma at�lyesi varsa, yeni bir nesne yaratmay�n
            if (checkResources(siegeWorkshop))
            {
                // Kaynaklar� azalt�n
                Kingdom.myKingdom.GoldAmount -= siegeWorkshop.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= siegeWorkshop.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= siegeWorkshop.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= siegeWorkshop.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= siegeWorkshop.buildFoodCost;

                SiegeWorkshop.buildLevel++;
                Debug.Log("Bina Seviyesi : " + SiegeWorkshop.buildLevel);
                siegeWorkshop.UpdateCosts(); // Maliyetleri g�ncelle

                // 3. seviyeye ula��ld���nda butonu yok et
                if (SiegeWorkshop.buildLevel == 3)
                {
                    Destroy(buildButton.gameObject);
                }
            }
            else
            {
                Debug.Log("Yeterli kaynak bulunmamaktad�r");
            }
        }

    }

    public void BuildWarehouse()
    {// Zaten var olan k��la nesnesini kullanmak i�in kontrol edin
        Warehouse warehouse = GetComponent<Warehouse>();

        if (!Warehouse.wasWarehouseCreated)
        {
            warehouse = gameObject.AddComponent<Warehouse>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(warehouse) && Farm.buildLevel >= 1 && Sawmill.buildLevel >= 1 && StonePit.buildLevel >= 1 && Blacksmith.buildLevel >= 1)
            //
            {

                //Kaynaklar� Azalt
                Kingdom.myKingdom.GoldAmount -= warehouse.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= warehouse.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= warehouse.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= warehouse.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= warehouse.buildFoodCost;

                buildButton.enabled = false;

                
                StartCoroutine(progressBarController.WarehouseIsFinished(warehouse, (isFinished) =>
                {
                    if (isFinished)
                    {
                        // Gerekli i�lemleri yap
                        Warehouse.wasWarehouseCreated = true;
                        Warehouse.buildLevel = 1;
                        Warehouse.IncreaseCapacity();
                        warehouse.UpdateCosts();
                        buttonText.text = "Y�kselt";
                        buildButton.enabled = true;
                        wareHousePanelController.refreshWarehouse();
                    }
                    else
                    {
                        // Kaynaklar� iade et
                        Kingdom.myKingdom.GoldAmount += warehouse.buildGoldCost;
                        Kingdom.myKingdom.StoneAmount += warehouse.buildStoneCost;
                        Kingdom.myKingdom.WoodAmount += warehouse.buildTimberCost;
                        Kingdom.myKingdom.IronAmount += warehouse.buildIronCost;
                        Kingdom.myKingdom.FoodAmount += warehouse.buildFoodCost;
                        buildButton.enabled = true;
                    }
                }));
            }
            else
            {
                Debug.Log("Yeterli kaynak bulunmamaktad�r veya �iftlik,Demirci,Ta�Oca��,Keresteci binalar� en az birinci seviye olmal�d�r.");
            }
        }
        else
        {
            if(Warehouse.buildLevel == 1) 
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();
                if (checkResources(warehouse) && Sawmill.buildLevel >= 2 && Blacksmith.buildLevel >= 2 && Farm.buildLevel >= 2 && StonePit.buildLevel >= 2)
                //

                {

                    Kingdom.myKingdom.GoldAmount -= warehouse.buildGoldCost;
                    Kingdom.myKingdom.StoneAmount -= warehouse.buildStoneCost;
                    Kingdom.myKingdom.WoodAmount -= warehouse.buildTimberCost;
                    Kingdom.myKingdom.IronAmount -= warehouse.buildIronCost;
                    Kingdom.myKingdom.FoodAmount -= warehouse.buildFoodCost;

                    buildButton.enabled = false;

                    StartCoroutine(progressBarController.WarehouseIsFinished(warehouse, (isFinished) =>
                    {
                        if (isFinished)
                        {
                            // Gerekli i�lemleri yap

                            Warehouse.buildLevel++;
                            Warehouse.IncreaseCapacity();
                            warehouse.UpdateCosts();
                            buttonText.text = "Y�kselt";
                            buildButton.enabled = true;
                            wareHousePanelController.refreshWarehouse();
                        }
                        else
                        {
                            // Kaynaklar� iade et
                            Kingdom.myKingdom.GoldAmount += warehouse.buildGoldCost;
                            Kingdom.myKingdom.StoneAmount += warehouse.buildStoneCost;
                            Kingdom.myKingdom.WoodAmount += warehouse.buildTimberCost;
                            Kingdom.myKingdom.IronAmount += warehouse.buildIronCost;
                            Kingdom.myKingdom.FoodAmount += warehouse.buildFoodCost;
                            buildButton.enabled = true;
                        }
                    }));    
                }
                else
                {
                    Debug.Log("Yeterli kaynak bulunmamaktad�r veya �iftlik,Demirci,Ta�Oca��,Keresteci binalar� en az ikinci seviye olmal�d�r.");
                }
            }

            else if(Warehouse.buildLevel == 2 && Sawmill.buildLevel >= 2 && Blacksmith.buildLevel >= 2 && Farm.buildLevel >= 2 && StonePit.buildLevel >= 2)
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();
                if (checkResources(warehouse) )
                //

                {
                    //ProgressBar Ekle,Zaman dolunca a�a��dakileri yap.
                    Kingdom.myKingdom.GoldAmount -= warehouse.buildGoldCost;
                    Kingdom.myKingdom.StoneAmount -= warehouse.buildStoneCost;
                    Kingdom.myKingdom.WoodAmount -= warehouse.buildTimberCost;
                    Kingdom.myKingdom.IronAmount -= warehouse.buildIronCost;
                    Kingdom.myKingdom.FoodAmount -= warehouse.buildFoodCost;

                    buildButton.enabled = false;

                    StartCoroutine(progressBarController.WarehouseIsFinished(warehouse, (isFinished) =>
                    {
                        if (isFinished)
                        {
                            // Gerekli i�lemleri yap
                            Warehouse.buildLevel++;
                            Warehouse.IncreaseCapacity();
                            wareHousePanelController.refreshWarehouse();
                            Destroy(buildButton.gameObject);
                        }
                        else
                        {
                            // Kaynaklar� iade et
                            Kingdom.myKingdom.GoldAmount += warehouse.buildGoldCost;
                            Kingdom.myKingdom.StoneAmount += warehouse.buildStoneCost;
                            Kingdom.myKingdom.WoodAmount += warehouse.buildTimberCost;
                            Kingdom.myKingdom.IronAmount += warehouse.buildIronCost;
                            Kingdom.myKingdom.FoodAmount += warehouse.buildFoodCost;
                            buildButton.enabled = true;
                        }
                    }));                  
                }
            }
            else
            {
                Debug.Log("Bir sorun var gibi duruyor 'BuildBuilder' scriptindeki buildWareHouse fonksiyonunu kontrol ediniz.");
            }
        }
    }




    public void UpgradeCastle()
    {
        // Zaten var olan demirci nesnesini kullanmak i�in kontrol edin
        Castle castle = GetComponent<Castle>();

        if (!Castle.wasCastleCreated)
        {
            castle = gameObject.AddComponent<Castle>();

            if (checkResources(castle))
            {
                // Kaynaklar� azalt�n
                Kingdom.myKingdom.GoldAmount -= castle.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= castle.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= castle.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= castle.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= castle.buildFoodCost;

                buildButton.enabled = false;

                StartCoroutine(progressBarController.CastleIsFinished(castle, (isFinished) =>
                {
                    if (isFinished)
                    {
                        Castle.wasCastleCreated = true;

                        Castle.buildLevel = 2;
                       //�zelliklerini artt�r
                        castle.UpdateCosts(); // Maliyetleri g�ncelle
                        buildButton.enabled = true;
                        castlePanelController.refreshCastle();
                    }
                    else
                    {
                        // Kaynaklar� iade et
                        Kingdom.myKingdom.GoldAmount += castle.buildGoldCost;
                        Kingdom.myKingdom.StoneAmount += castle.buildStoneCost;
                        Kingdom.myKingdom.WoodAmount += castle.buildTimberCost;
                        Kingdom.myKingdom.IronAmount += castle.buildIronCost;
                        Kingdom.myKingdom.FoodAmount += castle.buildFoodCost;
                        buildButton.enabled = true;
                    }
                }));
            }
            else
            {
                Debug.Log("Yeterli kaynak bulunmamaktad�r");
            }
        }
        else
        {
            

            if (Castle.buildLevel == 2)
            {             
                if (checkResources(castle))
                {
                    Kingdom.myKingdom.GoldAmount -= castle.buildGoldCost;
                    Kingdom.myKingdom.StoneAmount -= castle.buildStoneCost;
                    Kingdom.myKingdom.WoodAmount -= castle.buildTimberCost;
                    Kingdom.myKingdom.IronAmount -= castle.buildIronCost;
                    Kingdom.myKingdom.FoodAmount -= castle.buildFoodCost;

                    buildButton.enabled = false;

                    StartCoroutine(progressBarController.CastleIsFinished(castle, (isFinished) =>
                    {
                        if (isFinished)
                        {
                            // Gerekli i�lemleri yap

                            Castle.buildLevel++;
                           //Gerekeni Yap
                            castlePanelController.refreshCastle();
                            Destroy(buildButton.gameObject);
                        }
                        else
                        {
                            // Kaynaklar� iade et
                            Kingdom.myKingdom.GoldAmount += castle.buildGoldCost;
                            Kingdom.myKingdom.StoneAmount += castle.buildStoneCost;
                            Kingdom.myKingdom.WoodAmount += castle.buildTimberCost;
                            Kingdom.myKingdom.IronAmount += castle.buildIronCost;
                            Kingdom.myKingdom.FoodAmount += castle.buildFoodCost;
                            buildButton.enabled = true;
                        }
                    }));
                }
            }
            else
            {
                Debug.Log("Bir sorun var gibi duruyor 'BuildBuilder' scriptindeki BuildBlacksmith fonksiyonunu kontrol ediniz.");
            }
        }
    }

}

