using UnityEditor;
namespace FluidDynamics
{
    [CustomEditor(typeof(Fluid_Dynamics_Particles_Emitter))]
    public class Fluid_Dynamics_Particles_Area_Manipulator_Editor : Editor
    {
        bool m_debug = false;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Fluid_Dynamics_Particles_Emitter m_target = (Fluid_Dynamics_Particles_Emitter)target;
            if (m_target.m_mainSimulation == null)
            {
                EditorGUILayout.HelpBox("Fluid particles not defined", MessageType.Error);
            }
            m_target.m_strength = EditorGUILayout.Slider("Strength", m_target.m_strength / 1000f, 0.0f, 10.0f) * 1000f;
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
