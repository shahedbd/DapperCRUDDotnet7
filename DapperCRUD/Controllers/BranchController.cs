using DapperCRUD.Models;
using DapperCRUD.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DapperCRUD.Controllers
{
    public class BranchController : Controller
    {
        private IBranchRepository _iBranchRepository;
        public BranchController(IBranchRepository iBranchRepository)
        {
            _iBranchRepository = iBranchRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _iBranchRepository.GetAllAsync();
            return View(result);
        }
        public async Task<IActionResult> Details(Int64 id)
        {
            var result = await _iBranchRepository.GetByIdAsync(id);
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                branch.CreatedDate = DateTime.Now;
                branch.UpdatedDate = DateTime.Now;
                await _iBranchRepository.Create(branch);
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }
        public async Task<IActionResult> Edit(Int64 id)
        {
            var _Branch = await _iBranchRepository.GetByIdAsync(id);
            return View(_Branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Branch branch)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    branch.Id = id;
                    branch.UpdatedDate = DateTime.Now;
                    await _iBranchRepository.Update(branch);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }
        public async Task<IActionResult> Delete(Int64 id)
        {
            var _Branch = await _iBranchRepository.GetByIdAsync(id);
            return View(_Branch);
        }

        [HttpDelete, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(Int64 id)
        {
            await _iBranchRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}