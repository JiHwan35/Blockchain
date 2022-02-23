using System;
using System.Text;
using System.Security.Cryptography;

namespace BlockchainTest.Class
{
    public class Block
    {
        public Block(BlockHeader blockHeader, Object transactions)
        {
            this.blockHeader = blockHeader;
            this.transactions = transactions;
        }

        private int blockSize;
        private BlockHeader blockHeader;
        private int transactionCount;
        private object transactions;

     
        public string getBlockHash()
        {
            byte[] bytes = blockHeader.toByteArray();
            using (SHA256Managed hashstring = new SHA256Managed())
            {
                
                byte[] blockHash = hashstring.ComputeHash(bytes);
                blockHash = hashstring.ComputeHash(blockHash);

                return ByteArrayToString(blockHash);
            }
        }
            
       public string getBlocktransaction()
       {
           object tran = transactions;

           return tran.ToString();
       }
        public static string ByteArrayToString(byte[] bts)
        {
            StringBuilder strBld = new StringBuilder();
            foreach (byte bt in bts)
                strBld.AppendFormat("{0:X2}", bt);

            return strBld.ToString();
        }

    }

    public class BlockHeader
    {
        public BlockHeader(byte[] previousBlockHash, object transactions)
        {
            this.previousBlockHash = previousBlockHash;
            this.merkleRootHash = transactions.GetHashCode();
           
        }
    
        private byte[] previousBlockHash;
        private int merkleRootHash;
        private int timestamp;
        private static uint difficultyTarget = 5;
        private static int nonce = 0;
        
        
        public int ProofOfWorkCount()
        {
            using (SHA256Managed hashstring = new SHA256Managed())
            {
                byte[] bt;
                string sHash = string.Empty;
                while (sHash == string.Empty || sHash.Substring(0, (int)difficultyTarget) != ("").PadLeft((int)difficultyTarget, '0'))
                {
                    bt = Encoding.UTF8.GetBytes(merkleRootHash + nonce.ToString());
                    sHash = Block.ByteArrayToString(hashstring.ComputeHash(bt));
                    nonce++;
                }
                return nonce;
            }
        }

      
        public byte[] toByteArray()
        {
            string tmpStr = "";
            
            if (previousBlockHash != null)
            {
                tmpStr += Convert.ToBase64String(previousBlockHash);
            }
            tmpStr += merkleRootHash.ToString();
            return Encoding.UTF8.GetBytes(tmpStr);
        }

    }
}