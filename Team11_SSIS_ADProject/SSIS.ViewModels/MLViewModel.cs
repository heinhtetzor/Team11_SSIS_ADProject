﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.ViewModels
{
    public class MLViewModel
    {
        public Dictionary<String,double> Items_ROL { get; set; }

        public Dictionary<String, float> Pred_DailyUsage { get; set; }
        //public List<double> Pred_ROL { get; set; }
        //public List<double> Pred_Qty { get; set; }
    }

    public class MlViewModel_2
    {
        public List<String> Items;
        public List<float> usage;
    }
}