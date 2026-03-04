using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CreatingCards : MonoBehaviour
{


    public GameObject cardPrefab;
    public GameObject cardHolder; 
    public ScrollRect scrollRect;

    /*
        Sets the scroll position of the card holder to the top.
    */
    public void SetScroleToTop() {
        LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content);
        scrollRect.horizontalNormalizedPosition = 0f;
    }

    /*
        Creates a card UI element based on the settings of the game at the specified index.
        It populates the card with title, description, image, and slider values.
    */

    public void CreateCard(int index) {
        GameObject newCard = Instantiate(cardPrefab, cardHolder.transform);
        Transform CardTitle = newCard.transform.Find("Title");
        Transform CardDescription = newCard.transform.Find("Description");
        Transform CardImage = newCard.transform.Find("Image");
        Transform Selection = newCard.transform.Find("Selection");
        Transform Slider = newCard.transform.Find("Slider");

        Transform Max = newCard.transform.Find("Max");
        Transform Min = newCard.transform.Find("Min");
        Transform Value = newCard.transform.Find("Value");
        Transform Units = newCard.transform.Find("Units");

        CardTitle.GetComponent<TextMeshProUGUI>().text = GameList.staticGameList[GameList.gameIndex].Settings[index].cardName;
        CardDescription.GetComponent<TextMeshProUGUI>().text = GameList.staticGameList[GameList.gameIndex].Settings[index].cardDescription;
        CardImage.GetComponent<Image>().sprite = GameList.staticGameList[GameList.gameIndex].Settings[index].cardImage;

        Slider.GetComponent<Slider>().minValue = GameList.staticGameList[GameList.gameIndex].Settings[index].SettingValue.min;
        Slider.GetComponent<Slider>().maxValue = GameList.staticGameList[GameList.gameIndex].Settings[index].SettingValue.max;
        Slider.GetComponent<Slider>().value = GameList.staticGameList[GameList.gameIndex].Settings[index].SettingValue.value;

        Slider.GetComponent<SliderValueChange>().settingIndex = index;

        Max.GetComponent<TextMeshProUGUI>().text = GameList.staticGameList[GameList.gameIndex].Settings[index].SettingValue.max.ToString();
        Min.GetComponent<TextMeshProUGUI>().text = GameList.staticGameList[GameList.gameIndex].Settings[index].SettingValue.min.ToString();
        Value.GetComponent<TextMeshProUGUI>().text = GameList.staticGameList[GameList.gameIndex].Settings[index].SettingValue.value.ToString("F2");
        Units.GetComponent<TextMeshProUGUI>().text = GameList.staticGameList[GameList.gameIndex].Settings[index].SettingValue.units;

        if (GameList.staticGameList[GameList.gameIndex].Settings[index].sliderisWholeNumber)
        {
            Slider.GetComponent<Slider>().wholeNumbers = true;
        }

    }





    /*
        Creates a special card for platform settings based on the game's special card platform configuration.
        It populates the card with title, description, image, and platform values (front, back, left, right).
    */

    public void CreateSpecialCardPlatform(int index)
    {
        GameObject newCard = Instantiate((GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.cardPrefab, cardHolder.transform);
        Transform CardTitle = newCard.transform.Find("Title");
        Transform CardDescription = newCard.transform.Find("Description");
        Transform CardImage = newCard.transform.Find("Image");

        Transform frontValue = newCard.transform.Find("Platform").GetChild(0).GetChild(0);
        Transform backValue = newCard.transform.Find("Platform").GetChild(1).GetChild(0);
        Transform leftValue = newCard.transform.Find("Platform").GetChild(2).GetChild(0);
        Transform rightValue = newCard.transform.Find("Platform").GetChild(3).GetChild(0);

        CardTitle.GetComponent<TextMeshProUGUI>().text = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.cardName;
        CardDescription.GetComponent<TextMeshProUGUI>().text = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.cardDescription;
        CardImage.GetComponent<Image>().sprite = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.cardImage;

        frontValue.GetComponent<TextMeshProUGUI>().text = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.SettingValue.frontValue.ToString("F1");
        backValue.GetComponent<TextMeshProUGUI>().text = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.SettingValue.backValue.ToString("F1");
        leftValue.GetComponent<TextMeshProUGUI>().text = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.SettingValue.leftValue.ToString("F1");
        rightValue.GetComponent<TextMeshProUGUI>().text = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardPlatform.SettingValue.rightValue.ToString("F1");
    }

    /*
        Creates a special card for bias settings based on the game's special card bias configuration.
        It populates the card with title, description, image, and bias values (left, right, units).
    */

    public void CreateSpecialCardBias(int index)
    {
        GameObject newCard = Instantiate((GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardBias.cardPrefab, cardHolder.transform);
        Transform CardTitle = newCard.transform.Find("Title");
        Transform CardDescription = newCard.transform.Find("Description");
        Transform CardImage = newCard.transform.Find("Image");
        Transform Slider = newCard.transform.Find("Slider");

        Transform LeftBias = newCard.transform.Find("LeftBias");
        Transform RightBias = newCard.transform.Find("RightBias");
        Transform Units = newCard.transform.Find("Units");

        CardTitle.GetComponent<TextMeshProUGUI>().text = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardBias.cardName;
        CardDescription.GetComponent<TextMeshProUGUI>().text = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardBias.cardDescription;
        CardImage.GetComponent<Image>().sprite = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardBias.cardImage;

        Slider.GetComponent<Slider>().minValue = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardBias.SettingValue.min;
        Slider.GetComponent<Slider>().maxValue = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardBias.SettingValue.max;
        Slider.GetComponent<Slider>().value = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardBias.SettingValue.value;

        Slider.GetComponent<TargetBiasSlider>().settingIndex = index;

        LeftBias.GetComponent<TextMeshProUGUI>().text = ((GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardBias.SettingValue.max - (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardBias.SettingValue.value).ToString();
        RightBias.GetComponent<TextMeshProUGUI>().text = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardBias.SettingValue.value.ToString();
        Units.GetComponent<TextMeshProUGUI>().text = (GameList.staticGameList[GameList.gameIndex] as TargetTap).SpecialCardBias.SettingValue.units;
    }

    /// <summary>
    /// Creates a special card that has a active toggle and a slider to be used for settings that can be both adjust the settings and turn it on and off.
    /// It populates the card with a title, description, image, min/max values, units and sets isActive.
    /// Allows for custom left and right text descriptions if the "customeSliderDescription" is true.
    /// </summary>
    public void CreateSpecialCardBoolFloat(int index)
    {
        GameObject newCard = Instantiate((GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.cardPrefab, cardHolder.transform);
        Transform cardTitle = newCard.transform.Find("Title");
        Transform CardDescription = newCard.transform.Find("Description");
        Transform CardImage = newCard.transform.Find("Image");
        Transform Slider = newCard.transform.Find("Slider");

        Transform LeftBias = newCard.transform.Find("LeftBias");
        Transform RightBias = newCard.transform.Find("RightBias");
        Transform Units = newCard.transform.Find("Units");
        Transform activeToggle = newCard.transform.Find("ActiveToggle");
        Transform leftText = newCard.transform.Find("LeftText");
        Transform rightText = newCard.transform.Find("RightText");

        cardTitle.GetComponent<TextMeshProUGUI>().text = (GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.cardName;
        CardDescription.GetComponent<TextMeshProUGUI>().text = (GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.cardDescrption;
        CardImage.GetComponent<Image>().sprite = (GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.cardImage;

        Slider.GetComponent<Slider>().minValue = (GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.SettingValue.min;
        Slider.GetComponent<Slider>().maxValue = (GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.SettingValue.max;
        Slider.GetComponent<Slider>().value = (GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.SettingValue.value;

        //might not need this?
        //Slider.GetComponent<TargetBiasSlider>().settingIndex = index;

        LeftBias.GetComponent<TextMeshProUGUI>().text = ((GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.SettingValue.max - (GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.SettingValue.value).ToString();
        RightBias.GetComponent<TextMeshProUGUI>().text = ((GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.SettingValue.value).ToString();
        Units.GetComponent<TextMeshProUGUI>().text = (GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.SettingValue.units;

        activeToggle.GetComponent<Toggle>().isOn = (GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.isActive;

        //If custom slider descriptions
        if ((GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.customSliderDescription)
        {
            leftText.GetComponent<TextMeshProUGUI>().text = ((GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.leftSliderDescription);
            rightText.GetComponent<TextMeshProUGUI>().text = ((GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardEmergencyVehicle.rightSliderDescription);
        }
        else
        {
            leftText.GetComponent<TextMeshProUGUI>().text = null;
            rightText.GetComponent<TextMeshProUGUI>().text = null;
        }
    }
    /// <summary>
    /// Creates a special card that has a active toggle based on the special card bool configuration.
    /// </summary>

    public void CreateSpecialCardBool(int index)
    {
        GameObject newCard = Instantiate((GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardDetour.cardPrefab, cardHolder.transform);
        Transform cardTitle = newCard.transform.Find("Title");
        Transform CardDescription = newCard.transform.Find("Description");
        Transform CardImage = newCard.transform.Find("Image");
        Transform activeToggle = newCard.transform.Find("ActiveToggle");

        cardTitle.GetComponent<TextMeshProUGUI>().text = (GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardDetour.cardName;
        CardDescription.GetComponent<TextMeshProUGUI>().text = (GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardDetour.cardDescription;
        CardImage.GetComponent<Image>().sprite = (GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardDetour.cardImage;
        
        activeToggle.GetComponent<Toggle>().isOn = (GameList.staticGameList[GameList.gameIndex] as TrafficJam).SpecialCardDetour.isActive;
    }
    /*
        Unity method called once before the first frame update.  
        Initializes the card creation process based on the game's settings and card order.
    */
    void Start()
    {
        
        
        int cardCount = GameList.staticGameList[GameList.gameIndex].Settings.Length;

        bool cardOrder = GameList.staticGameList[GameList.gameIndex].CardOrder;


        //A switch statment that looks at the current gameIndex gameName to determin how the settings cards are handled
        switch (GameList.staticGameList[GameList.gameIndex].gameName)
        {
            case "TargetTap":
                {
                    if (cardOrder == true)
                    {
                        CreateCard(0);
                        CreateCard(1);
                        CreateCard(2);


                        TargetTap t = GameList.staticGameList[GameList.gameIndex] as TargetTap;
                        if (t == null)
                        {
                            Debug.Log("no special Cards");
                        }
                        else
                        {
                            CreateSpecialCardPlatform(0);
                            CreateSpecialCardBias(1);
                        }
                        CreateCard(3);
                        CreateCard(4);
                        CreateCard(5);


                    }
                    else
                    {
                        if (cardCount == 3)
                            for (int i = 0; i < cardCount; i++)
                            {
                                CreateCard(i);
                            }
                        TargetTap t = GameList.staticGameList[GameList.gameIndex] as TargetTap;
                        if (t == null)
                        {
                            Debug.Log("no special Cards");
                        }
                        else
                        {
                            CreateSpecialCardPlatform(0);
                            CreateSpecialCardBias(1);
                        }
                    }
                    break;
                }
            case "TrafficJam":
                {
                    for (int index = 0; index < cardCount; index++)
                    {
                        CreateCard(index);
                    }
                    CreateSpecialCardBoolFloat(0);
                    CreateSpecialCardBool(1);

                    break;
                }
            default:
                {
                    for (int index = 0; index < cardCount; index++)
                    {
                        CreateCard(index);
                    }
                    break;
                }
        }

            SetScroleToTop();
    }
}
