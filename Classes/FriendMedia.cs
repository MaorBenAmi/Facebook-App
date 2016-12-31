using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;

namespace A17_Ex01_Maor_308345354_Nir_032620890
{
    public class FriendMedia
    {
        private UserWrapper m_Friend;
        private List<PostWarpper> m_ListMediaUrl;
        private string m_MediaDescription = string.Empty;

        public FriendMedia(UserWrapper i_User)
        {
            this.Friend = i_User;
            ListMediaUrl = new List<PostWarpper>();
        }

        public string MediaDescription
        {
            get { return m_MediaDescription; }
            set { m_MediaDescription = value; }
        }

        public List<PostWarpper> ListMediaUrl
        {
            get { return m_ListMediaUrl; }
            set { m_ListMediaUrl = value; }
        }

        public UserWrapper Friend
        {
            get { return m_Friend; }
            set { m_Friend = value; }
        }

        public static string ParseLink(string i_VideoLink)
        {
         string newVideoLink=string.Empty;
        newVideoLink=i_VideoLink.Replace("watch?", "");
        newVideoLink = newVideoLink.Replace("=", "/");
        return newVideoLink;
        
        }


    }
}
