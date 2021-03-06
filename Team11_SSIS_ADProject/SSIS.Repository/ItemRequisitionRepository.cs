﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.SSIS.Repository
{

    public class ItemRequisitionRepository : Repository<ItemRequisition>, IItemRequisitionRepository
    {
        //custom query to retreive itemrequisition by requisitionid
        public IEnumerable<ItemRequisition> GetAllByRequisitionId(string id)
        {
            IEnumerable<ItemRequisition> irs = _context.ItemRequisitions
                                                .Where(ir => ir.RequisitionId == id)
                                                .ToList();
            return irs;                            
        }

        public IEnumerable<GroupedItemID> groupItemRequisitionsByDateRange(DateTime startDate, DateTime endDate)
        {
            DateTime newEndDate = endDate.AddDays(1).AddTicks(-1);
            var grouped = _context.Requisitions
                            .Include("ItemRequisitions")
                            .Include("Items")
                            .Where(x => x.createdDateTime >= startDate && x.createdDateTime <= newEndDate)
                            .SelectMany(x => x.ItemRequisitions)
                            .GroupBy(x => x.Item)
                            .Select(group => new GroupedItemID
                            {
                                ItemCode = group.Key.ItemNumber,
                                ItemDescription = group.Key.ItemDescription,
                                Quantity = group.Sum(x => x.Quantity)
                            }).ToList();
            return grouped;
        }

        public IEnumerable<GroupedItemID> groupDepartmentItemRequisitionsByDateRange(DateTime startDate, DateTime endDate, string departmentId)
        {
            DateTime newEndDate = endDate.AddDays(1).AddTicks(-1);
            var grouped = _context.Requisitions
                            .Include("ItemRequisitions")
                            .Include("Items")
                            .Where(x => x.DepartmentId == departmentId)
                            .Where(x => x.Status == CustomStatus.Approved)
                            .Where(x => x.createdDateTime >= startDate && x.createdDateTime <= newEndDate)
                            .SelectMany(x => x.ItemRequisitions)
                            .GroupBy(x => x.Item)
                            .Select(group => new GroupedItemID
                            {
                                ItemCode = group.Key.ItemNumber,
                                ItemDescription = group.Key.ItemDescription,
                                Quantity = group.Sum(x => x.Quantity)
                            }).ToList();
            return grouped;
        }

        public IEnumerable<GroupedItemID> ItemRequisitionsThisWeek()
        {
            var date = DateTime.Now.Date.AddDays(-7);
            var result = _context.ItemRequisitions
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

        public IEnumerable<GroupedItemID> ItemRequisitionsTrend(string id)
        {
            var date = DateTime.Now.Date.AddDays(-7);
            var result = _context.ItemRequisitions
                        .Include("Items")
                        .Where(x => x.createdDateTime >= date && x.ItemId == id)
                        .GroupBy(x => x.createdDateTime)
                        .Select(group => new GroupedItemID
                        {
                            Quantity = group.Sum(x => x.Quantity),
                            Date = group.Key
                        })
                        .ToList();
            
            return result;
        }
    }
}