using System;
using System.Collections.Generic;
using System.Text;
using BlockchainTest.Class;
using System.Diagnostics;

namespace BlockchainTest
{
    class Program
    {
        List<Block> blockchain = new List<Block>();

        static void Main(string[] args)
        {
            int k = 5;
            string[] transactions = {""};
            
            
            BlockHeader blockheader = new BlockHeader(null, transactions);
            Block genesisBlock = new Block(blockheader, transactions);
            Console.WriteLine("Genesis Block Hash : {0} ", genesisBlock.getBlockHash());
        
            
            
            Block previousBlock = genesisBlock;
            for(int i = 0; i< k; i++)
            {
                previousBlock = miner(previousBlock,transactions,i+1);
            }
        }

        static Block miner(Block previousBlock, string[] transactions,int index){
            
            BlockHeader secondBlockheader = new BlockHeader(Encoding.UTF8.GetBytes(previousBlock.getBlockHash()), transactions);
            Block nextBlock = new Block(secondBlockheader, transactions);
        
            int count = secondBlockheader.ProofOfWorkCount();

            Console.WriteLine("The {0}th new Block has been mined \nNew Block Hash : {1} ", index.ToString(), nextBlock.getBlockHash());
            previousBlock = nextBlock;

            return previousBlock;    
        }

    }
}