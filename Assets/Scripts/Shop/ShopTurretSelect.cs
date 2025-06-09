using UnityEngine;
using UnityEngine.EventSystems;

namespace NF.TD.TurretShop 
{
    public class ShopTurretSelect : MonoBehaviour, IPointerClickHandler
    {
        public GameObject turretInfo;

        //Only one turret info should be shown at a time
        public static GameObject currentlySelected;

        public void OnPointerClick(PointerEventData eventData)
        {
            // Hide previously selected turret info
            if (currentlySelected != null && currentlySelected != turretInfo)
            {
                currentlySelected.SetActive(false);
            }

            // Toggle the current turret info
            bool isActive = turretInfo.activeSelf;
            turretInfo.SetActive(!isActive);

            currentlySelected = turretInfo.activeSelf ? turretInfo : null;
        }

        public static void Deselect()
        {
            if (currentlySelected != null)
            {
                currentlySelected.SetActive(false);
                currentlySelected = null;
            }
        }
    }
}
