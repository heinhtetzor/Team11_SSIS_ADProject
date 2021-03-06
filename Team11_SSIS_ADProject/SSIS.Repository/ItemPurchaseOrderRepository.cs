﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts.Repositories;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.SSIS.Repository
{

    public class ItemPurchaseOrderRepository : Repository<ItemPurchaseOrder>, IItemPurchaseOrderRepository
    {
        public IEnumerable<GroupedItemID> groupItemPurchaseOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            DateTime newEndDate = endDate.AddDays(1).AddTicks(-1);

            var grouped = _context.ItemPurchaseOrders
                            .Include("Items")
                            .Where(x => x.createdDateTime >= startDate && x.createdDateTime <= newEndDate && x.PurchaseOrder.Status == 9)
                            .GroupBy(x => x.Item)
                            .Select(group => new GroupedItemID
                            {
                                ItemCode = group.Key.ItemNumber,
                                ItemDescription = group.Key.ItemDescription,
                                Quantity = group.Sum(x => x.Quantity)
                            }).ToList();
   

            return grouped;
        }

        public IEnumerable<GroupedItemID> ItemPurchaseOrdersThisWeek()
        {
            var date = DateTime.Now.Date.AddDays(-7);
            var result = _context.ItemPurchaseOrders
                        .Include("Items")
                        .Where(x => x.createdDateTime >= date)
                        .GroupBy(x => x.Item)
                        .Select(group => new GroupedItemID
                        {
                            ItemDescription = group.Key.ItemDescription,
                            Quantity = group.Sum(x => x.Quantity)
                        }).ToList();
            return result;
        }
    }
}