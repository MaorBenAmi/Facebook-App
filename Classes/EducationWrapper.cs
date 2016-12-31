using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;

namespace A17_Ex01_Maor_308345354_Nir_032620890
{
    class EducationWrapper:Fetchable
    {
        private Education m_EducationFB;


        public EducationWrapper(Education i_Education)
        {
            EducationFB = i_Education;
        }

        public Education EducationFB
        {
            get { return m_EducationFB; }
            set { m_EducationFB = value; }
        }

        public override string ToString()
        {
           return this.EducationFB.School.Name;
        }
    }
}
