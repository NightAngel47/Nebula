using UnityEditor;
namespace FluidDynamics
{
    [CustomEditor(typeof(Fluid_Dynamics_Velocity_Emitter))]
    public class Fluid_Dynamics_Velocity_Manipulator_Editor : Editor
    {
        bool m_debug = false;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Fluid_Dynamics_Velocity_Emitter m_target = (Fluid_Dynamics_Velocity_Emitter)target;
            if (m_target.m_fluid == null)
            {
                EditorGUILayout.HelpBox("Fluid not defined", MessageType.Error);
            }
            m_target.m_fluidVelocitySpeed = EditorGUILayout.Slider("Speed", m_target.m_fluidVelocitySpeed, 0.0f, 50.0f);
            string[] direction_options = { "Global Rotation", "Movement Direction" };
            int direction = m_target.m_velocityFromMovement ? 1 : 0;
            direction = EditorGUILayout.Popup("Direction", direction, direction_options);
            m_target.m_velocityFromMovement = (direction == 1) ? true : false;
            m_target.m_useScaleAsSize = !EditorGUILayout.Toggle("Set Size Manually", !m_target.m_useScaleAsSize);
            if (m_target.m_useScaleAsSize)
            {
                EditorGUILayout.HelpBox(" Using global scale as size", MessageType.None);
            }
            else
            {
                ++EditorGUI.indentLevel;
                m_target.m_radius = EditorGUILayout.Slider("Radius", m_target.m_radius, 0.0f, 5.0f);
                --EditorGUI.indentLevel;
            }
            m_debug = EditorGUILayout.Foldout(m_debug, "Debug");
            if (m_debug)
            {
                ++EditorGUI.indentLevel;
                m_target.m_showGizmo = EditorGUILayout.Toggle("Draw Gizmo", m_target.m_showGizmo);
                --EditorGUI.indentLevel;
            }
            EditorUtility.SetDirty(m_target);
        }
    }
}