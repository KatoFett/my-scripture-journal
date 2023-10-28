using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyScriptureJournal.Data;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Pages.Journal
{
    public class CreateModel : PageModel
    {
        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public CreateModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ScriptureJournalEntry ScriptureJournalEntry { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.ScriptureJournalEntry == null || ScriptureJournalEntry == null)
            {
                return Page();
            }

            ScriptureJournalEntry.Added = DateTime.Now;

            _context.ScriptureJournalEntry.Add(ScriptureJournalEntry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
