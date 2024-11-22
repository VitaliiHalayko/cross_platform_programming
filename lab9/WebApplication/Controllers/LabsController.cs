namespace WebApplication.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebApplication.Models;
using System.ComponentModel.DataAnnotations;
using LabLibrary;

[Authorize]
public class LabsController : Controller
{
    [HttpGet]
    public IActionResult Lab1() => View();
    
    [HttpPost]
    public IActionResult RunLab1(string inputData)
    {
        var result = new LabRunner().RunLab1(inputData);

        ViewBag.Result = result;

        return View("Lab1");
    }
    
    [HttpGet]
    public IActionResult Lab2() => View();
    
    [HttpPost]
    public IActionResult RunLab2(string inputData)
    {
        var result = new LabRunner().RunLab2(inputData);

        ViewBag.Result = result;

        return View("Lab2");
    }
    
    [HttpGet]
    public IActionResult Lab3() => View();
    
    [HttpPost]
    public IActionResult RunLab3(string inputData)
    {
        var result = new LabRunner().RunLab3(inputData);

        ViewBag.Result = result;

        return View("Lab3");
    }
}