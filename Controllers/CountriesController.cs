using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Prueba.Model;
using Prueba.Pagination;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Prueba.Controllers
{
    [Authorize]
    [ApiController]
    public class CountriesController : Controller
    {
        private readonly ISO3166DBContext _context;

        public CountriesController(ISO3166DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get Countries as paginated result.
        /// </summary>
        /// <param name="SearchCriteria">Texto de búsqueda.</param>
        /// <param name="SortField">Nombre de campo por el cual ordenar (distingue mayúsculas).</param>
        /// <param name="SortType">Tipo de orden: ASC (ascendente) / DESC (descendente).</param>
        /// <param name="CurrentPage">Número de página a obtener.</param>
        /// <param name="RecordsPerPage">Número de registros por página.</param>
        /// <response code="200">OK</response>
        /// <response code="400">Solicitud incorrecta</response>
        /// <response code="401">No tiene acceso al recurso. Autenticación requerida.</response>
        /// <response code="404">No existen datos para mostrar o no tiene permiso de acceso</response>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Countries")]
        public GenericPaginator<Country> Get(string SearchCriteria, string SortField = "Name", string SortType = "ASC", int CurrentPage = 1, int RecordsPerPage = 10)
        {
            List<Country> _Country;
            GenericPaginator<Country> _CountryPaginatedResult;
            _Country = _context.Countries.ToList();

            // Apply SearchCriteria
            if (!string.IsNullOrEmpty(SearchCriteria))
            {
                foreach (var item in SearchCriteria.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    _Country = _Country.Where(x => x.Name.Contains(item) || x.Iso2.Contains(item) || x.Iso3.Contains(item)).ToList();
                }
            }

            // Apply Sort Field and Sort Type
            switch (SortField)
            {
                case "name":
                    if (SortType.ToLower() == "desc")
                        _Country = _Country.OrderByDescending(x => x.Name).ToList();
                    else if (SortType.ToLower() == "asc")
                        _Country = _Country.OrderBy(x => x.Name).ToList();
                    break;

                case "region":
                    if (SortType.ToLower() == "desc")
                        _Country = _Country.OrderByDescending(x => x.Region).ToList();
                    else if (SortType.ToLower() == "asc")
                        _Country = _Country.OrderBy(x => x.Region).ToList();
                    break;

                //Appply default sort fiel and sort type
                default:
                    _Country = _Country.OrderBy(x => x.Name).ToList();
                    break;
            }

            //Apply Pagination 
            int _TotalRecs = 0;
            int _TotalPages = 0;

            _TotalRecs = _Country.Count(); // Get total records in table
            _Country = _Country.Skip((CurrentPage - 1) * RecordsPerPage).Take(RecordsPerPage).ToList(); // Setup Pagination
            _TotalPages = (int)Math.Ceiling((double)_TotalRecs / RecordsPerPage); // Get total pages

            // Set all pagination params and return result
            _CountryPaginatedResult = new GenericPaginator<Country>()
            {
                CurrentPage = CurrentPage,
                RecordsPerPage = RecordsPerPage,
                TotalRecords = _TotalRecs,
                TotalPages = _TotalPages,
                CurrentSearch = SearchCriteria,
                CurrentSort = SortField,
                CurrentSortType = SortType,
                Result = _Country
            };

            return _CountryPaginatedResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CountryName"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Solicitud incorrecta</response>
        /// <response code="401">No tiene acceso al recurso. Autenticación requerida.</response>
        /// <response code="404">No existen datos para mostrar o no tiene permiso de acceso</response>
        [HttpGet]
        [Route("api/Countries/{CountryName}")]
        public dynamic Get(string CountryName)
        {
            if (CountryName == null)
            {
                return NotFound();
            }

            var Country = _context.Countries.FirstOrDefault(c => c.Name.Contains(CountryName));

            if (Country == null)
            {
                return NotFound();
            }
            else
            {
                return Country;
            }
        }

        /// <summary>
        /// Get Country Filtering by ISO2 Code
        /// </summary>
        /// <param name="ISO2"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Solicitud incorrecta</response>
        /// <response code="401">No tiene acceso al recurso. Autenticación requerida.</response>
        /// <response code="404">No existen datos para mostrar o no tiene permiso de acceso</response>
        [HttpGet]
        [Route("api/GetCountryByISO2Code/{ISO2}")]
        public dynamic GetCountryByISO2Code(string ISO2)
        {
            if (ISO2 == null)
            {
                return NotFound();
            }

            var Country = _context.Countries.FirstOrDefault(c => c.Iso2.Equals(ISO2));

            if (Country == null)
            {
                return NotFound();
            }
            else
            {
                return Country;
            }
        }

        /// <summary>
        /// Create a new country with subdivision and cities
        /// </summary>
        /// <param name="NewCountry"></param>
        /// <returns>Ok if method success</returns>
        /// <response code="201">Record created</response>
        /// <response code="400">Solicitud incorrecta</response>
        /// <response code="401">No tiene acceso al recurso. Autenticación requerida.</response>
        /// <response code="404">No existen datos para mostrar o no tiene permiso de acceso</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("api/AddNewCountry")]
        public dynamic AddNewCountry([FromBody] Country_Item NewCountry)
        {
            try
            {
                Country _Country;
                _Country = new()
                {
                    Name = NewCountry.Name,
                    Iso3 = NewCountry.Iso3,
                    Iso2 = NewCountry.Iso2,
                    NumericCode = NewCountry.NumericCode,
                    PhoneCode = NewCountry.PhoneCode,
                    Capital = NewCountry.Capital,
                    Currency = NewCountry.Currency,
                    CurrencyName = NewCountry.CurrencyName,
                    CurrencySymbol = NewCountry.CurrencySymbol,
                    Tld = NewCountry.Tld,
                    Native = NewCountry.Native,
                    Region = NewCountry.Region,
                    Subregion = NewCountry.Subregion,
                    Latitude = NewCountry.Latitude,
                    Longitude = NewCountry.Longitude,
                    Emoji = NewCountry.Emoji,
                    EmojiU = NewCountry.EmojiU
                };

                _context.Countries.Add(_Country);
                _context.SaveChanges();
                NewCountry.CountryId = _Country.CountryId;
                return CreatedAtAction("AddNewCountry", NewCountry);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Remove a country and all child subregion and child cities
        /// </summary>
        /// <param name="ISO2"></param>
        /// <returns>Ok if method success</returns>
        /// <response code="204">OK</response>
        /// <response code="400">Solicitud incorrecta</response>
        /// <response code="401">No tiene acceso al recurso. Autenticación requerida.</response>
        /// <response code="404">No existen datos para mostrar o no tiene permiso de acceso</response>
        [HttpDelete]
        [Route("api/DelCountryRecord")]        
        public dynamic DelCountryRecord(string ISO2)
        {
            try
            {
                if (ISO2 == null)
                {
                    return NotFound();
                }

                var _ItemToDel = _context.Countries.FirstOrDefault(c => c.Iso2.Equals(ISO2));

                if (_ItemToDel == null)
                {
                    return NotFound();
                }
                else
                {
                    //Get State to delete
                    var _ChildStatesToDel = (States)_context.States.Where(s => s.CountryId == _ItemToDel.CountryId);
                    //Get Cities to delete
                    var _ChildCitiesToDel = (City)(from c in _context.Cities where c.State.CountryId == _ItemToDel.CountryId select c);
                    //remover cities
                    _context.Cities.Remove(_ChildCitiesToDel);
                    //remove states
                    _context.States.Remove(_ChildStatesToDel);
                    //remove country
                    _context.Countries.Remove(_ItemToDel);
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
