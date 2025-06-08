using UnityEngine;
using UnityEngine.EventSystems;

namespace NF.TD.TurretShop 
{
    public class ShopTurretHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject turretInfo;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (turretInfo != null)
                turretInfo.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (turretInfo != null)
                turretInfo.SetActive(false);
        }
    }
}
