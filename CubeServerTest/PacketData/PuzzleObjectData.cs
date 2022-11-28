using System;
using System.Collections.Generic;

[Serializable]
public abstract class PuzzleObjectData
{
    public int cubeIndex;
    public int wallIndex;
    public ObjectType type;
    public abstract void Serialize(byte[] buffer, int startPos);
    public abstract string ToJson();
    public abstract int Size();
}