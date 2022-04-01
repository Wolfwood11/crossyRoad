using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Base
{
    public class BasePopupController : MonoBehaviour
    {
        [SerializeField] private Button button;

        protected virtual void OnButtonClick()
        {
            SceneManager.LoadScene(0);
        }
        protected virtual void Awake()
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }
}
