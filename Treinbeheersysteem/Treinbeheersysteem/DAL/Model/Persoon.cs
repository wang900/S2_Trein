using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Treinbeheersysteem.DAL.Model
{
    public class Persoon : Entity
    {
        public string Voornaam { get; protected set; }
        public string Achternaam { get; protected set; }
        public string Email { get; protected set; }

        public Persoon()
        {

        }

        public Persoon(string voornaam, string achternaam, string email)
        {
            Voornaam = voornaam;
            Achternaam = achternaam;
            Email = email;
        }

        public Persoon(long id, string email)
        {
            Id = id;
            Email = email;
        }
        public void SetEmail(string email)
        {
            Email = email;
        }

        //protected void ChangeEmail(string email)
        //{
        //    Email = email;
        //}

        //protected void ChangeVoornaam(string voornaam)
        //{
        //    Voornaam = voornaam;
        //}

        //protected void ChangeAchternaam(string achternaam)
        //{
        //    Achternaam = achternaam;
        //}
    }
}
