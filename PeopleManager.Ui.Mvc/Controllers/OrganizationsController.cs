using Microsoft.AspNetCore.Mvc;
using PeopleManager.Ui.Mvc.Core;
using PeopleManager.Ui.Mvc.Models;

namespace PeopleManager.Ui.Mvc.Controllers
{
    public class OrganizationsController : Controller
    {
        private readonly PeopleManagerDbContext _peopleManagerDbContext;

        public OrganizationsController(PeopleManagerDbContext peopleManagerDbContext)
        {
            _peopleManagerDbContext = peopleManagerDbContext;
        }

        public IActionResult Index()
        {
            var organizations = _peopleManagerDbContext.Organizations.ToList();
            return View(organizations);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Organization organization)
        {
            _peopleManagerDbContext.Organizations.Add(organization);
            _peopleManagerDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var organization = _peopleManagerDbContext.Organizations
                .FirstOrDefault(p => p.Id == id);

            if (organization == null)
            {
                return RedirectToAction("Index");
            }

            return View(organization);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, [FromForm] Organization organization)
        {
            var dbOrganization = _peopleManagerDbContext.Organizations
                .FirstOrDefault(p => p.Id == id);

            if (dbOrganization == null)
            {
                return RedirectToAction("Index");
            }

            //Mapping
            dbOrganization.Name = organization.Name;
            dbOrganization.Description = organization.Description;

            _peopleManagerDbContext.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost("[controller]/Delete/{id:int?}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var organization = _peopleManagerDbContext.Organizations
                .FirstOrDefault(p => p.Id == id);

            if (organization == null)
            {
                return RedirectToAction("Index");
            }

            _peopleManagerDbContext.Organizations.Remove(organization);

            _peopleManagerDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
