using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Prueba.Pagination;
using System.Linq;
using Prueba.Model;
using System;

namespace Prueba.Controllers
{
    [ApiController]
    public class CitiesController : Controller
    {
        private readonly ISO3166DBContext _context;

        public CitiesController(ISO3166DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all cities as a paginated result
        /// </summary>
        /// <param name="SearchCriteria">Texto de búsqueda.</param>
        /// <param name="SortField">Nombre de campo por el cual ordenar (distingue mayúsculas).</param>
        /// <param name="SortType">Tipo de orden: ASC (ascendente) / DESC (descendente).</param>
        /// <param name="CurrentPage">Número de página a obtener.</param>
        /// <param name="RecordsPerPage">Número de registros por página.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Cities")]
        public GenericPaginator<City> Get(string SearchCriteria, string SortField = "Name", string SortType = "ASC", int CurrentPage = 1, int RecordsPerPage = 10)
        {

            List<City> _Cities;
            GenericPaginator<City> _CitiesPaginatedResult;
            _Cities = _context.Cities.ToList();

            // Apply SearchCriteria
            if (!string.IsNullOrEmpty(SearchCriteria))
            {
                foreach (var item in SearchCriteria.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    _Cities = _Cities.Where(x => x.Name.Contains(item)).ToList();
                }
            }

            // Apply Sort Field and Sort Type
            switch (SortField)
            {
                case "CityId":
                    if (SortType.ToLower() == "desc")
                        _Cities = _Cities.OrderByDescending(x => x.CityId).ToList();
                    else if (SortType.ToLower() == "asc")
                        _Cities = _Cities.OrderBy(x => x.CityId).ToList();
                    break;

                case "name":
                    if (SortType.ToLower() == "desc")
                        _Cities = _Cities.OrderByDescending(x => x.Name).ToList();
                    else if (SortType.ToLower() == "asc")
                        _Cities = _Cities.OrderBy(x => x.Name).ToList();
                    break;

                //Appply default sort fiel and sort type
                default:
                    _Cities = _Cities.OrderBy(x => x.Name).ToList();
                    break;
            }

            //Apply Pagination 
            int _TotalRecs = 0;
            int _TotalPages = 0;

            _TotalRecs = _Cities.Count(); // Get total records in table
            _Cities = _Cities.Skip((CurrentPage - 1) * RecordsPerPage).Take(RecordsPerPage).ToList(); // Setup Pagination
            _TotalPages = (int)Math.Ceiling((double)_TotalRecs / RecordsPerPage); // Get total pages

            // Set all pagination params and return result
            _CitiesPaginatedResult = new GenericPaginator<City>()
            {
                CurrentPage = CurrentPage,
                RecordsPerPage = RecordsPerPage,
                TotalRecords = _TotalRecs,
                TotalPages = _TotalPages,
                CurrentSearch = SearchCriteria,
                CurrentSort = SortField,
                CurrentSortType = SortType,
                Result = _Cities
            };

            return _CitiesPaginatedResult;
        }

        [HttpGet]
        [Route("api/Cities/{CityName}")]
        public dynamic Get(string CityName)
        {
            if (CityName == null)
            {
                return NotFound();
            }

            var city = _context.Cities.FirstOrDefault(c => c.Name.Contains(CityName));
           
            if (city == null)
            {
                return NotFound();
            }
            else
            {
                return city;
            }            
        }

        /// <summary>
        /// Create a new city child of a parent subdivision or state
        /// </summary>
        /// <param name="NewCitie"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/AddNewCity")]
        public dynamic AddNewCity([FromBody] City_Item NewCitie)
        {
            try
            {
                City _City;
                _City = new()
                {
                    Name=NewCitie.Name,
                    StateId=NewCitie.StateId,
                    Latitude=NewCitie.Latitude,
                    Longitude=NewCitie.Longitude,
                    WikiDataId=NewCitie.WikiDataId
                };

                _context.Cities.Add(_City);
                _context.SaveChanges();
                return CreatedAtAction(nameof(City), new { id = NewCitie.CityId }, NewCitie);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        /// <summary>
        /// Remove a city record by its given name
        /// </summary>
        /// <param name="CityName"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/DelCityRecord")]
        public dynamic DelCityRecord(string CityName)
        {
            try
            {
                if (CityName == null)
                {
                    return NotFound();
                }

                var _ItemToDel = _context.Cities.FirstOrDefault(c => c.Name.Contains(CityName));

                if (_ItemToDel == null)
                {
                    return NotFound();
                }
                else
                {
                    //remover cities
                    _context.Cities.Remove(_ItemToDel);
                    _context.SaveChanges();

                    return NoContent();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
