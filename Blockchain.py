import hashlib
import json
from time import time
from uuid import uuid4

class Blockchain(object):
    
    def __init__(self):
        self.chain=[]
        self.current_transaction=[]

        self.new_block(previous_hash = 1,proof = 100) #genesis block create
        
    def new_block(self, proof, previous_hash = None): #new block create - index,time,transaction,proof,previous_hash
        block = {
            'index':len(self.chain)+1,
            'timestamp' : time(),
            'transaction' : self.current_transaction,
            'proof': proof,
            'previous_hash' : previous_hash or self.hash(self.chain[-1])    
        }
        #self.current_transaction = []

        self.chain.append(block)
        return block    

    def new_transaction(self,sender,recipient,amount): #new transaction create - self,sender,recipient,amount
        newtransaction = {
            'sender': sender,
            'recipient' : recipient,
            'amount' : amount,
        }
        self.current_transaction.append(newtransaction)
        return self.last_block['index']+1

    @property
    def last_block(self):
        return self.chain[-1]   

    @staticmethod
    def hash(block):
        block_string = json.dumps(block,sort_keys = True).encode()
        return hashlib.sha256(block_string).hexdigest()


    def pow(self, last_proof): #pow
        proof = 0
        while self.valid_proof(last_proof,proof) is False:
            proof += 1
        return proof

    @staticmethod
    def valid_proof(last_proof, proof): #pow verification
        guess = f'{last_proof}{proof}'.encode()
        guess_hash = hashlib.sha256(guess).hexdigest()
        return guess_hash[:5] == "00000"
