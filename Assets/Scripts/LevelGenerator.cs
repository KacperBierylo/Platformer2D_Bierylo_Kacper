using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int gNumber = 0;
    public int maxGNumber = 6;
    public static LevelGenerator instance;
    public Transform levelStartPoint;
    public List<LevelPieceBasic> levelPrefabs = new List<LevelPieceBasic>();
    public List<LevelPieceBasic> pieces = new List<LevelPieceBasic>();

    public LevelPieceBasic startPlatformPrefab;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        AddPiece();
        AddPiece();
    }

    public void  ShowPiece(LevelPieceBasic piece)
    {
        piece.transform.SetParent(this.transform, false);
        if (pieces.Count < 1)
            piece.transform.position = new Vector2(
                levelStartPoint.position.x - piece.startPoint.localPosition.x,
                levelStartPoint.position.y - piece.startPoint.localPosition.y);
        else
            piece.transform.position = new Vector2(
            pieces[pieces.Count - 1].exitPoint.position.x - pieces[pieces.Count - 1].startPoint.position.x,
            pieces[pieces.Count - 1].exitPoint.position.y - pieces[pieces.Count - 1].startPoint.position.y);

        pieces.Add(piece);

       
    }
    // Update is called once per frame
  
    public void AddPiece()
    {
        gNumber++;
        if (gNumber < maxGNumber)
        {
            int randomIndex = Random.Range(0, levelPrefabs.Count - 1);
            LevelPieceBasic piece = (LevelPieceBasic)Instantiate(levelPrefabs[randomIndex]);
            piece.transform.SetParent(this.transform, false);

            if (pieces.Count < 1)
                piece.transform.position = levelStartPoint.position;
            else
                piece.transform.position = pieces[pieces.Count - 1].exitPoint.position;

            pieces.Add(piece);
        }
        else if(gNumber == maxGNumber)
        {
            LevelPieceBasic piece = (LevelPieceBasic)Instantiate(levelPrefabs[levelPrefabs.Count - 1]);
            piece.transform.SetParent(this.transform, false);

            if (pieces.Count < 1)
                piece.transform.position = levelStartPoint.position;
            else
                piece.transform.position = pieces[pieces.Count - 1].exitPoint.position;

            pieces.Add(piece);
        }
        else
        {

        }
    }

    public void RemoveOldestPiece()
    {
        if (pieces.Count > 1)
        {
            LevelPieceBasic oldestPiece = pieces[0];
            pieces.RemoveAt(0);
            Destroy(oldestPiece.gameObject);
        }
    }
}
