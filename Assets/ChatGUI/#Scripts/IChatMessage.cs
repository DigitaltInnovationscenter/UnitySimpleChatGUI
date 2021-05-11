using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Innovationscenter.ChatGUI
{
    public interface IChatMessage
    {
        void ShowMessage(ChatManager.IMessage message, RectTransform contentView);
    }

}
