using System;
using System.Collections;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using Facebook;
using System.Windows.Forms;
using System.Collections.Generic;

namespace A17_Ex01_Maor_308345354_Nir_032620890
{
    class LogicManagment
    {
        private UserWrapper m_LoggedInUser;
        private FilterCheckins m_FilterCheckins;
        private Fetcher m_Fetcher;
        private List<Fetchable> m_FetchesObjects;
        private List<FriendMedia> m_FriendsSongsList;

        internal List<FriendMedia> FriendsSongsList
        {
            get { return m_FriendsSongsList; }
            set { m_FriendsSongsList = value; }
        }


        public LogicManagment()
        {
            Fetcher = new Fetcher();
            FetchesObjects = new List<Fetchable>();
        }
        public FilterCheckins FilterCheckins
        {
            get { return m_FilterCheckins; }
            set { m_FilterCheckins = value; }
        }


        public Fetcher Fetcher
        {
            get { return m_Fetcher; }
            set { m_Fetcher = value; }
        }


        public List<Fetchable> FetchesObjects
        {
            get { return m_FetchesObjects; }
            set { m_FetchesObjects = value; }
        }
        public UserWrapper LoggedInUser
        {
            get { return m_LoggedInUser; }
            set { m_LoggedInUser = value; }
        }

        public bool userLogin()
        {
            try
            {
                LoginResult result = FacebookService.Login("330018737384008",
                             "public_profile",
                             "user_education_history",
                             "user_birthday",
                             "user_actions.video",
                             "user_actions.news",
                             "user_actions.music",
                             "user_actions.fitness",
                             "user_actions.books",
                             "user_about_me",
                             "user_friends",
                             "publish_actions",
                             "user_events",
                             "user_games_activity",
                             "user_hometown",
                             "user_likes",
                             "user_location",
                             "user_managed_groups",
                             "user_photos",
                             "user_posts",
                             "user_relationships",
                             "user_relationship_details",
                             "user_religion_politics",
                             "user_tagged_places",
                             "user_videos",
                             "user_website",
                             "user_work_history",
                             "read_custom_friendlists",
                             "read_page_mailboxes",
                             "manage_pages",
                             "publish_pages",
                             "publish_actions",
                             "rsvp_event"
                             );
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            /*
            if (!string.IsNullOrEmpty(result.AccessToken))
            {
                m_LoggedInUser = new UserWrapper(result.LoggedInUser);
                return true;
            }
            else
            {
                return false;
            }*/
            return true;

        }

        public void filterOrShowAll(string i_DayOfTheWeek, string i_Keyword,bool i_ShowAll)
        {
           
           m_FilterCheckins = new FilterCheckins(i_Keyword, i_DayOfTheWeek, LoggedInUser.UserFB.Friends);
           if (i_ShowAll == false)
           {
               m_FilterCheckins.Filter();
           }
           else
           {
               m_FilterCheckins.ShowAll();
           }
            
            }

        public void fetchInformationFromUser(Type i_TypeToFetch)
        {
            FetchesObjects.Clear();
            FetchesObjects = Fetcher.FetchInformation(LoggedInUser, i_TypeToFetch);
        }
        public void fetchInformationFromPost(PostWarpper i_PostToFetch,Type i_TypeToFetch)
        {
            FetchesObjects.Clear();
            FetchesObjects = Fetcher.FetchInformation(i_PostToFetch, i_TypeToFetch);
        }
        public void fetchInformationFromCheckin(CheckinWrapper i_SelectedCheckin, Type i_TypeToFetch)
        {
            FetchesObjects.Clear();
            FetchesObjects = Fetcher.FetchInformation(i_SelectedCheckin, i_TypeToFetch);
        }

        public void fetchSongs()
        {
            FriendsSongsList=Fetcher.FetchFriendsMedias(LoggedInUser);

        }
    }
 }

