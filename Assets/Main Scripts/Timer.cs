
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class Timer : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image img;

    [SerializeField] private Gradient grad;
    
    // Start is called before the first frame update
    void Start()
    {
        img = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        var percentage = (SceneBehaviour.Instance.currentGametimer- Time.timeSinceLevelLoad) / SceneBehaviour.Instance.currentGametimer;
        img.fillAmount = percentage;
        img.color =grad.Evaluate(percentage);
        Debug.Log(percentage);
    }
}
