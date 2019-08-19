using System.Threading.Tasks;
using API.Base.Web.Base.Controllers.Ui;
using API.Base.Web.Base.Database.Repository.Helpers;
using API.StartApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.StartApp.Controllers.Ui
{
    public class ExampleUiController : GenericUiController<ExampleEntity>
    {
        // GET: Controller
        [AllowAnonymous]
        public override async Task<IActionResult> Index()
        {
            return await base.Index();
        }

        // POST: ExampleAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Create([Bind("Name,Age")] ExampleEntity exampleEntity)
        {
            if (ModelState.IsValid)
            {
                await Repo.Add(exampleEntity);
                return RedirectToAction(nameof(Index));
            }

            return View(exampleEntity);
        }

        // POST: ExampleAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(string id, [Bind("Id,Name,Age")] ExampleEntity exampleEntity)
        {
            if (id != exampleEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existing = await Repo.GetOne(id);
                if (existing == null)
                {
                    return NotFound();
                }

                try
                {
                    var epb = EntityUpdateHelper<ExampleEntity>.GetEpbFor(exampleEntity, existing);
                    var updated = await Repo.Patch(epb);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await EntityExists(exampleEntity.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(exampleEntity);
        }
    }
}