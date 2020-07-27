using UnityEngine;
namespace FluidDynamics
{
    [AddComponentMenu("Fluid Dynamics/Emitters/Fluid Mouse Emitter")]
    public class Fluid_Dynamics_Mouse_Emitter : MonoBehaviour
    {
        #region Variables
        public Main_Fluid_Simulation m_fluid;
        public states updateMode;
        public bool m_alwaysOn = false;
        private Vector3 m_previousMousePosition;
        [HideInInspector]
        public float m_velocityStrength = 10f;
        [HideInInspector]
        public float m_velocityRadius = 5f;
        [HideInInspector]
        public float m_particlesStrength = 1f;
        [HideInInspector]
        public float m_particlesRadius = 5f;
        private Collider m_tempCol;
        private Renderer m_tempRend;
        private Ray ray;
        private RaycastHit hitInfo;
        private float fWidth;
        private float fRadius;
        private Vector3 direction;
        private Vector3 m_mousePos;
        [SerializeField] private Camera fluidRTCam;
        private Camera _mainCamera;
        [SerializeField, Range(0, 1f)] private float wrapOffset = 0.4f;
        private GasMode _gasMode;

        #endregion
        
        private void Start()
        {
            _mainCamera = Camera.main;
            m_tempCol = m_fluid.GetComponent<Collider>();
            m_tempRend = m_fluid.GetComponent<Renderer>();
            _gasMode = FindObjectOfType<GasGiantFluidController>().gasMode;
        }
        private void DrawGizmo()
        {
            float col = m_particlesStrength / 10000.0f;
            Gizmos.color = Color.Lerp(Color.yellow, Color.red, col);
            var position = transform.position;
            Gizmos.DrawWireSphere(position, m_particlesRadius);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(position, m_velocityRadius);
        }
        private void OnDrawGizmosSelected()
        {
            DrawGizmo();
        }
        private void OnDrawGizmos()
        {
            DrawGizmo();
        }
        private void Update()
        {
            if (updateMode == states.Update)
                ManipulateParticles();
        }
        private void LateUpdate()
        {
            if (updateMode == states.LateUpdate)
                ManipulateParticles();
        }
        private void FixedUpdate()
        {
            if (updateMode == states.FixedUpdate)
                ManipulateParticles();
        }
        private void ManipulateParticles()
        {
            if (Input.GetMouseButton(0) && _gasMode.CurrentInteractMode == GasMode.InteractMode.Painting || m_alwaysOn)
            {
                m_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
                ray = _mainCamera.ScreenPointToRay(m_mousePos);
                //Debug.DrawRay(ray.origin, ray.direction, Color.magenta);
                
                Vector3 uvWorldPosition = Vector3.zero;
                if (!HitTestUVPosition(ray, ref uvWorldPosition)) return;
                
                Ray fluidRay = new Ray(fluidRTCam.transform.position + uvWorldPosition, Vector3.forward);
                //Debug.DrawRay(fluidRay.origin, fluidRay.direction, Color.green, 1f);
                
                if (m_tempCol.Raycast(fluidRay, out hitInfo, 100))
                {
                    fWidth = m_tempRend.bounds.extents.x * 2f;
                    fRadius = (m_particlesRadius * m_fluid.GetParticlesWidth()) / fWidth;
                    m_fluid.AddParticles(hitInfo.textureCoord, fRadius, m_particlesStrength * Time.deltaTime);
                    m_fluid.AddParticles(hitInfo.textureCoord - new Vector2(0, wrapOffset), fRadius, m_particlesStrength * Time.deltaTime);
                }
                
            }
            /* Default Fluid Dynamic Mouse Right Click Functionality
            if (Input.GetMouseButtonDown(1))
            {
                m_previousMousePosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(1))// || m_alwaysOn)
            {
                m_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
                ray = _mainCamera.ScreenPointToRay(m_mousePos);
                
                Vector3 uvPos = Vector3.zero;
                if (!HitTestUVPosition(ray, ref uvPos)) return;

                Ray fluidRay = new Ray(fluidRTCam.transform.position + uvPos, Vector3.forward);

                // Asset's original implementation
                if (m_tempCol.Raycast(fluidRay, out hitInfo, 100))
                {
                    direction = (Input.mousePosition - m_previousMousePosition) * (m_velocityStrength * Time.deltaTime);
                    fWidth = m_tempRend.bounds.extents.x * 2f;
                    fRadius = (m_velocityRadius * m_fluid.GetWidth()) / fWidth;

                    if (Input.GetMouseButton(0))
                    {
                        m_fluid.AddVelocity(hitInfo.textureCoord, -direction, fRadius);
                        m_fluid.AddVelocity(hitInfo.textureCoord - new Vector2(0, wrapOffset), -direction, fRadius);
                    }
                    else
                    {
                        m_fluid.AddVelocity(hitInfo.textureCoord, direction, fRadius);
                        m_fluid.AddVelocity(hitInfo.textureCoord - new Vector2(0, wrapOffset), direction, fRadius);

                    }
                }
                m_previousMousePosition = Input.mousePosition;
            }
            */
        }
        
        private bool HitTestUVPosition(Ray cursorRay, ref Vector3 uvWorldPosition)
        {
            if (Physics.Raycast(cursorRay, out var hit, 1000, LayerMask.GetMask("Default")))
            {
                Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
                var orthographicSize = fluidRTCam.orthographicSize;
                uvWorldPosition.x = -(pixelUV.y * (orthographicSize * 2) - orthographicSize); //To center the UV on X
                uvWorldPosition.y = pixelUV.x * (orthographicSize * 2) - orthographicSize; //To center the UV on Y
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}