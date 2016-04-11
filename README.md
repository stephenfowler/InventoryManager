# InventoryManager

## How to run


## Assumptions: ##
* Responses are in JSON.
* Prefered boundary crossing structure is JSON 
* Authentication will be mocked
* Notification mechanism will be mocked and simplistic.
* Frequently I think of APIs being something that is tied to HTTP, however, for this exercise I am not going to build it with HTTP wrapping frameworks because a good API may be just something internal. It should be simple enough to update it to be a public facing API via HTTP. 

## Notes: ##
####Initial thoughts reading specs: 

#####API Methods: 
* Add
* Retrieve
   * Needs to use a retrieve implementation that has notifications but shouldn't be required of the API

    
Items need to have something that allows them to notify when they are expired. This could either be some sort of functionality on the item, but I would rather have a inventory monitor which is in charge of notifying if an item is expired.
    
There should be a notifications provider such that I can hand it a message and it knows how the notifications will be delivered.    

####Tradeoffs or design decisions(DD):
DD - I wasn't sure what types of things were going to be stored and for now I am going with an assumption that the storage of information is very loosely defined. At some point it might make sense to say that this is an inventory for a department store and design a means for creating type categories (e. g. Perishables -> fruits and veggies, Auto, Home, etc) For the sake of this excercise I just care about the label, expiration, and item number / sku. I also made the sku an int for simplicity of testing etc. 

DD - I am taking the liberty of making retrieve work off of the sku. There are so many things that I am not answering as part of this project such as how to handle returning multiple items if someone searches by a generic label, or how do you determine uniqueness in the system? If I used a noSql db how would I make sure there weren't arbitrary orphaned items? If I used a SQL Db how would new a new type of item be added? 

DD - The unit test would be better made to mock strict so I would have to mock out the notifier. 

I chose not to put an Http request structure on my API but I think that it would have been good to do so, since that was what I was aiming for in the long run, and that is how I have been considering the IdP, and authorization etc. 






