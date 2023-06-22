# SolliBankEnterpriseQueue
Enterprise queue implementation to create a highly available and fault tolerant system that add, process and store bank transactions.

Imagine a scenario where you have a client sending some sort of important data to the server, the naive approach will be to establish
direct request response communication between client and server to tranfer the data.

This is for sure elegant and easy to implement, but what if the server is unavailable and cannot receive the data sent by the client? 
The data will be lost forever and if it is some sort of critical data like a bank transaction, than you don´t want to do that.

What you need instead is a "middleman" that can store that critical data sent by the client, to than later on be processed by the server.
Here even if the server is unavailable the data will still be available and can be processed later on when the server is running.

Here below a diagram of how this looks like
![Alt text](.vs/Untitled Diagram.drawio/to/img.jpg?raw=true "Title")
