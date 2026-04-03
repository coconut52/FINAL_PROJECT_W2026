using UnityEngine;

[CreateAssetMenu(fileName = "PlantInfo", menuName = "Plants/Plant Info")]
public class PlantInfoData : ScriptableObject
{
    public string plantName;

    [TextArea(4, 8)]
    public string description;

    public Sprite plantImage;
}