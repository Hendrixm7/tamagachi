using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tamagachi.Models;
namespace Tamagachi.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        public DatabaseContext db { get; set; } = new DatabaseContext ();

        [HttpGet]
        public List<Pet> GetAllPets ()
        {
            var Pets = db.Pets.OrderBy (m => m.Name);
            return Pets.ToList ();
        }

        [HttpGet ("{id}")]
        public Pet GetOnePet (int id)
        {
            var item = db.Pets.FirstOrDefault (i => i.Id == id);
            return item;
        }

        [HttpPost]
        public Pet CreatePet (Pet item)
        {
            db.Pets.Add (item);
            db.SaveChanges ();
            return item;
        }

        [HttpPost ("multiple")]
        public List<Pet> AddManyPets (List<Pet> items)
        {
            db.Pets.AddRange (items);
            db.SaveChanges ();
            return items;
        }

        [HttpPut ("{id}/play")]
        public Pet PlayWithPet (int id)
        {
            var item = db.Pets.FirstOrDefault (i => i.Id == id);
            item.HappinessLevel += 5;
            item.HungerLevel += 3;

            db.SaveChanges ();

            return item;
        }

        [HttpPut ("{id}/feed")]
        public Pet FeedPet (int id)
        {
            var item = db.Pets.FirstOrDefault (i => i.Id == id);
            item.HappinessLevel += 3;
            item.HungerLevel -= 5;

            db.SaveChanges ();

            return item;
        }

        [HttpPut ("{id}/scold")]
        public Pet ScoldPet (int id)
        {
            var item = db.Pets.FirstOrDefault (i => i.Id == id);
            item.HappinessLevel -= 3;

            db.SaveChanges ();

            return item;
        }

        [HttpPatch ("{id}")]
        public Pet HappinessLevel (int id, Pet data)
        {
            var item = db.Pets.FirstOrDefault (i => i.Id == id);
            item.HappinessLevel = data.HappinessLevel;
            db.SaveChanges ();
            return item;
        }

        [HttpDelete ("{id}")]
        public ActionResult DeleteOne (int id)
        {
            var item = db.Pets.FirstOrDefault (f => f.Id == id);
            if (item == null)
            {
                return NotFound ();
            }
            db.Pets.Remove (item);
            db.SaveChanges ();
            return Ok ();
        }
    }
}