using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class SolidWorksSceneNavigation
{
    static SolidWorksSceneNavigation()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        Event e = Event.current;

        if (e.type == EventType.MouseDrag && e.button == 2)
        {
            if (e.control)
            {
                float panSpeed = sceneView.size * 0.002f;
                Vector3 right = sceneView.camera.transform.right;
                Vector3 up = sceneView.camera.transform.up;

                sceneView.pivot -= right * (e.delta.x * panSpeed);
                sceneView.pivot += up * (e.delta.y * panSpeed);
                
                sceneView.Repaint();
                e.Use();
            }
            else if (!e.alt && !e.shift)
            {
                float orbitSpeed = 0.3f;
                Quaternion rotation = sceneView.rotation;

                Quaternion yaw = Quaternion.AngleAxis(e.delta.x * orbitSpeed, Vector3.up);
                Quaternion pitch = Quaternion.AngleAxis(e.delta.y * orbitSpeed, rotation * Vector3.right);

                sceneView.rotation = yaw * pitch * rotation;
                
                sceneView.Repaint();
                e.Use();
            }
        }
    }
}