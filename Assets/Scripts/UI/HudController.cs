using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Text textValue;
       
        private void Update()
        {
            textValue.text = GameController.Instance.Difficulty.ToString();
        }
    }
}
