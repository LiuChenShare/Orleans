using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Client_WebMVCApp.Models;
using Orleans;
using IGrains;
using Client_WebMVCApp.Services;

namespace Client_WebMVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly OrleansService _orleansService;
        private readonly IClassroom _classroom;

        public HomeController(ILogger<HomeController> logger, OrleansService orleansService)
        {
            _logger = logger;
            _orleansService = orleansService;
            _classroom = _orleansService.GetGrain<IClassroom>(0);
        }

        /// <summary>
        /// 报名拿学号
        /// </summary>
        /// <param name="name">学生姓名</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetStudentId(string name)
        {
            var studentId = await _classroom.Enroll(name);
            IStudent student = _orleansService.GetGrain<IStudent>(studentId);
            _classroom.Seated(student);//落座，不等待它
            //return Json(new { Success = true, Data = studentId, Message = "获取成功！" });
            return new JsonResult(new { Success = true, Data = studentId, Message = "获取成功！" });
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
