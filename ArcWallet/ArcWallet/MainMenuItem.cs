using System;
using System.Collections.Generic;
using System.Text;

namespace ArcWallet
{
    /// <summary>
    /// Class used to the nav side bar menu
    /// </summary>
    public class MainMenuItem
    {
        public string Title { get; set; }

        public string Icon { get; set; }

        public Type TargetType { get; set; }
    }
}
