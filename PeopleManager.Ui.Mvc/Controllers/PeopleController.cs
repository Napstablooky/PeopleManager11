using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Ui.Mvc.Core;
using PeopleManager.Ui.Mvc.Models;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PeopleManagerDbContext _peopleManagerDbContext;

        public PeopleController(PeopleManagerDbContext peopleManagerDbContext)
        {
            _peopleManagerDbContext = peopleManagerDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var people = _peopleManagerDbContext.People
                .Include(p => p.Organization)
                .ToList();
            return View(people);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var organizations = _peopleManagerDbContext.Organizations.ToList();

            ViewBag.Organizations = organizations;
            //ViewData["Organizations"] = organizations;
            //TempData["Organizations"] = organizations;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                var organizations = _peopleManagerDbContext.Organizations.ToList();
                ViewBag.Organizations = organizations;
                return View(person);
            }

            _peopleManagerDbContext.People.Add(person);
            _peopleManagerDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var person = _peopleManagerDbContext.People
                .FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                return RedirectToAction("Index");
            }

            var organizations = _peopleManagerDbContext.Organizations.ToList();

            ViewBag.Organizations = organizations;

            return View(person);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id, [FromForm]Person person)
        {
            if (!ModelState.IsValid)
            {
                var organizations = _peopleManagerDbContext.Organizations.ToList();
                ViewBag.Organizations = organizations;
                return View(person);
            }

            var dbPerson = _peopleManagerDbContext.People
                .FirstOrDefault(p => p.Id == id);

            if (dbPerson == null)
            {
                return RedirectToAction("Index");
            }

            //Mapping
            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;
            dbPerson.Email = person.Email;
            dbPerson.OrganizationId = person.OrganizationId;

            _peopleManagerDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var person = _peopleManagerDbContext.People
                .FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                return RedirectToAction("Index");
            }

            return View(person);
        }

        [HttpPost("[controller]/Delete/{id:int?}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var dbPerson = _peopleManagerDbContext.People
                .FirstOrDefault(p => p.Id == id);

            if (dbPerson == null)
            {
                return RedirectToAction("Index");
            }

            _peopleManagerDbContext.People.Remove(dbPerson);

            _peopleManagerDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
