using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Innovationscenter.ChatGUI;


public class TestChat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ChatManager.Instance.messageOthersButtonInteractEvent.AddListener(OnMessageInteractEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonTest1()
    {
        /*
        Innovationscenter.ChatGUI.ChatManager.Instance.NewMessage(new Innovationscenter.ChatGUI.ChatManager.MessageMyText
        {
            Text = "qeqweqweqwe"
        });
        
        Innovationscenter.ChatGUI.ChatManager.Instance.NewMessage(new Innovationscenter.ChatGUI.ChatManager.MessageOthersText
        {
            Text = "qeqweqweqwe",
            Sender ="Sender!!!!"
        });
        
        */
        
        ChatManager.Instance.NewMessage(new ChatManager.MessageOthersButton
        {
            ButtonText = "Button 123",
            Sender = "Sender!!!!"
        });
        
    }

    private void OnMessageInteractEvent(ChatManager.MessageOthersButton messageOthersButton)
    {
        ChatManager.Instance.NewMessage(new ChatManager.MessageOthersText
        {
            Sender = "Sender!!!",
            Text = "You pressed the nice little button labeled with the text " + messageOthersButton.ButtonText
        });
    }
}
