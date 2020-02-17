using System;
using System.Collections.Generic;
using System.Text;

namespace Late2Meet.Models
{
    public enum MenuItemType
    {
        Leaderboard,
        Analysis,
        Settings,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
