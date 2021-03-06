# InventoryManager

## How to run
1. Pull down the repo.
2. Open the Solution with Visual Studio which should also install the Nuget packages for its dependencies.
3. Right click on the InventoryManagerUnitTests package inside of Solution Explorer and run all tests. 


## Assumptions: ##
* Responses are in JSON.
* Prefered boundary crossing structure is JSON 
* Authentication will be mocked
* Notification mechanism will be mocked and simplistic.
* Frequently I think of APIs being something that is tied to HTTP, however, for this exercise I am not going to build it with HTTP wrapping frameworks because a good API may be just something internal. It should be simple enough to update it to be a public facing API via HTTP. 


## Notes: ##
####Initial thoughts reading specs: 
When reading the doc for this exercise it seemed to me that there are two jobs going on here. I like to keep my API's as simple and side effect free as possible, and as such it made sense to me to move the cleanup of expired items into a seperate concern.

#####API Methods: 
* Add
* Retrieve
    
Items need to have something that allows them to notify when they are expired. This could either be some sort of functionality on the item, but I would rather have a inventory monitor which is in charge of notifying if an item is expired.
    
There should be a notifications provider such that I can hand it a message and it knows how the notifications will be delivered.    

####Tradeoffs:
- I wasn't sure what types of things were going to be stored and for now I am going with an assumption that the storage of information is very loosely defined. At some point it might make sense to say that this is an inventory for a department store and design a means for creating type categories (e. g. Perishables -> fruits and veggies, Auto, Home, etc) For the sake of this excercise I just care about the label, expiration, and item number / sku. I also made the sku an int for simplicity of testing etc. 

- I am taking the liberty of making retrieve work off of the sku. There are so many things that I am not answering as part of this project such as how to handle returning multiple items if someone searches by a generic label, or how do you determine uniqueness in the system? If I used a noSql db how would I make sure there weren't arbitrary orphaned items? If I used a SQL Db how would new a new type of item be added? 

- I don't like overloading the API with a bunch of notifications and this project had a lot of "side effects" that I don't like. I don't like my API having notifications as a side effect. Additionally I am not crazy about having the Retrieve clean up the expired items. Because I don't like side effects I created an "ExpirationMonitor" class that would ideally be run on the storage system on a regular basis (daily). 

- I chose not to put an Http request structure on my API but I think that it would have been good to do so, since that was what I was aiming for in the long run, and that is how I have been considering the IdP, and authorization etc. 

- This project would have been better if I had taken more time to create a storage mechanism that was outside of the WorkerAPI class. I created a Shelf class and that is what the ExpirationMonitor looks at to remove expired items but you would have to run that on the larger storage system. 

- I didn't like that I ended up putting parameters into the constructor for WorkerAPI for testing purposes. The way I did it is fine, but I am always a bit hesitant about making changes to production code for testing reasons.

####Final thoughts:
As I progressed through the exercise I regretted not making it wrapped up with HTTP POST GET etc because my initial thought was that way and as such there were some decisions that didn't make tons of sense outside of that context (e. g. using strings and a JSON convert to pass things between my "boundaries"). There are several things that I would clean up in a secondary version for example putting the dictionary of "shelves" in a class of its own instead of making it be persisted inside the WorkerAPI. I probably should have done a little more strict mocking in the WorkerAPI tests but I have found that if I test to strictly it becomes very combersome and pushes me into making integration tests. Generally I prefer to have many unit tests and fewer integration tests since integration tests are more prone to slowness. 
