using System;
using System.Collections.Generic;
using System.Data;

namespace CP380_B1_BlockList.Models
{
    public class BlockList
    {
        public IList<Block> Chain { get; set; }

        public int Difficulty { get; set; } = 2;

        public BlockList()
        {
            Chain = new List<Block>();
            MakeFirstBlock();
        }

        public void MakeFirstBlock()
        {
            var block = new Block(DateTime.Now, null, new List<Payload>());
            block.Mine(Difficulty);
            Chain.Add(block);
        }

        public void AddBlock(Block block)
        {
            // TODO - Done
            var block1 = new Block(block.TimeStamp, block.PreviousHash, block.Data);
            block1.Mine(Difficulty);
            Chain.Add(block1);
        }

        public bool IsValid()
        {
            // TODO
            for (int i = 1; i < Chain.Count; i++)
            {
                Block prevVal = Chain[i - 1];
                Block curtVal = Chain[i];
                if (curtVal.Hash != curtVal.CalculateHash())
                    return false;
                if (curtVal.PreviousHash != prevVal.Hash)
                    return false;
            }
            return true;
        }
    }
}
