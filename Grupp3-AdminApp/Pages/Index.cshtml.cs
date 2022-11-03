﻿using Grupp3_Elevator.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Grupp3_Elevator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;


        public IndexModel(ApplicationDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        public string ErrandsAmount { get; set; }
        public string ElevatorsAmount { get; set; }
        public string TechnichansAmount { get; set; }
        public string CommentsAmount { get; set; }

        public void OnGet()
        {
            ErrandsAmount = _context.Errands.Select(a => a.Id).Count().ToString();
            ElevatorsAmount = _context.Elevators.Select(a => a.Id).Count().ToString();

            TechnichansAmount = _context.Technicians.Select(a => a.Id).Count().ToString();
            CommentsAmount = _context.ErrandComments.Select(a => a).Count().ToString();
        }
    }
}