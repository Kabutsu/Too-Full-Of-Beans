using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public List<Button> buttonList;

    private int SelectedTrackId = 0;

    public Color StandardButtonColor;
    public Color HighlightedButtonColor;

    private void Start()
    {
        foreach(var button in buttonList)
        {
            SetColor(button, StandardButtonColor);
        }

        SetColor(buttonList[SelectedTrackId], HighlightedButtonColor);
    }

    public void SelectButton(ControllerDirection direction)
    {
        if (direction == ControllerDirection.Left)
        {
            if (SelectedTrackId > 0)
            {
                SetColor(buttonList[SelectedTrackId], StandardButtonColor);
                SelectedTrackId--;
                SetColor(buttonList[SelectedTrackId], HighlightedButtonColor);
            }
        }
        else if (direction == ControllerDirection.Right)
        {
            if (SelectedTrackId < buttonList.Count - 1)
            {
                SetColor(buttonList[SelectedTrackId], StandardButtonColor);
                SelectedTrackId++;
                SetColor(buttonList[SelectedTrackId], HighlightedButtonColor);
            }
        }
    }

    private void SetColor(Button button, Color color)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = color;
        button.colors = colors;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}

public enum ControllerDirection
{
    Left = -1,
    Right = 1,
    None = 0,
}
