using NF.TD.BaseTurret;
using NF.TD.BuildCore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NF.TD.TurretShop 
{
    public class TurretShop : MonoBehaviour
    {
        [Header("Turrets")]
        public TurretScriptable[] turretOptions;

        [Header("UI")]
        public Transform buttonParent;
        public GameObject turretButtonPrefab;
        public Image selectedTurretImageUI;

        BuildManager buildManager;

        private void Start()
        {
            buildManager = BuildManager.instance;

            foreach (TurretScriptable turret in turretOptions)
            {
                CreateTurretButton(turret);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1)) // Right click
            {
                buildManager.DeselectTurret();
                selectedTurretImageUI.sprite = null;
                Debug.Log("Turret deselected.");
            }
        }

        private void CreateTurretButton(TurretScriptable turret)
        {
            GameObject btnGO = Instantiate(turretButtonPrefab, buttonParent);
            Button button = btnGO.GetComponent<Button>();

            //Get UI elements from children
            Image icon = btnGO.transform.Find("Icon").GetComponent<Image>();
            Transform turretInfo = btnGO.transform.Find("TurretInfo");
            TextMeshProUGUI nameText = turretInfo.Find("NameText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI costText = turretInfo.Find("CostText").GetComponent<TextMeshProUGUI>();

            //Set UI values
            icon.sprite = turret.turretIcon;
            nameText.text = turret.TurretName;
            costText.text = turret.turretCost.ToString();

            //Add click functionality
            button.onClick.AddListener(() =>
            {
                buildManager.SelectTurretToBuild(turret);
                selectedTurretImageUI.sprite = turret.turretIcon;
                Debug.Log($"Selected turret: {turret.TurretName}");
            });

            ShopTurretHover hoverScript = btnGO.AddComponent<ShopTurretHover>();
            hoverScript.turretInfo = turretInfo.gameObject;
            turretInfo.gameObject.SetActive(false);
        }
    }
}
