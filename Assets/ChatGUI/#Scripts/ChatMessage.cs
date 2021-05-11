using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Innovationscenter.ChatGUI
{
    public class ChatMessage : MonoBehaviour, IChatMessage
    {
        [SerializeField] protected float paddingHieght = 10f;
        //[SerializeField] protected float messageSpaceMy = 30f;
        //[SerializeField] protected float messageSpaceOthers = 60f;

        public ChatManager.IMessage Message
        {
            get
            {
                return message;
            }
            protected set
            {
                message = value;
            }
        }
        private ChatManager.IMessage message;

        public virtual void ShowMessage(ChatManager.IMessage message, RectTransform contentView)
        {

        }

    }
}

