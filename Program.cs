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
        static string savetran = "";
        static string savehash = "";

        static void Main(string[] args)
        {
            int k = 5;
    
            string transactions = "Jihwan";
            
            //GenesisBlock create
            BlockHeader blockheader = new BlockHeader(null, transactions);
            Block genesisBlock = new Block(blockheader, transactions);
            Console.WriteLine("Genesis Block Hash : {0} ", genesisBlock.getBlockHash());

            //hash save
            savehash = genesisBlock.getBlockHash();
            
            //New Block mining
            Block previousBlock = genesisBlock;
            for(int i = 0; i< k; i++)
            {
                previousBlock = miner(previousBlock,transactions,i+1);
            }
            
            //newblock save
            savehash = previousBlock.getBlockHash();
            savetran = previousBlock.getBlocktransaction();
            Console.WriteLine(savehash);
            Console.WriteLine(savetran);
        }

        static Block miner(Block previousBlock, string transactions,int index){
            
            BlockHeader secondBlockheader = new BlockHeader(Encoding.UTF8.GetBytes(previousBlock.getBlockHash()), transactions);
            Block nextBlock = new Block(secondBlockheader, transactions);
        
            int count = secondBlockheader.ProofOfWorkCount();

            Console.WriteLine("The {0}th new Block has been mined \nNew Block Hash : {1} ", index.ToString(), nextBlock.getBlockHash());
            Console.WriteLine(transactions);
            previousBlock = nextBlock;

            return previousBlock;    
        }

    }
}