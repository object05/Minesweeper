using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineFieldRenderer : MonoBehaviour
{
    private MineField field;

    // Start is called before the first frame update
    void Start()
    {
        field = gameObject.GetComponent<MineField>();

    }

    // Update is called once per frame
    void Update()
    {
        drawBoard();
    }

    void drawBoard()
    {
        for(int x = 0; x < field.width; x++)
        {
            for(int y = 0; y < field.height; y++)
            {
                if(field == null)
                {
                    Debug.Log("field null");
                }
                if (field.minefield == null)
                {
                    Debug.Log("minefield null");
                }
                field.minefield[x, y].GetComponent<SpriteRenderer>().sprite = Assets.dictionary[RegionNames.MINECELL];
                field.minefield[x, y].gameObject.transform.position = new Vector3(x, y,5);
            }
        }
    }
}
