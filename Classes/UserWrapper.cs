using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;

namespace A17_Ex01_Maor_308345354_Nir_032620890
{
    public class UserWrapper : Fetchable
    {
        private User m_UserFB;

        public UserWrapper(User i_User)
        {
            UserFB = i_User;
        }

        public User UserFB
        {
            get { return m_UserFB; }
            set { m_UserFB = value; }
        }

        public override string ToString()
        {

            return this.UserFB.Name;
        }
    }
}
