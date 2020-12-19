using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Canvas background;
    [SerializeField] private InputField inputField;
    public int n;
    float offsetHeight,offsetWidth;
    int backgroundSize;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private GameObject cellParent;
    public void CreateLevel()
    {
        
        n = int.Parse(inputField.text);
        if(background.pixelRect.width>background.pixelRect.height)
        {
           offsetHeight  = (int)background.pixelRect.height % n;//offset height for grid 
            backgroundSize = (int)background.pixelRect.height - (int)offsetHeight;//grid size calculation
            offsetWidth = (int)background.pixelRect.width - backgroundSize;//offset width for grid
            Debug.Log(offsetHeight);

            CreateGrid(backgroundSize / n);//create grid with cellsize parameter 
        }
        else
        {
            offsetWidth = (int)background.pixelRect.width % n;//offset width for grid
            backgroundSize = (int)background.pixelRect.width - (int)offsetWidth;
            offsetHeight = (int)background.pixelRect.height - backgroundSize;//offset height for grid
            CreateGrid(backgroundSize / n);//create grid with cellsize parameter 
        }
    }
    private void CreateGrid(int cellSize)
    {
        Vector2 startPosition = new Vector2(offsetWidth / 2+cellSize/2, offsetHeight / 2+ cellSize / 2);
        
        for(int i=0;i<n*n;i++)
        {
            Cell newCell = Instantiate(cellPrefab, cellParent.transform);
            newCell.SetCell(cellSize,startPosition +Vector2.right*(i%n)*cellSize+Vector2.up*(i/n)*cellSize,i);
            GameManager.INSTANCE.cells.Add(newCell);
        }
    }
}
