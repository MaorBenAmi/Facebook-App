using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace A17_Ex01_Maor_308345354_Nir_032620890
{
   internal class CheckinWrapper: Fetchable
    {
        private Checkin m_CheckinFB;

        public Checkin CheckinFB
        {
            get { return m_CheckinFB; }
            set { m_CheckinFB = value; }
        }
       public override string ToString()
       {
           if (this.CheckinFB.Message != null)
           {
               return this.CheckinFB.Message;
           }
           else if (this.CheckinFB.Caption != null)
           {
               return this.CheckinFB.Caption;
           }
           else
           {
               return string.Format("{0}", this.CheckinFB.Type);
           }
       }
    }
}
