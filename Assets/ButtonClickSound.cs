using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    void OnEnable() {
        Button _button = GetComponent<Button>();
        _button.onClick.AddListener(MusicManager.Instance.OnButtonClick);
    }
}
