using System;
using UnityEngine;
using System.Linq;

namespace Assets.SimpleLocalization
{
	/// <summary>
	/// Asset usage example.
	/// </summary>
	public class LocalizationSource : DropdownSettings
	{
		[SerializeField] Language[] languages = new Language[0];

		/// <summary>
		/// Called on app start.
		/// </summary>
		protected override void Awake()
		{
			LocalizationManager.Read();
			int current = 0;

            for (int i = 0; i < languages.Length; i++)
            {
				if(languages[i].internalLanguage == Application.systemLanguage)
                {
					current = i;
					break;
                }
            }

			LocalizationManager.Language = languages[current].internalLanguage.ToString();
			dropdown.SetItems(languages.Select(x => x.userLanguage).ToArray(), current);
			base.Awake();
		}

        /// <summary>
        /// Change localization at runtime
        /// </summary>
        public void SetLocalization(string localization)
		{
			LocalizationManager.Language = localization;
		}

        protected override void ChangeValue(int value)
        {
			SetLocalization(languages[value].internalLanguage.ToString());
		}
    }
}

[Serializable]
public struct Language
{
	public SystemLanguage internalLanguage;
	public string userLanguage;
}