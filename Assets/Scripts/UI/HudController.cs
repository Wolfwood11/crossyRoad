using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private Text textValue;
        // Start is called before the first frame update
        private void Start()
        {
            textValue.text = GameController.Instance.Difficulty.ToString();
        }
    }
}
