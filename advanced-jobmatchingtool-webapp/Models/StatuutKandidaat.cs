using System.ComponentModel.DataAnnotations;

namespace advanced_jobmatchingtool_webapp.Models
{
    public class StatuutKandidaat
    {
        public int Id { get; set; }

        
        public DiagnoseOpties Diagnose { get; set; }

        
        public bool HeeftIMWStatuut { get; set; }

        //beveiliging IMW + uploaden bestand.
        //Opslag van bestandsnamen of paden voor statuten
        public string IMWStatuutBestand { get; set; }
        public string IMWStatuutBestandOrigineleNaam { get; set; }


        public bool HulpNodigBijInvullen { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        //extra bestanden kunnen uploaden pdf, jpeg, doc, txt, png
        //gn bestanden > 20mb

        //bool contact opnemen ja of nee
        // pop-upje met tekstblok om iets in te vullen als hulpmiddel => doorsturen naar Marijke.
        //kopietje naar kandidaat als bevestiging.

        //check statuut ja /nee

        

    }
}
