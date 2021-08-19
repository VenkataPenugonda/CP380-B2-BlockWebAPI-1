using CP380_B1_BlockList.Models;
using CP380_B2_BlockWebAPI.Models;
using CP380_B2_BlockWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CP380_B2_BlockWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BlocksController : ControllerBase
    {
        private readonly BlockList blocklist;
        public BlocksController(BlockList blockList)
        {
            blocklist = blockList;
        }

        /// <summary>
        /// Retrieve all the blocks in the chain
        /// </summary>
        /// <returns></returns>
        [Route("/Blocks")]
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(blocklist.Chain.Select(block => new BlockSummary()
            {
                TimeStamp = block.TimeStamp,
                PreviousHash = block.PreviousHash,
                Hash = block.Hash
            }));
        }

        /// <summary>
        /// Retrieve a specific block
        /// </summary>
        /// <returns></returns>
        [Route("/Blocks/{hash}")]
        [HttpGet]
        public string GetHash(string hash)
        {
            var obj = blocklist.Chain
                .Where(block => block.Hash.Equals(hash));

            if (obj != null && obj.Count() > 0)
            {
                return JsonConvert.SerializeObject(obj.Select(block => new BlockSummary()
                    {
                        Hash = block.Hash,
                        PreviousHash = block.PreviousHash,
                        TimeStamp = block.TimeStamp
                    }).First());
            }

            return JsonConvert.SerializeObject(NotFound());
        }

        /// <summary>
        /// Retrieve all payloads for a given block
        /// </summary>
        /// <returns></returns>
        [Route("/Blocks/{hash}/Payloads")]
        [HttpGet]
        public string GetAllPayloads(string hash)
        {
            var obj = blocklist.Chain
                        .Where(block => block.Hash.Equals(hash));

            if (obj != null && obj.Count() > 0)
            {
                return JsonConvert.SerializeObject(obj
                    .Select(block => block.Data).First());
            }

            return JsonConvert.SerializeObject(NotFound());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        [Route("/Blocks")]
        [HttpPost]
        public string Post(Block block)
        {
            BlockListService _blocklist = new();
            Block obj = _blocklist.SubmitNewBlock(block.Hash, block.Nonce, block.TimeStamp);
            if(obj != null)
            {
                return JsonConvert.SerializeObject(obj);
            }
            else
            {
                return "401 Bad request";
            }
        }
    }
}
