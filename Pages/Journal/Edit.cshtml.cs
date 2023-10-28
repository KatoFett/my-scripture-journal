using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.Journal
{
    public class EditModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public EditModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ScriptureJournalEntry ScriptureJournalEntry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ScriptureJournalEntry == null)
            {
                return NotFound();
            }

            var scripturejournalentry =  await _context.ScriptureJournalEntry.FirstOrDefaultAsync(m => m.Id == id);
            if (scripturejournalentry == null)
            {
                return NotFound();
            }
            ScriptureJournalEntry = scripturejournalentry;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ScriptureJournalEntry).State = EntityState.Modified;
            _context.Entry(ScriptureJournalEntry).Property(nameof(ScriptureJournalEntry.Added)).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScriptureJournalEntryExists(ScriptureJournalEntry.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ScriptureJournalEntryExists(int id)
        {
          return (_context.ScriptureJournalEntry?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
