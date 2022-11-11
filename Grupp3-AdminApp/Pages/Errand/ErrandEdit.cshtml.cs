using Grupp3_AdminApp.Services.ErrandComment;
using Grupp3_Elevator.Data;
using Grupp3_Elevator.Models;
using Grupp3_Elevator.Services;
using Grupp3_Elevator.Services.Errand;
using Grupp3_Elevator.Services.Technician;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grupp3_Elevator.Pages.Errand
{
    public class ErrandEditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IElevatorService _elevatorService;
        private readonly IErrandService _errandService;
        private readonly IErrandCommentService _errandCommentService;

        public ErrandEditModel(ApplicationDbContext context, IElevatorService elevatorService, IErrandService errandService, IErrandCommentService errandCommentService)
        {
            _context = context;
            _elevatorService = elevatorService;
            _errandService = errandService;
            _errandCommentService = errandCommentService;
        }

        [BindProperty]
        public ErrandModel Errand { get; set; }
        [BindProperty]
        public List<SelectListItem> SelectTechnicianEdit { get; set; }
        [BindProperty]
        public TechnicianModel Technician { get; set; }
        //public Guid TechnicianId { get; set; }
        public ElevatorDeviceItem Elevator { get; set; }
        //[BindProperty]
        //public List<ErrandCommentModel> Comments { get; set; }


        public async Task<IActionResult> OnGetAsync(string elevatorId, string? errandId)
        {
            Elevator = await _elevatorService.GetElevatorDeviceByIdAsync(elevatorId);
            Errand = _errandService.GetErrandById(errandId);

            SelectTechnicianEdit = _errandService.SelectTechnicianEdit(Errand.Technician.Id.ToString());

            if (Errand == null)
            {
                return NotFound();
            }

            return Page();
        }

        // TO DO: ModelState
        public async Task<IActionResult> OnPost(string errandId)
        {
            Errand = _errandService.GetErrandById(errandId);

            //Comments = _errandCommentService.GetErrandCommentsFromErrandId(errandId).ToList();

            //try
            //{
            //    var editedErrandId = await _errandService.EditErrandAsync(errandId, Errand, TechnicianId.ToString(), Comments);
            //    return RedirectToPage("ErrandDetails", new { errandId });
            //}
            //catch
            //{
            //    return Page();
            //}

            if (ModelState.IsValid)
            {
                var editedErrandId = await _errandService.EditErrandAsync(errandId, Errand, Errand.Technician.Id.ToString(), Errand.Comments);

                return RedirectToPage("ErrandDetails", new { errandId });
            }

            return Page();

        }
    }
}
