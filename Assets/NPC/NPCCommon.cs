using UnityEngine;
using System.Collections;

public static class NPCCommon
{
    public static bool TargetDistCheck(Vector3 p_start, Vector3 p_other)
    {
        Vector3 planeDist = new Vector3(p_start.x - p_other.x, (p_start.y - p_other.y) * 0.1f, p_start.z - p_other.z);
        if (Vector3.SqrMagnitude(planeDist) < 0.5f)
        {
            return true;
        }
        return false;
    }
}
