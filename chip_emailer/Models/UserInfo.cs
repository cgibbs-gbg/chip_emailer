using System.Linq;

namespace ChipEmailer.Models
{
    public class UserInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string[] Groups { get; set; }

        public bool IsMemberOf(string groupName)
        {
            return Groups.Contains(groupName);
        }

        public bool IsService
        {
            get
            {
                return IsMemberOf("Services");
            }
        }

        public bool IsAdmin
        {
            get
            {
                return IsMemberOf("All.Administrators") || IsMemberOf("Ragnarok.Administrators");
            }
        }

        public bool IsAuthorizedUser
        {
            get
            {
                return IsAdmin || IsMemberOf("Ragnarok.Users");
            }
        }


    }
}
