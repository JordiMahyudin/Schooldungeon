using UnityEditor;
using UnityEngine;

#if  (UNITY_EDITOR)  
[CustomEditor(typeof(NewEnemyMovement))]
public class Fieldofviewtest : Editor
{
    private void OnSceneGUI()
    {
        NewEnemyMovement fov = (NewEnemyMovement)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.m_radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.m_angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.m_angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.m_radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.m_radius);

        if (fov.m_canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.m_playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
#endif
