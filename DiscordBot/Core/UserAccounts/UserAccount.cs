using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Core.UserAccounts
{
    public class UserAccount
    {
        //uint - unsigned int doesnt go below 0
        public ulong ID { get; set; }
        
        public uint Points { get; set; }

        public uint XP { get; set; }
    }
}
