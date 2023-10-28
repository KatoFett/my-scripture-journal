using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.Journal
{
    public class DeleteModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public DeleteModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
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

            var scripturejournalentry = await _context.ScriptureJournalEntry.FirstOrDefaultAsync(m => m.Id == id);

            if (scripturejournalentry == null)
            {
                return NotFound();
            }
            else 
            {
                ScriptureJournalEntry = scripturejournalentry;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ScriptureJournalEntry == null)
            {
                return NotFound();
            }
            var scripturejournalentry = await _context.ScriptureJournalEntry.FindAsync(id);

            if (scripturejournalentry != null)
            {
                ScriptureJournalEntry = scripturejournalentry;
                _context.ScriptureJournalEntry.Remove(ScriptureJournalEntry);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
