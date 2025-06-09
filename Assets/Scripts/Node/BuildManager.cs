using NF.TD.BaseTurret;
using NF.TD.BuildArea;
using NF.TD.PlayerCore;
using NF.TD.Turret;
using NF.TD.TurretVisualRange;
using NF.TD.Extensions;
using UnityEngine;
using NF.TD.UICore;

namespace NF.TD.BuildCore 
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("More than one BuildManager in Scene");
                return;
            }
            instance = this;
        }

        private TurretScriptable turretToBuild;
        private Node selectedNode;

        public bool CanBuild { get { return turretToBuild != null; } }
        public bool HasMoney { get { return turretToBuild != null && PlayerStats.Money >= turretToBuild.turretCost; } }

        /// <summary>
        /// Instantiates and places the selected turret on the specified node.
        /// - Spawns the turret prefab (`turretModel`) at the node's build position.
        /// - Stores the spawned turret reference on the node.
        /// - Passes the selected turret data (`turretToBuild`) into the turret's script component (`TurretTower`) if available.
        /// </summary>
        public void BuildTurretOn(Node node)
        {
            // Check if the player has enough money and deduct the cost using the SpendMoney helper method.
            // SpendMoney returns true if the player had enough money and the amount was deducted.
            // This centralizes money-handling logic, preventing duplicated checks and increasing maintainability.
            if (!PlayerStatsExtension.SpendMoney(turretToBuild.turretCost))
            {
                Debug.Log("Not Enough Money");
                return;  // Exit if the player can't afford the turret.
            }

            GameObject turret = Instantiate(turretToBuild.turretModel, node.GetBuildPosition(), Quaternion.identity);
            node.turret = turret;

            TurretTower turretScript = turret.GetComponent<TurretTower>();
            if (turretScript != null)
            {
                turretScript.turretData = turretToBuild.Clone();
            }

            Debug.Log("Turret Build, Money Left " + PlayerStats.Money);
        }

        /// <summary>
        /// Selects the given Node (with a turret) to display its turret's range visualizer.
        /// - Hides the range visualizer of the previously selected turret (if any).
        /// - Shows the range visualizer for the newly selected turret.
        /// - Updates the `selectedNode` reference.
        /// </summary>
        public void SelectNode(Node node)
        {
            //Hide previous selected visualizer
            if (selectedNode != null && selectedNode.turret != null)
            {
                TurretRangeVisualizer prevVisualizer = selectedNode.turret.GetComponentInChildren<TurretRangeVisualizer>();
                if (prevVisualizer != null)
                {
                    prevVisualizer.SetVisualizerVisible(false);
                }
            }

            selectedNode = node;
            turretToBuild = null;

            //Show selected turret's visualizer
            if (selectedNode != null && selectedNode.turret != null)
            {
                TurretRangeVisualizer newVisualizer = selectedNode.turret.GetComponentInChildren<TurretRangeVisualizer>();
                if (newVisualizer != null)
                {
                    newVisualizer.SetVisualizerVisible(true);
                }
            }

            UIManager.Instance.ShowUpgradeShop(node);
        }

        public void SelectTurretToBuild(TurretScriptable turret)
        {
            //Hide any previously selected turret’s visualizer
            if (selectedNode != null && selectedNode.turret != null)
            {
                TurretRangeVisualizer visualizer = selectedNode.turret.GetComponentInChildren<TurretRangeVisualizer>();
                if (visualizer != null)
                {
                    visualizer.SetVisualizerVisible(false);
                }
            }

            turretToBuild = turret;
            selectedNode = null;

            UIManager.Instance.HideUpgradeShop();

        }

        public void DeselectTurret()
        {
            //Hide visualizer
            if (selectedNode != null && selectedNode.turret != null)
            {
                TurretRangeVisualizer visualizer = selectedNode.turret.GetComponentInChildren<TurretRangeVisualizer>();
                if (visualizer != null)
                {
                    visualizer.SetVisualizerVisible(false);
                }
            }

            turretToBuild = null;
            selectedNode = null;

            UIManager.Instance.HideUpgradeShop();
        }
    }
}
