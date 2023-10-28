using System.ComponentModel.DataAnnotations;

namespace MyScriptureJournal.Models
{
    public class ScriptureJournalEntry
    {
        const int NotesDisplayLength = 30;

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [MinLength(3)]
        [Required]
        public string Book { get; set; }

        [Range(1, int.MaxValue)]
        [Required]
        public int Chapter { get; set; }

        [Range(1, int.MaxValue)]
        [Required]
        public int Verse { get; set; }

        [StringLength(300)]
        public string? Notes { get; set; }

        public string? NotesShort
        {
            get
            {
                if (Notes == null) return null;
                if (Notes.Length > NotesDisplayLength) return Notes.Substring(0, NotesDisplayLength) + "...";
                return Notes;
            }
        }

        public DateTime Added { get; set; }
    }
}
