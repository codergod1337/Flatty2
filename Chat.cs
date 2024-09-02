using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flatty2
{
    class Chat
    {
        static ConcurrentQueue <string> _chats = new ConcurrentQueue<string>();

        public void AddChatMessage(string chatMessage)
        {
            _chats.Enqueue(chatMessage);
        }
        public string? GetChatMessage()
        {
            // Versuche, den ältesten Eintrag zu entfernen und zurückzugeben
            if (_chats.TryDequeue(out string? oldestMessage))
            {
                return oldestMessage;  // Gibt den ältesten Chat zurück
            }
            return null;  
        }
    }
}
