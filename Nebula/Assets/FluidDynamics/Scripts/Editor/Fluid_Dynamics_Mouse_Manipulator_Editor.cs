using UnityEditor;
namespace FluidDynamics
{
    [CustomEditor(typeof(Fluid_Dynamics_Mouse_Emitter))]
    public class Fluid_Dynamics_Mouse_Manipulator_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Fluid_Dynamics_Mouse_Emitter m_target = (Fluid_Dynamics_Mouse_Emitter)target;
            if (m_target.m_fluid == null)
            {
                EditorGUILayout.HelpBox("Fluid simulator not defined", MessageType.Error);
            }

            m_target.m_particlesStrength = EditorGUILayout.Slider("Particles Strength", m_target.m_particlesStrength / 1000f, 0.0f, 10.0f) * 1000f;
            m_target.m_particlesRadius = EditorGUILayout.Slider("Particles Radius", m_target.m_particlesRadius, 0.0f, 5.0f);
            m_target.m_velocityStrength = EditorGUILayout.Slider("Velocity Strength", m_target.m_velocityStrength, 0.0f, 10.0f);
            m_target.m_velocityRadius = EditorGUILayout.Slider("Velocity Radius", m_target.m_velocityRadius, 0.0f, 5.0f);
            EditorUtility.SetDirty(m_target);
        }
    }
}