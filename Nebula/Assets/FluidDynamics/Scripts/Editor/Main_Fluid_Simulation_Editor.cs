using UnityEngine;
using UnityEditor;
namespace FluidDynamics
{
    [CustomEditor(typeof(Main_Fluid_Simulation))]
    public class Main_Fluid_Simulation_Editor : Editor
    {
        static bool m_showAdvanced = false;
        public static bool Foldout(bool foldout, GUIContent content, bool toggleOnLabelClick, GUIStyle style)
        {
            Rect position = GUILayoutUtility.GetRect(40f, 40f, 16f, 16f, style);
            return EditorGUI.Foldout(position, foldout, content, toggleOnLabelClick, style);
        }
        public static bool Foldout(bool foldout, string content, bool toggleOnLabelClick, GUIStyle style)
        {
            return Foldout(foldout, new GUIContent(content), toggleOnLabelClick, style);
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Main_Fluid_Simulation m_target = (Main_Fluid_Simulation)target;
            /// Quality Settings
            string[] options = { "High", "Medium", "Low" };
            int quality = 0;
            if (m_target.Resolution == 256)
            {
                quality = 1;
            }
            else if (m_target.Resolution == 128)
            {
                quality = 2;
            }
            quality = EditorGUILayout.Popup("Quality", quality, options);
            switch (quality)
            {
                case 0:
                    m_target.Resolution = 512;
                    break;
                case 1:
                    m_target.Resolution = 256;
                    break;
                default:
                    m_target.Resolution = 128;
                    break;
            }
            m_target.Vorticity = EditorGUILayout.Slider("Vorticity", m_target.Vorticity, 0.0f, 50.0f);
            m_target.Viscosity = EditorGUILayout.Slider("Viscosity", m_target.Viscosity * 10.0f, 0.0f, 1.0f) / 10.0f;

            m_showAdvanced = Foldout(m_showAdvanced, "Advanced", true, EditorStyles.foldout);
            if (m_showAdvanced)
            {
                EditorGUI.indentLevel++;
                m_target.m_cacheVelocity = EditorGUILayout.Toggle("Cache Velocity", m_target.m_cacheVelocity);
                m_target.Iterations = EditorGUILayout.IntSlider("Simulation Quality", m_target.Iterations, 0, 100);
                m_target.Speed = EditorGUILayout.Slider("Simulation Speed", m_target.Speed, 0.0f, 1000.0f);

                float disp_min = 0.9f;
                float disp_max = 1.0f;
                float range = disp_max - disp_min;
                float vel_min = 0.97f;
                float vel_max = 1.0f;
                range = vel_max - vel_min;
                float velocity_dissipation = Mathf.Clamp(m_target.VelocityDissipation, vel_min, vel_max);
                velocity_dissipation = (velocity_dissipation - vel_min) / range;
                velocity_dissipation = EditorGUILayout.Slider("Velocity Dissipation", velocity_dissipation, 0.0f, 1.0f);
                m_target.VelocityDissipation = velocity_dissipation * range + vel_min;
                EditorGUI.indentLevel--;
            }
            
            m_target.ParticlesResolution = (int)EditorGUILayout.Slider("Area Resolution", m_target.ParticlesResolution, 128.0f, 8192.0f);
            float disp_min2 = 0.9f;
            float disp_max2 = 1.0f;
            float range2 = disp_max2 - disp_min2;
            float density_dissipation = Mathf.Clamp(m_target.Dissipation, disp_min2, disp_max2);
            density_dissipation = (density_dissipation - disp_min2) / range2;
            density_dissipation = EditorGUILayout.Slider("Particle Life", density_dissipation, 0.0f, 1.0f);
            m_target.Dissipation = density_dissipation * range2 + disp_min2;
            EditorUtility.SetDirty(m_target);
        }
    }
}
