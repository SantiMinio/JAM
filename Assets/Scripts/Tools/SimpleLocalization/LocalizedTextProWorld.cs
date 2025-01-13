using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.SimpleLocalization
{
	/// <summary>
	/// Localize text component.
	/// </summary>
    [RequireComponent(typeof(TextMeshPro))]
    public class LocalizedTextProWorld : MonoBehaviour
    {
        public string LocalizationKey;

        public void Start()
        {
            Localize();
            LocalizationManager.LocalizationChanged += Localize;
        }

        public void OnDestroy()
        {
            LocalizationManager.LocalizationChanged -= Localize;
        }

        private void Localize()
        {
            GetComponent<TextMeshPro>().text = LocalizationKey.GetLocalization();
        }

        public void SetNewKey(string newKey)
        {
            LocalizationKey = newKey;
            GetComponent<TextMeshPro>().text = newKey.GetLocalization();
        }
    }
}