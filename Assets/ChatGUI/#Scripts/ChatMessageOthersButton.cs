using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Innovationscenter.ChatGUI
{
    public class ChatMessageOthersButton : ChatMessage
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI textMessageSender;
        [SerializeField] private float messageSpace = 60f;

        public override void ShowMessage(ChatManager.IMessage newMessage, RectTransform contentView)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();

            ChatManager.MessageOthersButton messageOthersButton = (ChatManager.MessageOthersButton)newMessage;
            Message = messageOthersButton;

            float messageHeight = rectTransform.sizeDelta.y;

            textMessageSender.text = messageOthersButton.Sender;
            textMessageSender.ForceMeshUpdate();

            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = messageOthersButton.ButtonText;
            buttonText.ForceMeshUpdate();

            //messageHeight += buttonText.textBounds.size.y;
            //messageHeight += paddingHieght;

            //Vector2 size = rectTransform.sizeDelta;
            //size.y = messageHeight;
            //rectTransform.sizeDelta = size;

            Vector2 contentSize = contentView.sizeDelta;
            contentSize.y += messageHeight + messageSpace;
            contentView.sizeDelta = contentSize;

            Vector2 messagePos = new Vector2();
            messagePos.y = -contentView.sizeDelta.y + (messageHeight / 2f);
            messagePos.x = rectTransform.sizeDelta.x / 2f;

            rectTransform.anchoredPosition = messagePos;
        }

        public void OnButtonClick()
        {
            ChatManager.Instance.messageOthersButtonInteractEvent.Invoke((ChatManager.MessageOthersButton)Message);
        }
    }
}

