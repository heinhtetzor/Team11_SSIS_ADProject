﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts
{
    public interface IItemRequisitionService
    {
        void Save(ItemRequisition itemRequisition);
        ItemCategory Get(string id);
        IEnumerable<ItemRequisition> GetAll();
        void Delete(string Id);
    }
}