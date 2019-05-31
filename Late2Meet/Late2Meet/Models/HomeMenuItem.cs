using System;
using System.Collections.Generic;
using System.Text;

namespace Late2Meet.Models
{
    public enum MenuItemType
    {
        Leaderboard,
        Settings,
        Browse,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
