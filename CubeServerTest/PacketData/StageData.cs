using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class StageData
{
    public int stageId;
    public List<PuzzleObjectData> objectData = new List<PuzzleObjectData>();
}