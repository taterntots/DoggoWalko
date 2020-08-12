using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject PressEnter_Text;
    public bool textBlink = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("flashingText");
        Timer.timer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Runner");
        }
    }

    // Coroutine function that flashes "Press Enter" text on the main menu
    public IEnumerator flashingText()
    {
        // While loop that keeps the text flashing as long as the textBlink bool is True (which it always remains)
        while (textBlink)
        {
            PressEnter_Text.SetActive(false);
            yield return new WaitForSeconds(.5f);
            PressEnter_Text.SetActive(true);
            yield return new WaitForSeconds(.5f);
        }
    }
}
