using CP380_B1_BlockList.Models;
using CP380_B2_BlockWebAPI.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CP380_B2_BlockWebAPI.Services
{
    public class BlockListService
    {
        BlockList _blockList = new BlockList();
        PendingPayloads _payloadsList = new PendingPayloads();
        public BlockListService (){}
        public BlockListService(IConfiguration configuration, BlockList blockList, PendingPayloads pendingPayloads)
        {
            _blockList = blockList;
            _payloadsList = pendingPayloads;
        }

        /// <summary>
        /// Submit New Block
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="nonce"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public Block SubmitNewBlock(string hash, int nonce, DateTime timestamp)
        {
            Block block = new(timestamp, hash, _payloadsList.Payloads);
            block.CalculateHash();
            if (block.Hash.Equals(hash))
            {
                _blockList.Chain.Add(block);
                block.Data.Clear();
                return block;
            }
            else
            {
                return null;
            }
        }
    }
}
