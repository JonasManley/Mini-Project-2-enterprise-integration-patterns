# Mini-Project-2-enterprise-integration-patterns

The main objective of this project is to improve practical experience in implementing integrating enterprise applications by use of integration platforms and middleware.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites
Software needed for running this project

- Install RabbitMQ (https://www.rabbitmq.com/download.html)
- Install Visual studio 2019 (https://visualstudio.microsoft.com/downloads/)

### Setup Instructions
- Download this repository
- Open both the client and server with Visual studio 2019
- Change file paths to the direction your project is installed (There are 2 paths to adjust, both located in Receive.cs)
	- Mini-Project-2-enterprise-integration-patterns -> MiniProject2Server -> Receive.cs
- Start RabbitMQ
- Run Server and the Client afterwards
	
## BPMN
**Take a look at our BPMN model to understand the functionality of our program.**
	- As you see on the diagram, we have a **client** and a **server**. The first thing which happen is that the client sends a type 	 and date for a car which they want to rent. Then the server recieve the request and checks for availability, based on want it 		finds a message will be sent back to the client. 
	1: if the server couldt find any cars, the client is asked to try another date or type.
	2: if the server finds any cars which matches the conditions, the color of the cars are then returned to client.
	3:
![BPMN model](BPMN/BPMNModel.JPG)	
	
## Intergration Patterns we cover

### Message Channel
![Request Reply](https://www.enterpriseintegrationpatterns.com/img/MessageChannelSolution.gif)
### Message
![Request Reply](https://www.enterpriseintegrationpatterns.com/img/MessageSolution.gif)
### Splitter
![Request Reply](https://www.enterpriseintegrationpatterns.com/img/Sequencer.gif)
### Aggregator
![Request Reply](https://www.enterpriseintegrationpatterns.com/img/Aggregator.gif)
### Request-Reply
![Request Reply](https://www.enterpriseintegrationpatterns.com/img/RequestReply.gif)
