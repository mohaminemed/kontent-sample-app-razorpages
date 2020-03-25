﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kentico.Kontent.Delivery;
using Kentico.Kontent.Delivery.Abstractions;
using kontent_sample_app_razorpages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kontent_sample_app_razorpages.Pages.Cafes
{
    public class IndexModel : PageModel
    {
        private readonly IDeliveryClient _deliveryClient;

        public DeliveryItemResponse<Models.Home> Home { get; set; }

        public List<Cafe> CompanyCafes { get; set; }

        public List<Cafe> PartnerCafes { get; set; }

        public IndexModel(IDeliveryClient deliveryClient)
        {
            _deliveryClient = deliveryClient;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _deliveryClient.GetItemsAsync<Cafe>(new OrderParameter("system.name"));
            var cafes = response.Items;

            CompanyCafes = cafes.Where(c => c.Country == "USA").ToList();
            PartnerCafes = cafes.Where(c => c.Country != "USA").ToList();

            return Page();
        }
    }
}