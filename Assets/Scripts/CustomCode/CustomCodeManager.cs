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

        int memoryCount = Directory.GetFiles("CustomCodes").Length;
        string fileName = (memoryCount + 1) + ".bin";

        variables = new Dictionary<string, int>();
        foreach (TMP_InputField inputField in inputFields)
        {
            string inputFieldName = inputField.name;
            int inputFieldValue = int.Parse(inputField.text);
            variables.Add(inputFieldName, inputFieldValue);
        }

        CellsContainer cellsContainer = new CellsContainer(compiler, variables, null);
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
