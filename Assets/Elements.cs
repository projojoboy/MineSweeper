using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elements : MonoBehaviour {

    public bool mine;

    public Sprite[] emptyTextures;
    public Sprite mineTexture;

	// Use this for initialization
	void Start () {
        mine = Random.value < 0.15;

        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        Grid.elements[x, y] = this;
    }

    public void LoadTexture(int adjacentCount)
    {
        if (mine)
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        else
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
    }

    public bool IsCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "default";
    }

    public void OnMouseUpAsButton()
    {
        if (mine)
        {
            // ToDo: uncover all mines
            Grid.UncoverMines();

            // game over
            Debug.Log("you lose");
        }
        else
        {
            // ToDo show adjacent mine number
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            LoadTexture(Grid.AdjacentMines(x, y));

            // ToDo uncover area without mines
            Grid.FFuncover(x, y, new bool[Grid.w, Grid.h]);

            // ToDo find out if the game was won now
            if (Grid.IsFinished())
                Debug.Log("U win");
        }
    }
}
