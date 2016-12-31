using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace A17_Ex01_Maor_308345354_Nir_032620890
{
    class FilterCheckins
    {
        private string m_KeywordFilter;
        private string m_DayFilter;
        private List<CheckinWrapper> m_ListOfFilteredCheckins;
        private List<User> m_ListOfFriends;

        public FilterCheckins(string i_KeywordFilter, string i_DayFilter, ICollection<User> i_FirendsList)
        {
            KeywordFilter = i_KeywordFilter;
            DayFilter = i_DayFilter;
            ListOfFilteredCheckins = new List<CheckinWrapper>();
            ListOfFreinds = i_FirendsList.ToList();
        }

        public string DayFilter
        {
            get { return m_DayFilter; }
            set { m_DayFilter = value; }
        }


        public List<User> ListOfFreinds
        {
            get { return m_ListOfFriends; }
            set { m_ListOfFriends = value; }
        }

        public string KeywordFilter
        {
            get { return m_KeywordFilter; }
            set { m_KeywordFilter = value; }
        }
        public List<CheckinWrapper> ListOfFilteredCheckins
        {
            get { return m_ListOfFilteredCheckins; }
            set { m_ListOfFilteredCheckins = value; }
        }


        public void Filter()
        {
            CheckinWrapper newCheckinToAdd;
            foreach (User friend in m_ListOfFriends)
            {
                foreach (Checkin friendCheckin in friend.Checkins)
                {
                    newCheckinToAdd = new CheckinWrapper();
                    newCheckinToAdd.CheckinFB = friendCheckin;

                    string checkinDay = friendCheckin.CreatedTime.Value.DayOfWeek.ToString();
                    if (m_KeywordFilter != string.Empty && m_DayFilter != string.Empty)
                    {
                        if (friendCheckin.Place.Name.Contains(m_KeywordFilter) && m_DayFilter.Equals(checkinDay))
                        {
                            m_ListOfFilteredCheckins.Add(newCheckinToAdd);
                        }
                    }
                    else if (m_KeywordFilter != string.Empty && (friendCheckin.Place.Name.Contains(m_KeywordFilter)))
                    {
                        m_ListOfFilteredCheckins.Add(newCheckinToAdd);
                    }
                    else if (m_KeywordFilter != string.Empty && (friendCheckin.Place.Location.City != null))
                    {
                        if ((friendCheckin.Place.Location.City.Contains(m_KeywordFilter)))
                        {
                            m_ListOfFilteredCheckins.Add(newCheckinToAdd);
                        }
                        }
                    else if (m_DayFilter.Equals(checkinDay))
                    {
                        m_ListOfFilteredCheckins.Add(newCheckinToAdd);
                    }
                
                }
            }
 
        }

        public void ShowAll()
        {
            CheckinWrapper newCheckinToAdd;
            foreach (User friend in m_ListOfFriends)
            {
                foreach (Checkin friendCheckin in friend.Checkins)
                {
                    newCheckinToAdd = new CheckinWrapper();
                    newCheckinToAdd.CheckinFB = friendCheckin;
                    m_ListOfFilteredCheckins.Add(newCheckinToAdd);
                }
            }
        }
    }
}
