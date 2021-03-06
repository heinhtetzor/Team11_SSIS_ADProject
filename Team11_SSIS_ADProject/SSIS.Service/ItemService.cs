﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team11_SSIS_ADProject.SSIS.Contracts;
using Team11_SSIS_ADProject.SSIS.Models;
using Team11_SSIS_ADProject.SSIS.ViewModels;

namespace Team11_SSIS_ADProject.SSIS.Service
{
    public class ItemService : IItemService
    {
        IItemRepository itemContext;
        MLService mLService;

        public ItemService(IItemRepository itemRepository,MLService mLService)
        {
            this.itemContext = itemRepository;
            this.mLService = mLService;
        }

        public void Delete(string id)
        {
            var item = itemContext.Get(id);
            itemContext.Remove(item);
            itemContext.Commit();
        }

        public Item Get(string id)
        {
            return itemContext.Get(id);
        }

        public IEnumerable<Item> GetAll()
        {
            return itemContext.GetAll();
        }

        public void Save(Item item)
        {
            Item i = itemContext.Get(item.Id);
            if(i == null)
            {
                itemContext.Add(item);
            }
            else
            {
                i.ItemNumber = item.ItemNumber;
                i.ItemDescription = item.ItemDescription;
                i.ItemReorderLevel = item.ItemReorderLevel;
                i.ItemReorderQty = item.ItemReorderQty;
                i.ItemUnit = item.ItemUnit;
                i.ItemCategoryId = item.ItemCategoryId;
                i.ImagePath = item.ImagePath;
            }
            itemContext.Commit();
        }
        public IEnumerable<ItemPurchaseOrderViewModel> GetItemsLowerThanReorderLevel()
        {
            var dic = mLService.Pred_ROL(5);
            var list = itemContext.GetItemsLowerThanReorderLevel();

            foreach (var item in list)
            {
                item.PredictedReorderQty = dic[item.ItemId];
            }

            return list;
        }

        public IEnumerable<SelectListItem> GetItemList()
        {
            List<SelectListItem> items = itemContext.GetAll()
                                              .OrderBy(i => i.ItemDescription)
                                              .Select(i =>
                                                new SelectListItem
                                                {
                                                    Value = i.Id,
                                                    Text = i.ItemDescription
                                                }).ToList();
            var placeholder = new SelectListItem()
            {
                Value = null,
                Text = "--Select Item--"
            };
            items.Insert(0, placeholder);
            return new SelectList(items, "Value", "Text");

        }
    }
}