using Application.Services.ArchiveServices;
using Application.Services.WeatherServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SunTestProject.Models;

namespace SunTestProject.Controllers
{
    public class ArchiveController : Controller
    {
        private readonly IArchiveService _archiveService;
        public ArchiveController(IArchiveService archiveService)
        {
            _archiveService = archiveService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult UploadWeatherArchive()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadWeatherArchive(IFormFileCollection formFiles)
        {
            if (formFiles.Count == 0)
            {
                return View();
            }

            var totalRowCount = 0;

            ResultViewModel viewResult;

            foreach (var item in formFiles)
            {
                var result = await _archiveService.AddWeatherAsync(item);

                if (!result.IsSucceess)
                {
                    viewResult = new ResultViewModel { IsSuccess= result.IsSucceess, Message= item.FileName + ". " + result.Message };

                    return View(viewResult);
                }

                totalRowCount += result.Value;
            }

            viewResult = new ResultViewModel { IsSuccess = true, Message = $"{totalRowCount} строк было добавлено" };

            return View(viewResult);
        }
    }
}
