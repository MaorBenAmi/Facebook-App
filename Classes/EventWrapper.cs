using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FacebookWrapper.ObjectModel;

namespace A17_Ex01_Maor_308345354_Nir_032620890
{
    class EventWrapper:Fetchable
    {
        private Event m_EventFB;

        public EventWrapper(Event i_Event)
        {
            EventFB = i_Event;
        }

        public Event EventFB
        {
            get { return m_EventFB; }
            set { m_EventFB = value; }
        }
        public override string ToString()
        {
            return this.EventFB.Name;
        }
    }
}
