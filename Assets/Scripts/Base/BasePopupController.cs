using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Base
{
    public class BasePopupController : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Button playInf;
        protected virtual void OnButtonClick()
        {
            GameController.Instance.InfinityMode = false;
            GameController.Instance.GenerateNewWorld(true);
            Destroy(gameObject);
        }
        
        protected virtual void OnInfClick()
        {
            GameController.Instance.InfinityMode = true;
            GameController.Instance.GenerateNewWorld(true);
            Destroy(gameObject);
        }
        
        protected virtual void Awake()
        {
            button.onClick.AddListener(OnButtonClick);
            playInf.onClick.AddListener(OnInfClick);
        }
    }
}
