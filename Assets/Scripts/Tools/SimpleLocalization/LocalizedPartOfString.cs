using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Assets.SimpleLocalization
{
    public class LocalizedPartOfString : MonoBehaviour
    {
        [SerializeField] string LocalizationKey;

        [SerializeField] string partOfString;
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
            string newString = LocalizationManager.Localize(LocalizationKey);
            var ugui = GetComponent<TextMeshProUGUI>();
            ugui.text = ugui.text.Replace(partOfString, newString);
            partOfString = newString;
        }
    }
}
