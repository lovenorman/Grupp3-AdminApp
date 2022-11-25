using Grupp3_Elevator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Services.Errand;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Technician;
using Grupp3_AdminApp.Services.ErrandComment;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace Grupp3_Elevator.Pages.Errand
{
    public class ErrandDetailsModel : PageModel
    {
        private readonly IErrandService _errandService;
        private readonly IElevatorService _elevatorService;
        private readonly IErrandCommentService _errandCommentService;
        private readonly ITechnicianService _technicianService;

        public ErrandDetailsModel(IErrandService errandService, IElevatorService elevatorService, IErrandCommentService errandCommentService, ITechnicianService technicianService)
        {
            _errandService = errandService;
            _elevatorService = elevatorService;
            _errandCommentService = errandCommentService;
            _technicianService = technicianService;
        }

        [BindProperty]
        public ElevatorDeviceItem Elevator { get; set; }
        public ErrandModel Errand { get; set; }

        public List<SelectListItem> SelectTechnician { get; set; }
        [BindProperty]
        public Guid ChosenSelectTechnician { get; set; }

        [BindProperty]
        public string Content { get; set; }

        public async Task<IActionResult> OnGetAsync(string elevatorId, string errandId)
        {
            Elevator = await _elevatorService.GetElevatorDeviceByIdAsync(elevatorId);
            Errand = await _errandService.GetErrandByIdAsync(errandId);

            SelectTechnician = await _technicianService.SelectTechnician();

            if (Errand == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string elevatorId, string errandId)
        {
            Errand = await _errandService.GetErrandByIdAsync(errandId);

            if (ModelState.IsValid)
            {
                await _errandCommentService.CreateErrandCommentAsync(Errand, ChosenSelectTechnician.ToString(), Content);

                return RedirectToPage("ErrandDetails", new { elevatorId, errandId });
            }
            SelectTechnician = await _technicianService.SelectTechnician();
            return Page();
        }
    }
}