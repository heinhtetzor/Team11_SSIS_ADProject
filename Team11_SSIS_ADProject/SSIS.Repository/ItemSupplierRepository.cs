﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Contracts.Repositories;
using Team11_SSIS_ADProject.SSIS.Models;

namespace Team11_SSIS_ADProject.SSIS.Repository
{
    public class ItemSupplierRepository : Repository<ItemSupplier>, IItemSupplierRepository
    {
        public double GetItemPriceBySupplierIdAndItemId(string itemId, string supplierId)
        {
            return _context.ItemSuppliers
                        .Where(x => x.ItemId == itemId && x.SupplierId == supplierId)
                        .Select(x => x.Price)
                        .SingleOrDefault();
        }
    }
}