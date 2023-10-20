using MALLikeSite.Models;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using FluentValidation;
using BusinessLogic;
using Models;
using Humanizer.Localisation;
using System.Xml.Linq;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

public class AdminController : Controller
{
    private readonly ITitleService _titleService;
    private readonly ICharacterService _characterService;
    private readonly IStaffService _staffService;

    public AdminController(ITitleService titleService, ICharacterService characterService, IStaffService staffService)
    {
        _titleService = titleService;
        _characterService = characterService;
        _staffService = staffService;
    }

    public IActionResult ApproveTitles()
    {
        var unapprovedTitles = _titleService.GetUnapproved().AsEnumerable();
        return View(unapprovedTitles);
    }

    [HttpPost]
    public IActionResult ApproveTitle(Guid titleId)
    {
        _titleService.Approve(titleId);
        return RedirectToAction("ApproveTitles");
    }

    public IActionResult ApproveCharacters()
    {
        var unapprovedCharacters = _characterService.GetUnapproved().AsEnumerable();
        return View(unapprovedCharacters);
    }

    [HttpPost]
    public IActionResult ApproveCharacter(Guid characterId)
    {
        _characterService.Approve(characterId);
        return RedirectToAction("ApproveCharacters");
    }
    public IActionResult ApproveStaffs()
    {
        var unapprovedStaffs = _staffService.GetUnapproved().AsEnumerable();
        return View(unapprovedStaffs);
    }

    [HttpPost]
    public IActionResult ApproveStaff(Guid staffId)
    {
        _staffService.Approve(staffId);
        return RedirectToAction("ApproveStaffs");
    }
}