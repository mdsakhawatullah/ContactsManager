﻿using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.DTO.Enums;

namespace ContactsManager.Controllers
{
    public class PersonsController : Controller
    {
        private readonly IPersonsService _personsService;

        public PersonsController(IPersonsService personService)
        {
            _personsService = personService;
        }


        [Route("persons/index")]
        [Route("/")]
        public IActionResult Index(string searchBy, string? searchString,
                                   string sortBy = nameof(PersonResponse.PersonName),
                                   SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(PersonResponse.PersonName), "Person Name" },
                { nameof(PersonResponse.Email), "Email" },
                { nameof(PersonResponse.DateOfBirth), "Date of Birth" },
                { nameof(PersonResponse.Gender), "Gender" },
                { nameof(PersonResponse.CountryID), "Country" },
                { nameof(PersonResponse.Address), "Address" }

            };
            List<PersonResponse> persons = _personsService.GetFilteredPersons(searchBy, searchString);

            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            return View(persons); //Views/Persons/Index.cshtml
        }

        [Route("persons/create")]
        [HttpGet]

        public IActionResult Create()
        {

            return View();
        }
    }
}
