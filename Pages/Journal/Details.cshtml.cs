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
    public class DetailsModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public DetailsModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

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
    }
}
