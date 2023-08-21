# SolliBankEnterpriseQueue
Enterprise queue implementation to create a highly available and fault tolerant system that add, process and store bank transactions.

Imagine a scenario where you have a client sending some sort of important data to the server, the naive approach will be to establish
direct request response communication between client and server to transfer the data.

This is for sure elegant and easy to implement, but what if the server is unavailable and cannot receive the data sent by the client? 
The data will be lost forever and if it is some sort of critical data like a bank transaction, than you want to prevent that from happening.

What you need instead is a "middleman" that can store that critical data sent by the client, to later on be processed by the server.
Here even if the server is unavailable the data will still be available and can be processed later on when the server is running.

![client/server communication with a queue](https://github.com/yahiaalioua/SolliBankEnterpriseQueue/blob/ee41081d44940016d2d45f99884610463aa1b6c0/.vs/QueueDiagram1.drawio.png)

Now that we have an idea of how a queue can help us create a more fault tolerant system lets talk about the rules for implementing an enterprise queue.

First of all when a message is being processed from the queue by a consumer, we should make sure that the message is locked and can`t be processed by other
consumers.
Once the message is processed there should be 2 outcomes:
1) The message couldn´t be processed and therfore will fall into the dead letter queue or be deferred for a later retry.
2) The message is processed succesfully and will be removed from the queue.

![enterprise queue design](https://github.com/yahiaalioua/SolliBankEnterpriseQueue/blob/main/QueueDiagram2.drawio%20(1).png)

The pattern mentioned above has been implemented using Azure Service Bus queue and .Net
