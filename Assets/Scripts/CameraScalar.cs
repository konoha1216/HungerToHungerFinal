using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScalar : MonoBehaviour
{
    private Board board;
    public float cameraOffset;
    public float aspectRatio=0.875f;
    public float padding=0;
    public float yOffset=2;

    // [Header ("Scriptable Object Stuff")]
    // public World world;
    // public int level;

    private void SetCameraScalar(){
        if(board.world != null){
            if(board.world.levels[board.level] != null){
                aspectRatio = board.world.levels[board.level].aspectRatio;
                padding = board.world.levels[board.level].padding;
                yOffset = board.world.levels[board.level].yOffset;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        SetCameraScalar();
        if(board!=null){
            RepositionCamera(board.width-1, board.height-1);
        }
    }
    void RepositionCamera(float x, float y){
        Vector3 tempPosition = new Vector3(x/2, y/2+yOffset, cameraOffset);
        transform.position = tempPosition;
        if(board.width >= board.height){
            Camera.main.orthographicSize = (board.width/2 + padding)/ aspectRatio;
        }else{
            Camera.main.orthographicSize = board.height/2 + padding;
        }
    }
}
