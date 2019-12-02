using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "World", menuName = "Level")]
public class Level : ScriptableObject
{
    [Header("Board Dimensions")]
    public int width;
    public int height;

    [Header("Starting Tiles")]
    public TileType[] boardLayout;

    [Header("Available Dots")]
    public GameObject[] dots;

    [Header("Score Goals")]
    public int[] scoreGoals;

    [Header("Camera Scalar")]
    public float aspectRatio;
    public float padding;
    public float yOffset;

    [Header("End Game Requirements")]
    public EndGameRequirements endGameRequirements;
    public BlankGoal[] levelGoals;

    // [Header("Background Image")]
    // public Image bgimg;
}
