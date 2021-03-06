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
            var data = _storeService.GetAllStoreForApi();
            int number = data.Count();
            store.StoreId = "S-0" + number.ToString();
            store.Status = "Open";
            _storeService.CreateStore(store);
            return store;
        }

        [HttpPut("{id}")]
        public IActionResult EditStore([FromBody] Store store, string id)
        {
            var stores = _storeService.GetStoreById(id);
            if (stores == null)
            {
                return NotFound();
            }
            store.StoreId = id;
            _storeService.EditStore(id, store);
            return NoContent();
        }

        [HttpGet("{storeId}")]
        public IActionResult DeleteStore(string storeId)
        {
            var stores = _storeService.GetStoreById(storeId);
            var statusChange = stores.Status;
            if (stores == null)
            {
                return NotFound();
            }
            if (statusChange == "Open")
            {
                statusChange = "Close";
            }
            stores.Status = statusChange;
            _storeService.DeleteStore(storeId , stores);
            return NoContent();
        }

    }
}