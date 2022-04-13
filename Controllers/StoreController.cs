using StoreBK.Models;
using StoreBK.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using MongoDB.Driver;



namespace StoreBK.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly StoreService _storeService;
        public StoreController(StoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public ActionResult<List<Store>> GetAllStore() => _storeService.GetAllStore();

        [HttpGet("{id}")]
        public ActionResult<Store> GetStoreById(string id)
        {
            var store = _storeService.GetStoreById(id);
            if (store == null)
            {
                return NotFound();
            }
            return store;
        }

        [HttpPost]
        public Store CreateStore([FromBody] Store store)
        {
            var data = _storeService.GetAllStore();
            int number = data.Count();
            store.StoreId = "S-0" + number.ToString();
            _storeService.CreateStore(store);
            return store;
        }

        [HttpPut("{storeId}")]
        public IActionResult EditStore([FromBody] Store store, string storeId)
        {
            var stores = _storeService.GetStoreById(storeId);
            if (stores == null)
            {
                return NotFound();
            }
            store.StoreId = storeId;
            _storeService.EditStore(storeId, store);
            return NoContent();
        }

        [HttpDelete("{storeId}")]
        public IActionResult DeleteStore(string storeId)
        {
            var stores = _storeService.GetStoreById(storeId);
            if (stores == null)
            {
                return NotFound();
            }
            _storeService.DeleteStore(storeId);
            return NoContent();
        }

    }
}