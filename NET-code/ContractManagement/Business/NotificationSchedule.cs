//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  NotificationSchedule.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
//  notification related to the frequency

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractManagement.Business
{
    public class NotificationSchedule
    {
        private int _freqNotifyId;
        private int _frequencyId;
        private int _notifyScheduleId;


        public int FreqNotifyId
        {
            get { return _freqNotifyId; }
            set { _freqNotifyId = value; }
        }

        public int FrequencyId
        {
            get { return _frequencyId; }
            set { _frequencyId = value; }

        }

        public int NotifyScheduleId
        {
            get { return _notifyScheduleId; }
            set { _notifyScheduleId = value; }
        }
    }
}