using UnityEngine;
namespace FluidDynamics
{
    [AddComponentMenu("Fluid Dynamics/Emitters/Fluid Velocity Emitter")]
    [ExecuteInEditMode]
    public class Fluid_Dynamics_Velocity_Emitter : MonoBehaviour
    {
        public Main_Fluid_Simulation m_fluid;
        [HideInInspector]
        public bool m_useScaleAsSize = true;
        [HideInInspector]
        public bool m_velocityFromMovement = false;
        [HideInInspector]
        public float m_fluidVelocitySpeed = 1f;
        [HideInInspector]
        public float m_scaleVelocity = 1f;
        [HideInInspector]
        public float m_radius = 0.1f;
        [HideInInspector]
        public bool m_showGizmo = false;
        private Vector3 m_direction;
        private Vector3 m_speed;
        private Vector3 m_prevPosition;
        private Collider m_tempCol;
        private Renderer m_tempRend;
        private Ray ray;
        private RaycastHit hitInfo;
        private float fWidth;
        private float fRadius;

        private void Start()
        {
            // causes one time error upon instantiation that work themselves out
            m_tempCol = m_fluid.GetComponent<Collider>();
            m_tempRend = m_fluid.GetComponent<Renderer>();
            
            m_prevPosition = transform.position;
            m_direction = GetDirection();
        }
        public float GetRadius()
        {
            if (m_useScaleAsSize)
            {
                return Mathf.Max(transform.localScale.x, transform.localScale.y);
            }
            return m_radius;
        }
        public Vector3 GetDirection()
        {
            if (m_velocityFromMovement)
            {
                return transform.position - m_prevPosition;
            }
            return transform.rotation * Vector3.down;
        }
        private void UpdateValues()
        {
            m_direction = GetDirection();
            if (m_direction != Vector3.zero)
            {
                m_direction.Normalize();
                m_speed = m_direction * m_fluidVelocitySpeed * Time.deltaTime;
            }
            else
            {
                m_speed = Vector3.zero;
            }
            m_prevPosition = transform.position;
        }
        private void Update()
        {
            UpdateValues();
        }
        private void LateUpdate()
        {
            if (m_fluid)
            {
                Vector3 currentPosition = transform.position;
                if (m_speed != Vector3.zero)
                {
                    if (m_tempCol == null)
                        m_tempCol = m_fluid.GetComponent<Collider>();
                    if (m_tempRend == null)
                        m_tempRend = m_fluid.GetComponent<Renderer>();
                    
                    ray = new Ray(currentPosition, Vector3.forward);
                    if (m_tempCol.Raycast(ray, out hitInfo, 10))
                    {
                        fWidth = m_tempRend.bounds.extents.x * 2f;
                        fRadius = (GetRadius() * m_fluid.GetWidth()) / fWidth;
                        m_fluid.AddVelocity(hitInfo.textureCoord, -m_speed, fRadius);
                    }
                }
            }
        }
        private void DrawGizmo()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, GetRadius());
            if (!m_velocityFromMovement || Application.isPlaying)
            {
                Vector3 end_pos = transform.position - (m_direction * (2f + (m_fluidVelocitySpeed / 500f) * 5f));
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(transform.position, end_pos);
                Vector3 back_dir = (transform.position - end_pos);
                back_dir.Normalize();
                float angle = 25 * Mathf.Deg2Rad;
                float cos = Mathf.Cos(angle);
                float sin = Mathf.Sin(angle);
                Vector3 arrow = new Vector3(back_dir.x * cos - back_dir.y * sin,
                                            back_dir.x * sin + back_dir.y * cos, 0f);
                Gizmos.DrawLine(end_pos, end_pos + arrow * 0.5f);
                cos = Mathf.Cos(-angle);
                sin = Mathf.Sin(-angle);
                arrow = new Vector3(back_dir.x * cos - back_dir.y * sin,
                                    back_dir.x * sin + back_dir.y * cos, 0f);
                Gizmos.DrawLine(end_pos, end_pos + arrow * 0.5f);
            }
        }
        private void OnDrawGizmosSelected()
        {
            DrawGizmo();
        }
        private void OnDrawGizmos()
        {
            if (m_showGizmo)
            {
                DrawGizmo();
            }
        }
    }
}