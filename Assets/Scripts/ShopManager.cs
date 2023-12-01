using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public Canvas shopCanvas;
    public Canvas endScreenCanvas;


    public TextMeshProUGUI coinsText;

    public Button silverButton;
    public Button goldButton;
    public Button threeButton;
    public Button fiveButton;
    public Button sevenButton;
    public Button storeButton;

    public Sprite silverTray;
    public Sprite goldTray;

    public GameObject leftTray;
    public GameObject rightTray;

    // Start is called before the first frame update
    void Start()
    {
        RefreshCoinsText();

        silverButton.onClick.AddListener(SilverButtonClick);
        goldButton.onClick.AddListener(GoldButtonClick);

        threeButton.onClick.AddListener(ThreeButtonClick);
        fiveButton.onClick.AddListener(FiveButtonClick);
        sevenButton.onClick.AddListener(SevenButtonClick);

        storeButton.onClick.AddListener(FinishGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SilverButtonClick(){
        string silverButtonText = silverButton.GetComponentInChildren<TMP_Text>().text;
        string goldButtonText = goldButton.GetComponentInChildren<TMP_Text>().text;

        if(silverButtonText == "150" && GameManager.coins >= 150){
            silverButton.GetComponentInChildren<TMP_Text>().text = "Equip";

            GameManager.coins -= 150;

            RefreshCoinsText();
            
        }else if(silverButtonText == "Equip"){
            silverButton.GetComponentInChildren<TMP_Text>().text = "Equipped";

            leftTray.GetComponent<SpriteRenderer>().sprite = silverTray;
            rightTray.GetComponent<SpriteRenderer>().sprite = silverTray;

            GameManager.trayBonus = 15;

            if(goldButtonText == "Equipped"){
                goldButton.GetComponentInChildren<TMP_Text>().text = "Equip";
            }
        }
    }

    void GoldButtonClick(){
        string silverButtonText = silverButton.GetComponentInChildren<TMP_Text>().text;
        string goldButtonText = goldButton.GetComponentInChildren<TMP_Text>().text;

        if(goldButtonText == "500" && GameManager.coins >= 500){
            goldButton.GetComponentInChildren<TMP_Text>().text = "Equip";

            GameManager.coins -= 500;

            RefreshCoinsText();
        }else if(goldButtonText == "Equip"){
            goldButton.GetComponentInChildren<TMP_Text>().text = "Equipped";

            leftTray.GetComponent<SpriteRenderer>().sprite = goldTray;
            rightTray.GetComponent<SpriteRenderer>().sprite = goldTray;

            GameManager.trayBonus = 50;

            if(silverButtonText == "Equipped"){
                silverButton.GetComponentInChildren<TMP_Text>().text = "Equip";
            }
        }
    }

    void ThreeButtonClick(){
        string threeButtonText = threeButton.GetComponentInChildren<TMP_Text>().text;
        string fiveButtonText = fiveButton.GetComponentInChildren<TMP_Text>().text;
        string sevenButtonText = sevenButton.GetComponentInChildren<TMP_Text>().text;

        if(threeButtonText == "50" && GameManager.coins >= 50){
            threeButton.GetComponentInChildren<TMP_Text>().text = "Equip";

            GameManager.coins -= 50;

            RefreshCoinsText();
        }else if (threeButtonText == "Equip"){
            threeButton.GetComponentInChildren<TMP_Text>().text = "Equipped";

            GameManager.magicWeightWeight = 3;

            if(fiveButtonText == "Equipped"){
                fiveButton.GetComponentInChildren<TMP_Text>().text = "Equip";
            }

            if(sevenButtonText == "Equipped"){
                sevenButton.GetComponentInChildren<TMP_Text>().text = "Equip";
            }
        }
    }

    void FiveButtonClick(){
        string threeButtonText = threeButton.GetComponentInChildren<TMP_Text>().text;
        string fiveButtonText = fiveButton.GetComponentInChildren<TMP_Text>().text;
        string sevenButtonText = sevenButton.GetComponentInChildren<TMP_Text>().text;

        if(fiveButtonText == "100" && GameManager.coins >= 100){
            fiveButton.GetComponentInChildren<TMP_Text>().text = "Equip";

            GameManager.coins -= 100;

            RefreshCoinsText();
        }else if (fiveButtonText == "Equip"){
            fiveButton.GetComponentInChildren<TMP_Text>().text = "Equipped";

            GameManager.magicWeightWeight = 5;

            if(threeButtonText == "Equipped"){
                threeButton.GetComponentInChildren<TMP_Text>().text = "Equip";
            }

            if(sevenButtonText == "Equipped"){
                sevenButton.GetComponentInChildren<TMP_Text>().text = "Equip";
            }
        }
    }

    void SevenButtonClick(){
        string threeButtonText = threeButton.GetComponentInChildren<TMP_Text>().text;
        string fiveButtonText = fiveButton.GetComponentInChildren<TMP_Text>().text;
        string sevenButtonText = sevenButton.GetComponentInChildren<TMP_Text>().text;

        if(sevenButtonText == "150" && GameManager.coins >= 150){
            sevenButton.GetComponentInChildren<TMP_Text>().text = "Equip";

            GameManager.coins -= 150;

            RefreshCoinsText();
        }else if (sevenButtonText == "Equip"){
            sevenButton.GetComponentInChildren<TMP_Text>().text = "Equipped";

            GameManager.magicWeightWeight = 7;

            if(threeButtonText == "Equipped"){
                threeButton.GetComponentInChildren<TMP_Text>().text = "Equip";
            }

            if(fiveButtonText == "Equipped"){
                fiveButton.GetComponentInChildren<TMP_Text>().text = "Equip";
            }
        }
    }

    void RefreshCoinsText(){
        coinsText.text = GameManager.coins.ToString();
    }

    void FinishGame(){
        shopCanvas.gameObject.SetActive(false);
        endScreenCanvas.gameObject.SetActive(true);
    }

}
