﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Contracts.Services
{
    public interface IItemPurchaseOrderService
    {
        void Save(ItemPurchaseOrder itemPurchaseOrder);
        ItemPurchaseOrder Get(string id);
        IEnumerable<ItemPurchaseOrder> GetAll();
        void Delete(string Id);
    }
}