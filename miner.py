from Blockchain import Blockchain
import hashlib
import json
from textwrap import dedent
from time import time
from uuid import uuid4

item = Blockchain()

def mine():

    last_block = item.last_block
    last_proof = last_block['proof']
    proof = item.pow(last_proof)

    previous_hash = item.hash(last_block)
    block = item.new_block(proof, previous_hash)

    response = {
        'message' : '새로운 블록이 채굴되었습니다',
        'index': block['index'],
        'transaction': block['transaction'],
        'proof': block['proof'],
        'previous_hash': block['previous_hash'],
    }

    return print(response)

def transact():
    sender = input("판매자 : ")
    recipient = input("구매자 : ")
    amount = input("가격 : ")
    values = {
        'sender' : sender,
        'recipient' : recipient,
        'amount' : amount,
    }
    index = item.new_transaction(values['sender'], values['recipient'], values['amount'])

   # response = {'새로운 {index}번 블록이 채굴되었습니다'}
    return print(response)


mine()
mine()
mine()
mine()
mine()
mine()
mine()
mine()
mine()


