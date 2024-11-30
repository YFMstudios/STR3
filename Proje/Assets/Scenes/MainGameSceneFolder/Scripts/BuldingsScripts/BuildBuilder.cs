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

    public static bool checkResources(Building building) // Artýk Building türü kabul ediliyor
    {
        // Güncel maliyetleri kontrol edin
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
        // Zaten var olan taþ ocaðý nesnesini kullanmak için kontrol edin
        StonePit stonePit = GetComponent<StonePit>();

        if (!StonePit.wasStonePitCreated)
        {
            stonePit = gameObject.AddComponent<StonePit>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(stonePit))
            {
                // Kaynaklarý azaltýn
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
                        stonePit.UpdateCosts(); // Maliyetleri güncelle

                        Debug.Log("Bina Seviyesi : " + StonePit.buildLevel);
                        buttonText.text = "Yükselt";
                        buildButton.enabled = true;
                        stonepitPanelController.refreshStonePit();
                    }
                    else
                    {
                        // Kaynaklarý iade et
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
                Debug.Log("Yeterli kaynak bulunmamaktadýr");
            }
        }
        else
        {

            // Zaten bir taþ ocaðý varsa, yeni bir nesne yaratmayýn
            if (StonePit.buildLevel == 1)
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

                if (checkResources(stonePit))
                {
                    // Kaynaklarý azaltýn
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
                            // Gerekli iþlemleri yap

                            StonePit.buildLevel++;
                            StonePit.refreshStoneProductionRate(); // Üretim miktarýný güncelliyoruz.
                            stonePit.UpdateCosts(); // Maliyetleri güncelle
                            buttonText.text = "Yükselt";
                            buildButton.enabled = true;
                            stonepitPanelController.refreshStonePit();
                        }
                        else
                        {
                            // Kaynaklarý iade et
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
                    Debug.Log("Yeterli kaynak bulunmamaktadýr");
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
                            // Gerekli iþlemleri yap

                            StonePit.buildLevel++;
                            StonePit.refreshStoneProductionRate(); // Üretim miktarýný güncelliyoruz.                  
                            stonepitPanelController.refreshStonePit();
                            Destroy(buildButton.gameObject);
                        }
                        else
                        {
                            // Kaynaklarý iade et
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
        // Zaten var olan demirci nesnesini kullanmak için kontrol edin
        Blacksmith blacksmith = GetComponent<Blacksmith>();

        if (!Blacksmith.wasBlacksmithCreated)
        {
            blacksmith = gameObject.AddComponent<Blacksmith>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(blacksmith))
            {
                // Kaynaklarý azaltýn
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
                        blacksmith.UpdateCosts(); // Maliyetleri güncelle

                        Debug.Log("Bina Seviyesi : " + Blacksmith.buildLevel);
                        buttonText.text = "Yükselt";
                        buildButton.enabled = true;
                        blacksmithPanelController.refreshBlacksmith();
                    }
                    else
                    {
                        // Kaynaklarý iade et
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
                Debug.Log("Yeterli kaynak bulunmamaktadýr");
            }
        }
        else
        {
            // Zaten bir demirci varsa, yeni bir nesne yaratmayýn
            if (Blacksmith.buildLevel == 1)
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

                if (checkResources(blacksmith))
                {
                    // Kaynaklarý azaltýn
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
                            // Gerekli iþlemleri yap

                            Blacksmith.buildLevel++;
                            Blacksmith.refreshIronProductionRate(); // Üretim miktarýný güncelliyoruz.
                            blacksmith.UpdateCosts(); // Maliyetleri güncelle
                            buttonText.text = "Yükselt";
                            buildButton.enabled = true;
                            blacksmithPanelController.refreshBlacksmith();
                        }
                        else
                        {
                            // Kaynaklarý iade et
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
                    Debug.Log("Yeterli kaynak bulunmamaktadýr");
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
                            // Gerekli iþlemleri yap

                            Blacksmith.buildLevel++;
                            Blacksmith.refreshIronProductionRate(); // Üretim miktarýný güncelliyoruz.                   
                            blacksmithPanelController.refreshBlacksmith();
                            Destroy(buildButton.gameObject);
                        }
                        else
                        {
                            // Kaynaklarý iade et
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
        // Zaten var olan kereste ocaðý nesnesini kullanmak için kontrol edin
        Sawmill sawmill = GetComponent<Sawmill>();

        if (!Sawmill.wasSawmillCreated)
        {
            sawmill = gameObject.AddComponent<Sawmill>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(sawmill))
            {
                // Kaynaklarý azaltýn
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
                        sawmill.UpdateCosts(); // Maliyetleri güncelle

                        Debug.Log("Bina Seviyesi : " + Sawmill.buildLevel);
                        buttonText.text = "Yükselt";
                        buildButton.enabled = true;
                        sawmillPanelController.refreshSawmill();
                    }
                    else
                    {
                        // Kaynaklarý iade et
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
                Debug.Log("Yeterli kaynak bulunmamaktadýr");
            }
        }
        else
        {
            // Zaten bir kereste ocaðý varsa, yeni bir nesne yaratmayýn
            if (Sawmill.buildLevel == 1)
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

                if (checkResources(sawmill))
                {
                    // Kaynaklarý azaltýn
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
                            // Gerekli iþlemleri yap

                            Sawmill.buildLevel++;
                            Sawmill.refreshTimberProductionRate(); // Üretim miktarýný güncelliyoruz.
                            sawmill.UpdateCosts(); // Maliyetleri güncelle
                            buttonText.text = "Yükselt";
                            buildButton.enabled = true;
                            sawmillPanelController.refreshSawmill();
                        }
                        else
                        {
                            // Kaynaklarý iade et
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
                    Debug.Log("Yeterli kaynak bulunmamaktadýr");
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
                            // Gerekli iþlemleri yap

                            Sawmill.buildLevel++;
                            Sawmill.refreshTimberProductionRate(); // Üretim miktarýný güncelliyoruz.                  
                            sawmillPanelController.refreshSawmill();
                            Destroy(buildButton.gameObject);
                        }
                        else
                        {
                            // Kaynaklarý iade et
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
        // Zaten var olan çiftlik nesnesini kullanmak için kontrol edin
        Farm farm = GetComponent<Farm>();

        if (!Farm.wasFarmCreated)
        {
            farm = gameObject.AddComponent<Farm>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(farm))
            {
                // Kaynaklarý azaltýn
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
                        farm.UpdateCosts(); // Maliyetleri güncelle

                        Debug.Log("Bina Seviyesi : " + Farm.buildLevel);
                        buttonText.text = "Yükselt";
                        buildButton.enabled = true;
                        farmPanelController.refreshFarm();
                    }
                    else
                    {
                        // Kaynaklarý iade et
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
                Debug.Log("Yeterli kaynak bulunmamaktadýr");
            }
        }
        else
        {
            // Zaten bir çiftlik varsa, yeni bir nesne yaratmayýn
            if (Farm.buildLevel == 1)
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

                if (checkResources(farm))
                {
                    // Kaynaklarý azaltýn
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
                            // Gerekli iþlemleri yap

                            Farm.buildLevel++;
                            Farm.refreshFoodProductionRate(); // Üretim miktarýný güncelliyoruz.
                            farm.UpdateCosts(); // Maliyetleri güncelle
                            buttonText.text = "Yükselt";
                            buildButton.enabled = true;
                            farmPanelController.refreshFarm();
                        }
                        else
                        {
                            // Kaynaklarý iade et
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
                    Debug.Log("Yeterli kaynak bulunmamaktadýr");
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
                            // Gerekli iþlemleri yap

                            Farm.buildLevel++;
                            Farm.refreshFoodProductionRate(); // Üretim miktarýný güncelliyoruz.                  
                            farmPanelController.refreshFarm();
                            Destroy(buildButton.gameObject);
                        }
                        else
                        {
                            // Kaynaklarý iade et
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
        // Zaten var olan kýþla nesnesini kullanmak için kontrol edin
        Barracks barracks = GetComponent<Barracks>();

        if (!Barracks.wasBarracksCreated)
        {
            barracks = gameObject.AddComponent<Barracks>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(barracks) && Sawmill.buildLevel >= 1 && Farm.buildLevel >= 2 && Blacksmith.buildLevel >= 1)
            {                
                //Kaynaklarý Azalt
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
                        // Gerekli iþlemleri yap


                        Barracks.wasBarracksCreated = true;
                        Barracks.buildLevel = 1;
                        barracks.UpdateCosts(); // Maliyetleri güncelle
                        Debug.Log("Bina Seviyesi : " + Barracks.buildLevel);
                        buttonText.text = "Yükselt";
                        buildButton.enabled = true;
                        barracksPanelController.refreshBarracks();
                    }
                    else
                    {
                        // Kaynaklarý iade et
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
                Debug.Log("Binayý oluþturmak için gerekli gereksinimleri saðlamýyorsunuz.");
            }
        }
        else
        {
            if (Barracks.buildLevel == 1)
            {
                if (checkResources(barracks) && Sawmill.buildLevel >= 2 && Farm.buildLevel >= 3 && Blacksmith.buildLevel >= 2)
                {
                    //Eðer asker üretimi varsa buraya girme -----> Asker üretimi yaparken geliþtirilemez.
                    if (progressBarController.isUnitCreationActive)
                    {
                        Debug.Log("Asker Üretimi Sýrasýnda Bina Yükseltmesi Yapýlamaz.");
                    }
                    //yoksa gir.
                    else
                    {
                        //Kaynaklarý Azalt
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
                                // Gerekli iþlemleri yap


                                Barracks.wasBarracksCreated = true;
                                Barracks.buildLevel++;
                                barracks.UpdateCosts(); // Maliyetleri güncelle
                                Debug.Log("Bina Seviyesi : " + Barracks.buildLevel);
                                buildButton.enabled = true;
                                barracksPanelController.refreshBarracks();
                            }
                            else
                            {
                                // Kaynaklarý iade et
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
                    Debug.Log("Binayý oluþturmak için gerekli gereksinimleri saðlamýyorsunuz.");
                }
            }

            else if (Barracks.buildLevel == 2)
            {
                if (checkResources(barracks) && Sawmill.buildLevel >= 3 && Farm.buildLevel >= 3 && Blacksmith.buildLevel >= 3)
                {
                    //Asker üretimi varsa buraya girme.             
                    if (progressBarController.isUnitCreationActive)
                    {
                        Debug.Log("Asker Üretimi Sýrasýnda Bina Yükseltmesi Yapýlamaz.");
                    }
                    //yoksa gir.
                    else
                    {
                        //Kaynaklarý Azalt
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
                                // Gerekli iþlemleri yap


                                Barracks.buildLevel++;
                                barracks.UpdateCosts(); // Maliyetleri güncelle
                                Debug.Log("Bina Seviyesi : " + Barracks.buildLevel);
                                Destroy(buildButton.gameObject);
                                barracksPanelController.refreshBarracks();
                            }
                            else
                            {
                                // Kaynaklarý iade et
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
                    Debug.Log("Binayý oluþturmak için gerekli gereksinimleri saðlamýyorsunuz.");
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
        // Zaten var olan hastane nesnesini kullanmak için kontrol edin
        Hospital hospital = GetComponent<Hospital>();

        if (!Hospital.wasHospitalCreated)
        {
            hospital = gameObject.AddComponent<Hospital>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(hospital))
            {
                buildButton.enabled = false;
                // Kaynaklarý azaltýn
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
                        // Gerekli iþlemleri yap

                        Hospital.wasHospitalCreated = true;
                        Hospital.buildLevel = 1;
                        hospital.UpdateCapasity();
                        Debug.Log("Bina Seviyesi : " + Hospital.buildLevel);
                        Debug.Log("Hastane Kapasitesi : " + Hospital.capasity);
                        hospital.UpdateCosts(); // Maliyetleri güncelle              
                        buttonText.text = "Yükselt";
                        hospitalPanelController.refreshHospital();
                        buildButton.enabled = true;
                    }
                    else
                    {
                        // Kaynaklarý iade et
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
                Debug.Log("Yeterli kaynak bulunmamaktadýr");
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
                        Debug.Log("Ýyileþtirme esnasýnda bina yükseltmesi yapýlamaz.");
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
                                // Gerekli iþlemleri yap
                                Hospital.buildLevel++;
                                hospital.UpdateCapasity();
                                Debug.Log("Bina Seviyesi : " + Hospital.buildLevel);
                                Debug.Log("Hastane Kapasitesi : " + Hospital.capasity);
                                hospital.UpdateCosts(); // Maliyetleri güncelle
                                buttonText.text = "Yükselt";
                                hospitalPanelController.refreshHospital();
                                buildButton.enabled = true;
                            }
                            else
                            {
                                // Kaynaklarý iade et
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
                    Debug.Log("Yeterli kaynak bulunmamaktadýr");
                }

            }

            else if(Hospital.buildLevel == 2)
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();
                // Kaynaklarý azaltýn


                if (checkResources(hospital))
                {
                   
                    if (progressBarController.isHealActive)
                    {
                        Debug.Log("Ýyileþtirme esnasýnda bina yükseltmesi yapýlamaz.");
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
                                // Gerekli iþlemleri yap
                                Hospital.buildLevel++;
                                hospital.UpdateCapasity();
                                Destroy(buildButton.gameObject);
                                hospitalPanelController.refreshHospital();
                            }
                            else
                            {
                                // Kaynaklarý iade et
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
                    Debug.Log("Yeterli kaynak bulunmamaktadýr");
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

        if (Lab.wasLabCreated == false)//Daha önce üretilmediyse
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
                        // Gerekli iþlemleri yap
                        Lab.wasLabCreated = true;
                        Lab.buildLevel = 1;
                        //Araþtýrma Hýzýný Arttýr
                        researchController.OpenResearchUnit(Lab.buildLevel);
                        lab.UpdateCosts();
                        buttonText.text = "Yükselt";
                        buildButton.enabled = true;
                        labPanelController.refreshLab();
                    }
                    else
                    {
                        // Kaynaklarý iade et
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
                Debug.Log("Yeterli Kaynak Bulunmamaktadýr veya Keresteci 2.Seviye Deðil.");
            }
        }
        else//Daha önce üretildi ise
        {
            if(Lab.buildLevel == 1 & ResearchButtonEvents.isResearched[3] && ResearchButtonEvents.isResearched[4])//Lab 1.seviyeyse
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();
                if (checkResources(lab) )
                //&
                //Kaynaklar yeterliyse 3.ve 4. araþtýrma yapýldýysa.
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
                            // Gerekli iþlemleri yap                           
                            Lab.buildLevel++;
                            //Araþtýrma Hýzýný Arttýr
                            researchController.controlBuildLevelTwoResearches();
                            lab.UpdateCosts();
                            buttonText.text = "Yükselt";
                            buildButton.enabled = true;
                            labPanelController.refreshLab();
                        }
                        else
                        {
                            // Kaynaklarý iade et
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
                    Debug.Log("Lütfen kaynaklarýn yeterli olduðundan veya Dört ve Beþ numaralý araþtýrmalý tamamladýðýnýzdan emin olun!");
                }
            }

            else if(Lab.buildLevel ==2 )
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();
                if (checkResources(lab) && ResearchButtonEvents.isResearched[10] && ResearchButtonEvents.isResearched[11] && ResearchButtonEvents.isResearched[12] && Sawmill.buildLevel >= 3)
                //
                //Kaynak yeterliyse 11,12,13. araþtýrmalar yapýldýysa ve keresteci seviye 3'se
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
                            // Gerekli iþlemleri yap                           
                            Lab.buildLevel++;
                            //Araþtýrma Hýzýný Arttýr
                            researchController.controlBuildLevelThreeResearches();
                            lab.UpdateCosts();
                            buttonText.text = "Yükselt";
                            buildButton.enabled = true;
                            labPanelController.refreshLab();
                            Destroy(buildButton.gameObject);
                        }
                        else
                        {
                            // Kaynaklarý iade et
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
                    Debug.Log("Lütfen kaynaklarýn yeterli olduðundan, 11,12,13 numaralý araþtýrmalý tamamladýðýnýzdan ve Kerestecinizin 3.seviye olduðundan emin olun!");
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
        // Zaten var olan savunma atölyesi nesnesini kullanmak için kontrol edin
        DefenseWorkshop defenseWorkshop = GetComponent<DefenseWorkshop>();

        if (!DefenseWorkshop.wasDefenseWorkshopCreated)
        {
            defenseWorkshop = gameObject.AddComponent<DefenseWorkshop>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(defenseWorkshop))
            {
                // Kaynaklarý azaltýn
                Kingdom.myKingdom.GoldAmount -= defenseWorkshop.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= defenseWorkshop.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= defenseWorkshop.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= defenseWorkshop.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= defenseWorkshop.buildFoodCost;

                DefenseWorkshop.wasDefenseWorkshopCreated = true;
                DefenseWorkshop.buildLevel = 1;
                defenseWorkshop.UpdateCosts(); // Maliyetleri güncelle

                Debug.Log("Bina Seviyesi : " + DefenseWorkshop.buildLevel);
                buttonText.text = "Yükselt";
            }
            else
            {
                Debug.Log("Yeterli kaynak bulunmamaktadýr");
            }
        }
        else
        {
            // Zaten bir savunma atölyesi varsa, yeni bir nesne yaratmayýn
            if (checkResources(defenseWorkshop))
            {
                // Kaynaklarý azaltýn
                Kingdom.myKingdom.GoldAmount -= defenseWorkshop.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= defenseWorkshop.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= defenseWorkshop.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= defenseWorkshop.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= defenseWorkshop.buildFoodCost;

                DefenseWorkshop.buildLevel++;
                Debug.Log("Bina Seviyesi : " + DefenseWorkshop.buildLevel);
                defenseWorkshop.UpdateCosts(); // Maliyetleri güncelle

                // 3. seviyeye ulaþýldýðýnda butonu yok et
                if (DefenseWorkshop.buildLevel == 3)
                {
                    Destroy(buildButton.gameObject);
                }
            }
            else
            {
                Debug.Log("Yeterli kaynak bulunmamaktadýr");
            }
        }

    }

    public void BuildSiegeWorkshop()
    {
        // Zaten var olan kuþatma atölyesi nesnesini kullanmak için kontrol edin
        SiegeWorkshop siegeWorkshop = GetComponent<SiegeWorkshop>();

        if (!SiegeWorkshop.wasSiegeWorkshopCreated)
        {
            siegeWorkshop = gameObject.AddComponent<SiegeWorkshop>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(siegeWorkshop))
            {
                // Kaynaklarý azaltýn
                Kingdom.myKingdom.GoldAmount -= siegeWorkshop.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= siegeWorkshop.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= siegeWorkshop.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= siegeWorkshop.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= siegeWorkshop.buildFoodCost;

                SiegeWorkshop.wasSiegeWorkshopCreated = true;
                SiegeWorkshop.buildLevel = 1;
                siegeWorkshop.UpdateCosts(); // Maliyetleri güncelle

                Debug.Log("Bina Seviyesi : " + SiegeWorkshop.buildLevel);
                buttonText.text = "Yükselt";
            }
            else
            {
                Debug.Log("Yeterli kaynak bulunmamaktadýr");
            }
        }
        else
        {
            // Zaten bir kuþatma atölyesi varsa, yeni bir nesne yaratmayýn
            if (checkResources(siegeWorkshop))
            {
                // Kaynaklarý azaltýn
                Kingdom.myKingdom.GoldAmount -= siegeWorkshop.buildGoldCost;
                Kingdom.myKingdom.StoneAmount -= siegeWorkshop.buildStoneCost;
                Kingdom.myKingdom.WoodAmount -= siegeWorkshop.buildTimberCost;
                Kingdom.myKingdom.IronAmount -= siegeWorkshop.buildIronCost;
                Kingdom.myKingdom.FoodAmount -= siegeWorkshop.buildFoodCost;

                SiegeWorkshop.buildLevel++;
                Debug.Log("Bina Seviyesi : " + SiegeWorkshop.buildLevel);
                siegeWorkshop.UpdateCosts(); // Maliyetleri güncelle

                // 3. seviyeye ulaþýldýðýnda butonu yok et
                if (SiegeWorkshop.buildLevel == 3)
                {
                    Destroy(buildButton.gameObject);
                }
            }
            else
            {
                Debug.Log("Yeterli kaynak bulunmamaktadýr");
            }
        }

    }

    public void BuildWarehouse()
    {// Zaten var olan kýþla nesnesini kullanmak için kontrol edin
        Warehouse warehouse = GetComponent<Warehouse>();

        if (!Warehouse.wasWarehouseCreated)
        {
            warehouse = gameObject.AddComponent<Warehouse>();
            TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();

            if (checkResources(warehouse) && Farm.buildLevel >= 1 && Sawmill.buildLevel >= 1 && StonePit.buildLevel >= 1 && Blacksmith.buildLevel >= 1)
            //
            {

                //Kaynaklarý Azalt
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
                        // Gerekli iþlemleri yap
                        Warehouse.wasWarehouseCreated = true;
                        Warehouse.buildLevel = 1;
                        Warehouse.IncreaseCapacity();
                        warehouse.UpdateCosts();
                        buttonText.text = "Yükselt";
                        buildButton.enabled = true;
                        wareHousePanelController.refreshWarehouse();
                    }
                    else
                    {
                        // Kaynaklarý iade et
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
                Debug.Log("Yeterli kaynak bulunmamaktadýr veya Çiftlik,Demirci,TaþOcaðý,Keresteci binalarý en az birinci seviye olmalýdýr.");
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
                            // Gerekli iþlemleri yap

                            Warehouse.buildLevel++;
                            Warehouse.IncreaseCapacity();
                            warehouse.UpdateCosts();
                            buttonText.text = "Yükselt";
                            buildButton.enabled = true;
                            wareHousePanelController.refreshWarehouse();
                        }
                        else
                        {
                            // Kaynaklarý iade et
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
                    Debug.Log("Yeterli kaynak bulunmamaktadýr veya Çiftlik,Demirci,TaþOcaðý,Keresteci binalarý en az ikinci seviye olmalýdýr.");
                }
            }

            else if(Warehouse.buildLevel == 2 && Sawmill.buildLevel >= 2 && Blacksmith.buildLevel >= 2 && Farm.buildLevel >= 2 && StonePit.buildLevel >= 2)
            {
                TextMeshProUGUI buttonText = buildButton.GetComponentInChildren<TextMeshProUGUI>();
                if (checkResources(warehouse) )
                //

                {
                    //ProgressBar Ekle,Zaman dolunca aþaðýdakileri yap.
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
                            // Gerekli iþlemleri yap
                            Warehouse.buildLevel++;
                            Warehouse.IncreaseCapacity();
                            wareHousePanelController.refreshWarehouse();
                            Destroy(buildButton.gameObject);
                        }
                        else
                        {
                            // Kaynaklarý iade et
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




}

