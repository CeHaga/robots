using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class CustomCodeManager : MonoBehaviour
{
    [Header("Managers")]
    public PanelManager panelManager;

    [Header("Compiler")]
    [SerializeField] private Compiler compiler;

    [Header("Game Objects")]
    public TMP_Text compilePopupText;
    public TMP_InputField[] inputFields;

    public Dictionary<string, int> variables;

    public void ExportCode()
    {
        string compileResult = "";
        List<BlockController> blocks = panelManager.blocks;
        bool compiled = compiler.Compile(blocks, ref compileResult);
        if (!compiled)
        {
            compilePopupText.transform.parent.gameObject.SetActive(true);
            compilePopupText.SetText(compileResult);
            return;
        }

        int memoryCount = Directory.GetFiles("CustomMemories").Length;
        string fileName = (memoryCount + 1) + ".bin";

        int roundMedal = 0;
        int sizeMedal = 0;
        int hpPlayer = 0;
        int hpEnemy = 0;
        int dmgPlayer = 0;
        int dmgEnemy = 0;
        int defPlayer = 0;
        int defEnemy = 0;
        int chaPlayer = 0;
        int chaEnemy = 0;
        foreach (TMP_InputField inputField in inputFields)
        {
            switch (inputField.name)
            {
                case "RoundMedal":
                    roundMedal = int.Parse(inputField.text);
                    break;
                case "SizeMedal":
                    sizeMedal = int.Parse(inputField.text);
                    break;
                case "HPPlayer":
                    hpPlayer = int.Parse(inputField.text);
                    break;
                case "HPEnemy":
                    hpEnemy = int.Parse(inputField.text);
                    break;
                case "DmgPlayer":
                    dmgPlayer = int.Parse(inputField.text);
                    break;
                case "DmgEnemy":
                    dmgEnemy = int.Parse(inputField.text);
                    break;
                case "DefPlayer":
                    defPlayer = int.Parse(inputField.text);
                    break;
                case "DefEnemy":
                    defEnemy = int.Parse(inputField.text);
                    break;
                case "ChaPlayer":
                    chaPlayer = int.Parse(inputField.text);
                    break;
                case "ChaEnemy":
                    chaEnemy = int.Parse(inputField.text);
                    break;
            }
        }
        Medal medal = new Medal(roundMedal, sizeMedal);
        FighterAttributes playerFighter = new FighterAttributes(hpPlayer, dmgPlayer, defPlayer, chaPlayer);
        FighterAttributes enemyFighter = new FighterAttributes(hpEnemy, dmgEnemy, defEnemy, chaEnemy);

        CellsContainer cellsContainer = new CellsContainer(compiler, playerFighter, enemyFighter, medal);
        cellsContainer.Serialize(fileName);
        Debug.Log("Código exportado");
    }

    public void QuitGame()
    {
        panelManager.KillEvents();
        SceneManager.LoadScene("Menu");
    }

    public void ClearBlocks()
    {
        panelManager.Clear();
    }
}
