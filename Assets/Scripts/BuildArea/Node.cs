using NF.TD.BuildCore;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NF.TD.BuildArea
{
    /// <summary>
    /// Handles player interaction with a buildable platform (node) in the game world.
    /// - Manages turret placement and selection.
    /// - Provides visual feedback (color changes) for whether building is allowed.
    /// - Interacts with the BuildManager to build or select turrets.
    /// </summary>
    public class Node : MonoBehaviour
    {
        public Vector3 positionOffset;

        [Header("Color Feeback if Buildable")]
        public Color hoverColorBuildable = Color.green;
        public Color hoverColorBlocked = Color.red;

        [Header("Optional")]
        public GameObject turret;

        private Renderer rend;
        private Color startColor;

        private bool isHovered = false;

        BuildManager buildManager;

        private void Start()
        {
            rend = GetComponent<Renderer>();
            startColor = rend.material.color;

            buildManager = BuildManager.instance;
        }

        public Vector3 GetBuildPosition()
        {
            return transform.position + positionOffset;
        }

        private void Update()
        {
            if (!isHovered)
                return;

            if (!buildManager.CanBuild || turret != null)
            {
                rend.material.color = hoverColorBlocked;
            }
            else
            {
                rend.material.color = buildManager.HasMoney ? hoverColorBuildable : hoverColorBlocked;
            }
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (turret != null)
            {
                Debug.Log("Can't build here");
                buildManager.SelectNode(this);
                return;
            }

            if (!buildManager.CanBuild)
                return;

            buildManager.BuildTurretOn(this);
        }

        private void OnMouseEnter()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            // Don't show any color if no turret selected
            if (!buildManager.CanBuild)
                return;

            // If a turret is already built, show blocked color
            if (turret != null)
            {
                rend.material.color = hoverColorBlocked;
            }
            else
            {
                // Green if has money, red if not
                rend.material.color = buildManager.HasMoney ? hoverColorBuildable : hoverColorBlocked;
            }

            isHovered = true;
        }

        private void OnMouseExit()
        {
            rend.material.color = startColor;
            isHovered = false;
        }
    }

}
