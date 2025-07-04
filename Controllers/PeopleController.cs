using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Respuestas_HTTTP.Repository;
using Respuestas_HTTTP.Services;

namespace Respuestas_HTTTP.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase {

        private IPeopleService _peopleService;


        public PeopleController([FromKeyedServices("peopleService")]IPeopleService iPeopleService) 
        {
            //_peopleService = new PeopleServiceImpl(); quitamos la implementacion al agragarlo ya en Program.cs

            _peopleService = iPeopleService;
        }


        [HttpGet]
        public List<People> GetAll() => PeopleRepository.People;

        /*
         *  public List<People> GetAll() 
         *  {
         *      return PeopleRepository.People;
         *  } 
         */

        /*
         * Puede devolver tanto respuestas HTTP como el objeto directamente 
         * Tipado fuerte - especifica el tipo de datos que devuelve
         * Mejor para documentación automática (Swagger/OpenAPI)
         * // ✅ Para operaciones que devuelven datos
         */

        [HttpGet("{id}")]
        //public ActionResult<People> GetById(int id) => PeopleRepository.People.FirstOrDefault(x => x.Id == id);

        public ActionResult<People> GetById(int id) {
            var people = PeopleRepository.People.FirstOrDefault(p => p.Id == id);

            if (people == null) {
                return NotFound("No se encontro al usuario especificado");
            }

            return Ok(people);
        }


        [HttpGet("search/{search}")]
        public ActionResult<List<People>> GetSearch(string search) {
            var list = PeopleRepository.People.Where(p => p.Name.ToLower().Contains(search.ToLower()));

            if (!list.Any())
                return NotFound("No se encontro a ningun usuario especificado");

            return Ok(list);
        }

        /**
         * Solo puede devolver respuestas HTTP 
         * No tipado - no especifica qué tipo de datos devuelve, Más genérico y flexible
         * // ✅ Para operaciones que solo cambian estado
         */
        [HttpPost]
        public IActionResult Add(People people) {
            if (!_peopleService.Validate(people))
                return BadRequest("Error: El nombre no debe ser vacio o nulo");


            PeopleRepository.People.Add(people);

            //return NoContent();
            return CreatedAtAction(nameof(GetById), new { id = people.Id }, people);
        }

        /*
         * Principios SOLID
         * 
         *  S ::  Single Responsability Principle
         *  O ::  Open-Closed Principle
         *  L ::  Liskov Subtitution Principle
         *  I ::  Interface Segregation Principle
         *  D ::  Dependency Inversion Principle
         * 
         */
    }
}
