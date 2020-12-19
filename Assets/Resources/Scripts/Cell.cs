using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cell : MonoBehaviour
{
    private RectTransform recTransform;
    [SerializeField] private TextMeshProUGUI text;
    private Button button;
    public bool xSign;
    public bool L, R, U, D;
    public int borderNumber=0;
    public int index;
    private void Awake()
    {
        recTransform = GetComponent<RectTransform>();
        button = GetComponent<Button>();
    }
    public void SetCell(int size,Vector2 position,int indexNumber)
    {
        recTransform.sizeDelta = new Vector2(size,size);
        recTransform.position = position;
        text.rectTransform.sizeDelta = new Vector2(size, size);
        index = indexNumber;
    }
    public void SetX()
    {
        button.interactable = false;
        text.text = "X";
        xSign = true;
        IncreaseBorderNumber();
        SetBools();
        CheckBorders();
    }
    private void IncreaseBorderNumber()
    {
        int gridSize = GameManager.INSTANCE.levelManager.n;
        int temp;
        temp = (index + 1) % gridSize == 0 ? GameManager.INSTANCE.cells[index].borderNumber : GameManager.INSTANCE.cells[index + 1].borderNumber++;
        temp = (index - gridSize) < 0 ? GameManager.INSTANCE.cells[index].borderNumber : GameManager.INSTANCE.cells[index - gridSize].borderNumber++;
        temp = (index + gridSize) > gridSize * gridSize ? GameManager.INSTANCE.cells[index].borderNumber : GameManager.INSTANCE.cells[index + gridSize].borderNumber++;
        temp = (index - 1) % gridSize == (gridSize - 1) || (index - 1) < 0 ? GameManager.INSTANCE.cells[index].borderNumber : GameManager.INSTANCE.cells[index - 1].borderNumber++;
        GameManager.INSTANCE.cells[index].borderNumber++;
    }
    private void SetBools()
    {
        bool temp;
        int gridSize = GameManager.INSTANCE.levelManager.n;

        temp = (index + 1) % gridSize == 0 ? GameManager.INSTANCE.cells[index].R : GameManager.INSTANCE.cells[index + 1].L=true;
        temp = (index - gridSize) < 0 ? GameManager.INSTANCE.cells[index].D : GameManager.INSTANCE.cells[index - gridSize].U = true ;
        temp = (index + gridSize) > gridSize * gridSize ? GameManager.INSTANCE.cells[index].U : GameManager.INSTANCE.cells[index + gridSize].D=true;
        temp = (index - 1) % gridSize == (gridSize - 1) || (index - 1) < 0 ? GameManager.INSTANCE.cells[index].L : GameManager.INSTANCE.cells[index - 1].R=true;
    }
    private void CheckBorders()
    {
        int temp = GameManager.INSTANCE.cells[index].borderNumber;
        ;
        int gridSize = GameManager.INSTANCE.levelManager.n;
        
        if ((index-1)%gridSize!=(gridSize-1) &&(index-1)>=0)
        {
            
            if (GameManager.INSTANCE.cells[index-1].xSign)
            {
                if(GameManager.INSTANCE.cells[index-1].borderNumber>=3)
                {
                    GameManager.INSTANCE.cells[index - 1].MatchThree();
                }
                
                if(temp>=3)
                {
                    GameManager.INSTANCE.cells[index].MatchThree();
                }
            }
        }
        if((index+1)%gridSize!=0)
        {
            
            if (GameManager.INSTANCE.cells[index+1].xSign)
            {
               


                if (GameManager.INSTANCE.cells[index+1].borderNumber>=3)
                {
                    Debug.Log("1");
                    GameManager.INSTANCE.cells[index + 1].MatchThree();
                }
                if (temp >= 3)
                {
                    Debug.Log("2");
                    GameManager.INSTANCE.cells[index].MatchThree();
                }
            }
        }
        if((index+gridSize)<gridSize*gridSize)
        {
          
            if (GameManager.INSTANCE.cells[index+gridSize].xSign)
            {

                if(GameManager.INSTANCE.cells[index+gridSize].borderNumber>=3)
                {
                    GameManager.INSTANCE.cells[index + gridSize].MatchThree();
                }
                if (temp >= 3)
                {
                    GameManager.INSTANCE.cells[index].MatchThree();
                }

            }
        }
        if ((index - gridSize) >0 )
        {
           
            if (GameManager.INSTANCE.cells[index - gridSize].xSign)
            {

                if (GameManager.INSTANCE.cells[index - gridSize].borderNumber >= 3)
                {
                    GameManager.INSTANCE.cells[index - gridSize].MatchThree();
                }
                if (temp >= 3)
                {
                    GameManager.INSTANCE.cells[index].MatchThree();
                }

            }
        }
    }
    private void ResetCell()
    {
        
        text.text = "";
        xSign = false;
        borderNumber--;
        if(borderNumber<0)
        {
            borderNumber = 0;
        }
        ResetBools();
        if (!button.interactable)
        { DecreaseBorderNumber(); }
        button.interactable = true;
    }
    private void ResetBools()
    {
        bool temp;
        int gridSize = GameManager.INSTANCE.levelManager.n;

        temp = (index + 1) % gridSize == 0 ? GameManager.INSTANCE.cells[index].R : GameManager.INSTANCE.cells[index + 1].L = false;
        temp = (index - gridSize) < 0 ? GameManager.INSTANCE.cells[index].D : GameManager.INSTANCE.cells[index - gridSize].U = false;
        temp = (index + gridSize) > gridSize * gridSize ? GameManager.INSTANCE.cells[index].U : GameManager.INSTANCE.cells[index + gridSize].D = false;
        temp = (index - 1) % gridSize == (gridSize - 1) || (index - 1) < 0 ? GameManager.INSTANCE.cells[index].L : GameManager.INSTANCE.cells[index - 1].R = false;
    }
    private void DecreaseBorderNumber()
    {
        int gridSize = GameManager.INSTANCE.levelManager.n;
        int temp;
        temp = (index + 1) % gridSize == 0 ? 0 : GameManager.INSTANCE.cells[index + 1].borderNumber--;
        Debug.Log(temp);
        if(temp==-1)
        {
            GameManager.INSTANCE.cells[index + 1].borderNumber = 0;
        }
        temp = (index - gridSize) < 0 ? 0 : GameManager.INSTANCE.cells[index - gridSize].borderNumber--;
        Debug.Log(temp);
        if (temp == -1)
        {
            GameManager.INSTANCE.cells[index -gridSize].borderNumber = 0;
        }
        temp = (index + gridSize) > gridSize * gridSize ? 0 : GameManager.INSTANCE.cells[index + gridSize].borderNumber--;
        Debug.Log(temp);
        if (temp  == -1)
        {
            GameManager.INSTANCE.cells[index +gridSize].borderNumber = 0;
        }
        temp = (index - 1) % gridSize == (gridSize - 1) || (index - 1) < 0 ? 0 : GameManager.INSTANCE.cells[index - 1].borderNumber--;
        Debug.Log(temp);
        if (temp  == -1)
        {
            GameManager.INSTANCE.cells[index - 1].borderNumber = 0;
        }
    }
    
    private void MatchThree()
    {
        
        if (GameManager.INSTANCE.cells[index].L)
        {
            GameManager.INSTANCE.cells[index].L = false;
            GameManager.INSTANCE.cells[index - 1].R = false;
            GameManager.INSTANCE.cells[index - 1].ResetCell();
        }
        if(GameManager.INSTANCE.cells[index].R)
        {
            GameManager.INSTANCE.cells[index].R = false;
            GameManager.INSTANCE.cells[index + 1].L = false;
            GameManager.INSTANCE.cells[index + 1].ResetCell();

        }
        if (GameManager.INSTANCE.cells[index].U)
        {
            GameManager.INSTANCE.cells[index].U = false;
            GameManager.INSTANCE.cells[index + GameManager.INSTANCE.levelManager.n].D = false;
            GameManager.INSTANCE.cells[index + GameManager.INSTANCE.levelManager.n].ResetCell();
        }
        if (GameManager.INSTANCE.cells[index].D)
        {
            GameManager.INSTANCE.cells[index].D = false;
            GameManager.INSTANCE.cells[index - GameManager.INSTANCE.levelManager.n].U = false;
            GameManager.INSTANCE.cells[index - GameManager.INSTANCE.levelManager.n].ResetCell();
        }
        GameManager.INSTANCE.cells[index].ResetCell();
    }
}
