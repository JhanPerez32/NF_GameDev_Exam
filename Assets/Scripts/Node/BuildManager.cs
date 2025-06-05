using NF.Main.Core.PlayerStateMachine;
using NF.TD.TurretVisualRange;
using UnityEngine;
using NF.TD.BuildArea;
using NF.TD.Turret;
using NF.TD.BaseTurret;
using NF.TD.TurretCore;

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

        public TurretScriptable turretToBuild;
        private Node selectedNode;

        public bool CanBuild { get { return turretToBuild != null; } }

        /// <summary>
        /// Instantiates and places the selected turret on the specified node.
        /// - Spawns the turret prefab (`turretModel`) at the node's build position.
        /// - Stores the spawned turret reference on the node.
        /// - Passes the selected turret data (`turretToBuild`) into the turret's script component (`TurretTower`) if available.
        /// </summary>
        public void BuildTurretOn(Node node)
        {
            GameObject turret = Instantiate(turretToBuild.turretModel, node.GetBuildPosition(), Quaternion.identity);
            node.turret = turret;

            TurretTower turretScript = turret.GetComponent<TurretTower>();
            if (turretScript != null)
            {
                turretScript.turretData = turretToBuild;
            }
        }

        /// <summary>
        /// Selects the given Node (with a turret) to display its turret's range visualizer.
        /// - Still working on this parts
        ///             v
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
            //turretToBuild = null;

            //Show selected turret's visualizer
            if (selectedNode != null && selectedNode.turret != null)
            {
                TurretRangeVisualizer newVisualizer = selectedNode.turret.GetComponentInChildren<TurretRangeVisualizer>();
                if (newVisualizer != null)
                {
                    newVisualizer.SetVisualizerVisible(true);
                }
            }
        }
    }
}
