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
    public class IndexModel : PageModel
    {
        public enum SortMode
        {
            Added,
            Book,
        }

        private readonly MyScriptureJournal.Data.MyScriptureJournalContext _context;

        public IndexModel(MyScriptureJournal.Data.MyScriptureJournalContext context)
        {
            _context = context;
        }

        public IList<ScriptureJournalEntry> ScriptureJournalEntry { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string Book { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Notes { get; set; }

        [BindProperty(SupportsGet = true)]
        public SortMode Sort { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.ScriptureJournalEntry != null)
            {
                IQueryable<ScriptureJournalEntry> entries = _context.ScriptureJournalEntry;

                if (!string.IsNullOrEmpty(Book))
                    entries = entries.Where(e => e.Book.ToLower() == Book.ToLower());

                if (!string.IsNullOrEmpty(Notes))
                    entries = entries.Where(e => e.Notes != null && e.Notes.ToLower().Contains(Notes.ToLower()));

                entries = Sort switch
                {
                    SortMode.Book => entries.OrderBy(e => e.Book),
                    _ => entries.OrderBy(e => e.Added),
                };

                ScriptureJournalEntry = await entries.ToListAsync();
            }
        }
    }
}
