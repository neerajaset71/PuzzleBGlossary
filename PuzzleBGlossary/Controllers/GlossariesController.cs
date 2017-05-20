using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PuzzleBGlossary.Models;

namespace PuzzleBGlossary.Controllers
{
    public class GlossariesController : ApiController
    {
        private PuzzleBGlossaryContext db = new PuzzleBGlossaryContext();

        // GET: api/Glossaries
        public IQueryable<Glossary> GetGlossaries()
        {
            var glossaries = from g in db.Glossaries
                             orderby g.Term ascending
                             select g;

            return glossaries;
        }

        // GET: api/Glossaries/5
        [ResponseType(typeof(Glossary))]
        public async Task<IHttpActionResult> GetGlossary(int id)
        {
            Glossary glossary = await db.Glossaries.FindAsync(id);
            if (glossary == null)
            {
                return NotFound();
            }

            return Ok(glossary);
        }

        // GET: api/Glossaries/term
        [ResponseType(typeof(Glossary))]
        public async Task<IHttpActionResult> GetGlossaryByTerm(string term)
        {
            var glossary = await (from g in db.Glossaries
                                 where g.Term == term
                                 select g).FirstOrDefaultAsync();

            if (glossary == null)
            {
                return NotFound();
            }
            return Ok(glossary);
        }

        // PUT: api/Glossaries/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGlossary(int id, Glossary glossary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != glossary.ID)
            {
                return BadRequest();
            }

            if (GlossaryTermExists(glossary.ID, glossary.Term))
            {
                return Conflict();
            }

            db.Entry(glossary).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GlossaryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Glossaries
        [ResponseType(typeof(Glossary))]
        public async Task<IHttpActionResult> PostGlossary(Glossary glossary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (GlossaryTermExists(glossary.ID, glossary.Term))
            {
                return Conflict();
            }

            db.Glossaries.Add(glossary);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GlossaryExists(glossary.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = glossary.ID }, glossary);
        }

        // DELETE: api/Glossaries/5
        [ResponseType(typeof(Glossary))]
        public async Task<IHttpActionResult> DeleteGlossary(int id)
        {
            Glossary glossary = await db.Glossaries.FindAsync(id);
            if (glossary == null)
            {
                return NotFound();
            }

            db.Glossaries.Remove(glossary);

            await db.SaveChangesAsync();

            return Ok(glossary);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GlossaryExists(int id)
        {
            return db.Glossaries.Count(e => e.ID == id) > 0;
        }

        private bool GlossaryTermExists(int id, string term)
        {               
            return db.Glossaries.Count(e => e.Term == term && e.ID != id) > 0;
        }
    }
}